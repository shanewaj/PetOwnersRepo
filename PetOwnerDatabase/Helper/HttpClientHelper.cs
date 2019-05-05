using PetOwnerDatabase.Models;
using PetOwnerDatabase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PetOwnerDatabase.Helper
{
    public class HttpClientHelper
    {

        /// <summary>
        /// Build a http client
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static HttpClient GetHttpClient(string url)
        {
            var MyHttpClient = new HttpClient();
            MyHttpClient.DefaultRequestHeaders.Accept.Clear();
            MyHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            MyHttpClient.BaseAddress = new Uri(url);
            return MyHttpClient;
        }

        /// <summary>
        /// Consume web api
        /// </summary>
        /// <returns></returns>
        private static async Task<IEnumerable<PetOwnersModel>> ConsumeApiAsync(string url)
        {
            IEnumerable<PetOwnersModel> petOwners = Enumerable.Empty<PetOwnersModel>();

            if (!string.IsNullOrEmpty(url))
            {
                using (var client = GetHttpClient(url))
                {
                    HttpResponseMessage responseMessage = await client.GetAsync("");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return responseMessage.Content.ReadAsAsync<IList<PetOwnersModel>>().Result;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Build View Model from Json Data
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<GenderViewModel> getPetsViewModel(string url)
        {
            IEnumerable<GenderViewModel> viewModel = Enumerable.Empty<GenderViewModel>();

            IEnumerable<PetOwnersModel> petOwnersList = ConsumeApiAsync(url).Result;

            if(petOwnersList != null)
            {
                viewModel = ConvertToViewModel(petOwnersList);
            }

            return viewModel;
        }

        /// <summary>
        /// Convert Views
        /// </summary>
        /// <param name="petOwnersList"></param>
        /// <returns></returns>
        public static IEnumerable<GenderViewModel> ConvertToViewModel(IEnumerable<PetOwnersModel> petOwnersList)
        {
            return petOwnersList.GroupBy(o => o.Gender).Select(group => new GenderViewModel
            {
                Gender = group.Key,
                Pets = group.SelectMany(x => x.Pets != null ?
                               x.Pets.Where(t => t.Type.Equals("cat", StringComparison.CurrentCultureIgnoreCase)) : Enumerable.Empty<Pet>())
            }).ToList();
        }
    }
}
