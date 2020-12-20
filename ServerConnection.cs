#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeAssistantShortcuts
{
    sealed class PingResponse
    {
        public string? message { get; set; }
    }

    sealed class ServiceResponseItem
    {
        public string? domain { get; set; }
        public Dictionary<string, ServiceResponseItemService>? services { get; set; }
    }

    sealed class ServiceResponseItemService
    {
        public string? description { get; set; }
        public Dictionary<string, ServiceResponseItemServiceField>? fields { get; set; }
    }

    sealed class ServiceResponseItemServiceField
    {
        public string? description { get; set; }
        public JsonElement? example { get; set; }
    }

    public sealed class Service
    {
        public string Path { get; private set; }
        public string? Description { get; private set; }
        public string? PayloadPlaceholder { get; private set; }

        internal Service(string path, ServiceResponseItemService service)
        {
            Path = path;
            Description = service.description;

            if (service.fields?.Count > 0)
            {
                var options = new JsonSerializerOptions();
                options.WriteIndented = true;

                PayloadPlaceholder = JsonSerializer.Serialize(
                    service.fields.ToDictionary(field => field.Key, field => field.Value.example ?? new JsonElement()),
                    options
                );
            }
        }
    }

    public sealed class ServerConnection : IDisposable
    {
        private HttpClient? client = null;

        public string BaseUrl
        {
            set
            {
                if (value.Last() != '/')
                {
                    value.Append('/');
                }

                client = new HttpClient
                {
                    BaseAddress = new Uri(value),
                    Timeout = TimeSpan.FromSeconds(10)
                };
            }
        }

        public string? Token { set; private get; }

        public void Dispose()
        {
            client?.Dispose();
            client = null;
            GC.SuppressFinalize(this);
        }

        private async Task<T?> api<T>(HttpMethod method, string path, object? body = null) 
        {
            if (client is null)
            {
                throw new Exception($"{nameof(ServerConnection)}.{nameof(ServerConnection.api)} called before {nameof(BaseUrl)} has been set");
            }
            if (Token is null)
            {
                throw new Exception($"{nameof(ServerConnection)}.{nameof(ServerConnection.api)} called before {nameof(Token)} has been set");
            }

            using var message = new HttpRequestMessage(method, path);
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            message.Headers.Add("Accept", "application/json");

            using var bodyStream = new MemoryStream();
            if (!(body is null))
            {
                if (body is string stringBody)
                {
                    var bytes = Encoding.UTF8.GetBytes(stringBody);
                    await bodyStream.WriteAsync(bytes, 0, bytes.Length);
                }
                else
                {
                    await JsonSerializer.SerializeAsync(bodyStream, body);
                }
                await bodyStream.FlushAsync();
                bodyStream.Seek(0, SeekOrigin.Begin);
                message.Content = new StreamContent(bodyStream);
                message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            using var response = await client.SendAsync(message);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }

        public async Task<string?> Ping()
        {
            var response = await api<PingResponse>(HttpMethod.Get, "");
            return response?.message;
        }

        public async Task<List<Service>> ListServices()
        {
            var response = await api<List<ServiceResponseItem>>(HttpMethod.Get, "services");
            return (
                from item in response
                from service in item.services
                orderby item.domain, service.Key
                select new Service($"{item.domain}/{service.Key}", service.Value)
            ).ToList();
        }

        public async Task<List<object>?> CallService(string path, string payload = "")
        {
            return await api<List<object>>(HttpMethod.Post, $"services/{path}", payload);
        }
    }
}
