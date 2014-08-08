using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GuildfordBoroughCouncil.Address.Models;
using System.Web;

namespace GuildfordBoroughCouncil.Address
{
    /// <summary>
    /// This is a wrapper for the SinglePoint SearchService from Aligned Assets.
    /// </summary>
    public class Lookup
    {
        private static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = Api.Client.Properties.Settings.Default.AddressServiceUri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        /// <summary>
        /// Lookup a list of address details by post code
        /// </summary>
        /// <param name="postCode">Post code</param>
        /// <param name="IncludeHistorical">Include historical properties</param>
        /// <param name="CheckNational">Include results from AddressBase</param>
        /// <returns>List of <see cref="AddressDetails" /></returns>
        public static async Task<IEnumerable<Models.Address>> AddressDetailsByPostCode(string PostCode, bool IncludeHistorical = false, AddressSearchScope Scope = AddressSearchScope.Local)
        {
            using (var client = GetClient())
            {
                var response = client.GetAsync("Lookup/ByPostCode/" + PostCode + "?Scope=" + Scope.ToString() + "&IncludeHistorical=" + IncludeHistorical.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<Models.Address>>();
                }
                else
                {
                    return new List<Models.Address>();
                }
            }
        }

        /// <summary>
        /// Lookup a list of address details by UPRN
        /// </summary>
        /// <param name="uprn">UPRN</param>
        /// <param name="IncludeHistorical">Include historical properties</param>
        /// <param name="CheckNational">Include results from AddressBase</param>
        /// <returns>List of <see cref="AddressDetails" /></returns>
        public static async Task<IEnumerable<Models.Address>> AddressDetailsByUprn(Int64 Uprn, bool IncludeHistorical = false, AddressSearchScope Scope = AddressSearchScope.Local)
        {
            using (var client = GetClient())
            {
                var response = client.GetAsync("Lookup/ByUprn/" + HttpUtility.UrlEncode(Uprn.ToString()) + "?Scope=" + Scope.ToString() + "&IncludeHistorical=" + IncludeHistorical.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<Models.Address>>();
                }
                else
                {
                    return new List<Models.Address>();
                }
            }
        }

        /// <summary>
        /// Lookup a list of address details by USRN
        /// </summary>
        /// <param name="usrn">USRN</param>
        /// <param name="IncludeHistorical">Include historical properties</param>
        /// <param name="CheckNational">Include results from AddressBase</param>
        /// <returns>List of <see cref="AddressDetails" /></returns>
        public static async Task<IEnumerable<Models.Address>> AddressDetailsByUsrn(Int64 Usrn, bool IncludeHistorical = false, AddressSearchScope Scope = AddressSearchScope.Local)
        {
            using (var client = GetClient())
            {
                var response = client.GetAsync("Lookup/OnStreet/" + HttpUtility.UrlEncode(Usrn.ToString()) + "?Scope=" + Scope.ToString() + "&IncludeHistorical=" + IncludeHistorical.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<Models.Address>>();
                }
                else
                {
                    return new List<Models.Address>();
                }
            }
        }

        /// <summary>
        /// Lookup a list of address details by something
        /// </summary>
        /// <param name="Query">Query</param>
        /// <param name="IncludeHistorical">Include historical properties</param>
        /// <param name="CheckNational">Include results from AddressBase</param>
        /// <returns>List of <see cref="AddressDetails" /></returns>
        public static async Task<IEnumerable<Models.Address>> AddressDetailsBySomething(string Query, bool IncludeHistorical = false, AddressSearchScope Scope = AddressSearchScope.Local)
        {
            using (var client = GetClient())
            {
                var response = client.GetAsync("Lookup/BySomething/?Query=" + HttpUtility.UrlEncode(Query) + "&Scope=" + Scope.ToString() + "&IncludeHistorical=" + IncludeHistorical.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<Models.Address>>();
                }
                else
                {
                    return new List<Models.Address>();
                }
            }
        }
    }
}
