using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeAssistantShortcuts
{
    class PingResponse
    {
        public string message { get; set; }
    }

    class ServiceResponseItem
    {
        public string domain { get; set; }
        public Dictionary<string, ServiceResponseItemService> services { get; set; }
    }

    class ServiceResponseItemService
    {
        public string description { get; set; }
        public Dictionary<string, ServiceResponseItemServiceField> fields { get; set; }
    }

    class ServiceResponseItemServiceField
    {
        public string description { get; set; }
        // example: any
    }

    public class Service
    {
        public string Path { get; set; }
        public string Description { get; set; }
    }

    public class ServerConnection : IDisposable
    {
        private HttpClient client;

        public void Dispose()
        {
            client.Dispose();
        }

        public string BaseUrl
        {
            set
            {
                if (value.Last() != '/')
                {
                    value.Append('/');
                }

                client = new HttpClient();
                client.BaseAddress = new Uri(value);
                client.Timeout = TimeSpan.FromSeconds(10);
            }
            get => client?.BaseAddress.ToString();
        }

        public string Token { set; private get; }

        private async Task<T> api<T>(HttpMethod method, string path, object body = null) 
        {
            using var message = new HttpRequestMessage(method, path);
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
            message.Headers.Add("Accept", "application/json");

            using var bodyStream = new MemoryStream();
            if (body != null)
            {
                await JsonSerializer.SerializeAsync(bodyStream, body);
                message.Content = new StreamContent(bodyStream);
                message.Headers.Add("Content-Type", "application/json");
            }

            using var response = await client.SendAsync(message);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(stream);
        }

        public async Task<string> Ping()
        {
            var response = await api<PingResponse>(HttpMethod.Get, "");
            return response.message;
        }

        public async Task<List<Service>> ListServices()
        {
            var response = await api<List<ServiceResponseItem>>(HttpMethod.Get, "services");
            return (
                from item in response
                from service in item.services
                orderby item.domain, service.Key
                select new Service() {
                    Path = $"{item.domain}/{service.Key}",
                    Description = service.Value.description,
                }
            ).ToList();
        }
    }
}
