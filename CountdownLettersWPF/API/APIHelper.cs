using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CountdownLettersWPF.API
{
    public class APIHelper : IAPIHelper
    {
        private readonly HttpClient _apiClient;
        private const string _url = "http://www.anagramica.com";
        public HttpClient ApiClient
        {
            get
            {
                return _apiClient;
            }
        }
        public APIHelper()
        {
            _apiClient = new HttpClient
            {
                BaseAddress = new Uri(_url)
            };
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
