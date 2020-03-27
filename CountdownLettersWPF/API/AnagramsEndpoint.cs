using CountdownLettersWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CountdownLettersWPF.API
{
    public class AnagramsEndpoint : IAnagramsEndpoint
    {
        private readonly IAPIHelper _apiHelper;
        /// <summary>
        /// Constructor that uses di.
        /// </summary>
        /// <param name="apiHelper"></param>
        public AnagramsEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        /// <summary>
        /// Gets a list of anagrams for the input string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<List<string>> GetAnagrams(string text)
        {
            using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/all/{text}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<AnagramsResponseModel>();
                return result.all.ToList();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
