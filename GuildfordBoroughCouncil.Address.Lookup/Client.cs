using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GuildfordBoroughCouncil.Address.Models;
using System.Net;

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

        private static string FormatForQueryString(string name, IEnumerable<string> data)
        {
            var qs = string.Join("&" + name + "=", data);

            return (!string.IsNullOrWhiteSpace(qs) ? "&" + qs : "");
        }

        /// <summary>
        /// Lookup a list of address details by post code
        /// </summary>
        /// <param name="postCode">Post code</param>
        /// <param name="IncludeHistorical">Include historical properties</param>
        /// <param name="CheckNational">Include results from AddressBase</param>
        /// <param name="Scope">The search scope</param>
        /// <param name="Classifications">Filter by property classification (defaults to Residential) - use "All" to get everything</param>
        /// <param name="PostallyAddressable">Filter to only postally addressable</param>
        /// <returns>List of <see cref="AddressDetails" /></returns>
        public static async Task<IEnumerable<Models.Address>> AddressDetailsByPostCode(string PostCode, bool IncludeHistorical = false, AddressSearchScope Scope = AddressSearchScope.Local, IEnumerable<string> Classifications = null, bool PostallyAddressable = true)
        {
            using (var client = GetClient())
            {
                var response = await client.GetAsync("Lookup/ByPostCode/" + PostCode + "?Scope=" + Scope.ToString() + "&IncludeHistorical=" + IncludeHistorical.ToString() + "&postallyaddressable=" + PostallyAddressable.ToString() + FormatForQueryString("classifications[]", Classifications));

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
        /// <param name="Uprn">UPRN</param>
        /// <returns>List of <see cref="AddressDetails" /></returns>
        public static async Task<IEnumerable<Models.Address>> AddressDetailsByUprn(long Uprn)
        {
            using (var client = GetClient())
            {
                var response = await client.GetAsync("Lookup/ByUprn/" + WebUtility.UrlEncode(Uprn.ToString()));

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
        /// <param name="Uprn">UPRN</param>
        /// <param name="IncludeHistorical">Include historical properties</param>
        /// <param name="CheckNational">Include results from AddressBase</param>
        /// <param name="Scope">The search scope</param>
        /// <returns>List of <see cref="AddressDetails" /></returns>
        [Obsolete]
        public static async Task<IEnumerable<Models.Address>> AddressDetailsByUprn(long Uprn, bool IncludeHistorical = false, AddressSearchScope Scope = AddressSearchScope.Local)
        {
            return await AddressDetailsByUprn(Uprn);
        }

        /// <summary>
        /// Lookup a list of address details by USRN
        /// </summary>
        /// <param name="Usrn">USRN</param>
        /// <param name="IncludeHistorical">Include historical properties</param>
        /// <param name="CheckNational">Include results from AddressBase</param>
        /// <param name="Scope">The search scope</param>
        /// <param name="Classifications">Filter by property classification (defaults to Residential) - use "All" to get everything</param>
        /// <param name="PostallyAddressable">Filter to only postally addressable</param>
        /// <returns>List of <see cref="AddressDetails" /></returns>
        public static async Task<IEnumerable<Models.Address>> AddressDetailsByUsrn(long Usrn, bool IncludeHistorical = false, AddressSearchScope Scope = AddressSearchScope.Local, IEnumerable<string> Classifications = null, bool PostallyAddressable = true)
        {
            using (var client = GetClient())
            {
                var response = await client.GetAsync("Lookup/OnStreet/" + WebUtility.UrlEncode(Usrn.ToString()) + "?Scope=" + Scope.ToString() + "&IncludeHistorical=" + IncludeHistorical.ToString() + "&postallyaddressable=" + PostallyAddressable.ToString() + FormatForQueryString("classifications[]", Classifications));

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
        /// <param name="Scope">The search scope</param>
        /// <param name="Classifications">Filter by property classification (defaults to Residential) - use "All" to get everything</param>
        /// <param name="PostallyAddressable">Filter to only postally addressable</param>
        /// <returns>List of <see cref="AddressDetails" /></returns>
        public static async Task<IEnumerable<Models.Address>> AddressDetailsBySomething(string Query, bool IncludeHistorical = false, AddressSearchScope Scope = AddressSearchScope.Local, IEnumerable<string> Classifications = null, bool PostallyAddressable = true)
        {
            using (var client = GetClient())
            {
                var response = await client.GetAsync("Lookup/BySomething/?Query=" + WebUtility.UrlEncode(Query) + "&Scope=" + Scope.ToString() + "&IncludeHistorical=" + IncludeHistorical.ToString() + "&postallyaddressable=" + PostallyAddressable.ToString() + FormatForQueryString("classifications[]", Classifications));

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
