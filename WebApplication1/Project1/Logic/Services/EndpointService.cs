using ReportApp.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReportApp.Logic.Services
{
    public class EndpointService
    {
        static HttpClient client = new HttpClient();
        public static string ConnectionString = string.Empty;

        async public static Task<object> GetRequestFromEndpoint(string target, object[] filters, string connectionString = "")
        {
            object product = null;
            string address = ConnectionString + target;
            if (filters.Length > 1)
            {
                address += "/" + filters[0] + (string.IsNullOrEmpty(filters[1] + "") ? string.Empty : "/" + filters[1]);
            }

            HttpResponseMessage response = await client.GetAsync(address);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return product;
        }
    }
}
