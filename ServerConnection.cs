﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Runtime.Remoting.Messaging;
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
        public string Name { get; set; }
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
            if (!(body is null))
            {
                if (body is string)
                {
                    var bytes = Encoding.UTF8.GetBytes((string)body);
                    await bodyStream.WriteAsync(bytes, 0, bytes.Length);
                }
                else
                {
                    await JsonSerializer.SerializeAsync(bodyStream, body);
                }
                message.Content = new StreamContent(bodyStream);
                message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
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

        public async Task<List<object>> CallService(string path, string payload = "")
        {
            return await api<List<object>>(HttpMethod.Post, $"services/{path}", payload);
        }
    }
}
