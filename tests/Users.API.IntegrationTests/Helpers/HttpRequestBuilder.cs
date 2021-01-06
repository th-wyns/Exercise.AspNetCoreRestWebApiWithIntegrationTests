using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Users.API.IntegrationTests.Models;

namespace Users.API.IntegrationTests.Helpers
{
    public class HttpRequestBuilder
    {
        private readonly HttpClient _httpClient;
        private readonly HttpRequestMessage _request;
        private readonly List<string> _mediaTypes = new List<string>();
        public IReadOnlyCollection<string> MediaTypes => _mediaTypes.Count > 0 ? _mediaTypes : new[] { AcceptTypeRepresentations.Json };

        public HttpRequestBuilder(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _request = new HttpRequestMessage();
        }

        public static HttpRequestBuilder Default(HttpClient httpClient)
        {
            return new HttpRequestBuilder(httpClient);
        }

        public static HttpRequestBuilder Accept(HttpClient httpClient, AcceptType acceptType)
        {
            var builder = new HttpRequestBuilder(httpClient);

            if (acceptType.HasFlag(AcceptType.Json))
            {
                builder.WithAcceptJson();
            }
            if (acceptType.HasFlag(AcceptType.Xml))
            {
                builder.WithAcceptXml();
            }
            if (acceptType.HasFlag(AcceptType.Unsupported))
            {
                builder.WithAcceptUnsupportedType();
            }

            return builder;
        }

        public static HttpRequestBuilder AcceptJson(HttpClient httpClient)
        {
            return new HttpRequestBuilder(httpClient).WithAcceptJson();
        }

        public static HttpRequestBuilder AcceptXml(HttpClient httpClient)
        {
            return new HttpRequestBuilder(httpClient).WithAcceptXml();
        }

        public static HttpRequestBuilder AcceptUnsupportedType(HttpClient httpClient)
        {
            return new HttpRequestBuilder(httpClient).WithAcceptUnsupportedType();
        }

        public HttpRequestBuilder WithAcceptJson()
        {
            WithAccept(AcceptTypeRepresentations.Json);
            return this;
        }

        public HttpRequestBuilder WithAcceptXml()
        {
            WithAccept(AcceptTypeRepresentations.Xml);
            return this;
        }

        public HttpRequestBuilder WithAcceptUnsupportedType()
        {
            WithAccept(AcceptTypeRepresentations.Unsupported);
            return this;
        }

        public HttpRequestBuilder WithAccept(string mediaType)
        {
            _mediaTypes.Add(mediaType);
            _request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            return this;
        }

        public HttpRequestBuilder WithHeader(string name, string value)
        {
            _request.Headers.TryAddWithoutValidation(name, value);
            return this;
        }

        public HttpRequestBuilder WithBody<T>(T model, ContentType contentType)
        {
            switch (contentType)
            {
                case ContentType.Unset:
                    WithJsonBody(model, ContentType.Unset);
                    break;
                case ContentType.Json:
                    WithJsonBody(model);
                    break;
                case ContentType.Xml:
                    WithXmlBody(model);
                    break;
                default:
                    throw new NotSupportedException($"Content type is not supported: {contentType}");
            }

            return this;
        }

        public HttpRequestBuilder WithJsonBody<T>(T model, ContentType contentType = ContentType.Json)
        {

            var jsonModel = SerializeJsonContent(model);
            _request.Content = new StringContent(jsonModel);
            _request.Content.Headers.ContentType.MediaType = ContentTypeRepresentations.ToMediaType(contentType);
            return this;
        }

        public HttpRequestBuilder WithXmlBody<T>(T model, ContentType contentType = ContentType.Xml)
        {
            var xmlModel = SerializeXmlContent(model);
            _request.Content = new StringContent(xmlModel);
            _request.Content.Headers.ContentType.MediaType = ContentTypeRepresentations.ToMediaType(contentType);
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string relativeUri = null)
        {
            _request.Method = method;
            _request.RequestUri = new Uri(_httpClient.BaseAddress, relativeUri);
            return await _httpClient.SendAsync(_request);
        }

        public async Task<HttpResponseMessage> GetAsync(string relativeUri = null)
        {
            return await SendAsync(HttpMethod.Get, relativeUri);
        }

        public async Task<HttpResponseMessage> PostAsync(string relativeUri = null)
        {
            return await SendAsync(HttpMethod.Post, relativeUri);
        }

        public async Task<HttpResponseMessage> PutAsync(string relativeUri = null)
        {
            return await SendAsync(HttpMethod.Put, relativeUri);
        }

        public async Task<HttpResponseMessage> PatchAsync(string relativeUri = null)
        {
            return await SendAsync(HttpMethod.Patch, relativeUri);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string relativeUri = null)
        {
            return await SendAsync(HttpMethod.Delete, relativeUri);
        }

        public async Task<(HttpResponseMessage, T)> SendAsync<T>(HttpMethod method, string relativeUri = null)
        {
            var response = await SendAsync(method, relativeUri);
            var mediaType = response.Content.Headers.ContentType.MediaType;

            switch (AcceptTypeRepresentations.ToAcceptType(mediaType))
            {
                case AcceptType.Json:
                case AcceptType.Unset:
                    return new (response, await DeserializeJsonContentAsync<T>(response));
                case AcceptType.Xml:
                    return (response, await DeserializeXmlContentAsync<T>(response));
                default:
                    throw new NotSupportedException($"Media type is not supported: {mediaType}");
            }
        }

        public async Task<(HttpResponseMessage, T)> GetAsync<T>(string relativeUri = null)
        {
            var result = await SendAsync<T>(HttpMethod.Get, relativeUri);
            return result;
        }

        public async Task<(HttpResponseMessage, T)> PostAsync<T>(string relativeUri = null)
        {
            var result = await SendAsync<T>(HttpMethod.Post, relativeUri);
            return result;
        }

        public async Task<(HttpResponseMessage, T)> PutAsync<T>(string relativeUri = null)
        {
            var result = await SendAsync<T>(HttpMethod.Put, relativeUri);
            return result;
        }


        private static async Task<T> DeserializeJsonContentAsync<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        private static async Task<T> DeserializeXmlContentAsync<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStreamAsync();
            var xmlSerializer = new XmlSerializer(typeof(T));
            var model = (T)xmlSerializer.Deserialize(content);
            return model;
        }

        private static string SerializeJsonContent<T>(T model)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            return jsonModel;
        }

        private static string SerializeXmlContent<T>(T model)
        {
            using var stream = new MemoryStream();
            using var reader = new StreamReader(stream);
            var xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(stream, model);
            stream.Seek(0, SeekOrigin.Begin);
            var xmlModel = reader.ReadToEnd();
            return xmlModel;
        }
    }
}
