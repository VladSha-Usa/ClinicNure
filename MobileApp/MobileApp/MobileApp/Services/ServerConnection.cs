using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobileApp.Services
{
    public class ServerConnection<T>
    {
        const string Url = "";
        string urlEnd;
        bool isConnected;

        JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public ServerConnection()
        {
            isConnected = CheckConnection();
        }

        public void SetUrl(string url)
        {
            urlEnd += url;
        }

        public async Task<IEnumerable<T>> Get()
        {
            if (isConnected && Url.Length != 0)
            {
                HttpClient client = GetClient();
                string result = await client.GetStringAsync(Url + urlEnd);
                return JsonSerializer.Deserialize<IEnumerable<T>>(result, options);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Add(T obj)
        {
            if (!isConnected)
            {
                return false;
            }

            if (Url.Length != 0)
            {
                HttpClient client = GetClient();

                var response = await client.PostAsync(Url + urlEnd,
                    new StringContent(
                        JsonSerializer.Serialize(obj),
                        Encoding.UTF8, "application/json"));

                if (response.StatusCode != HttpStatusCode.OK)
                    return false;
            }

            return true;
        }

        public async Task<bool> Update(T obj)
        {
            if (!isConnected)
            {
                return false;
            }

            if (Url.Length != 0)
            {
                HttpClient client = GetClient();

                var response = await client.PutAsync(Url + urlEnd,
                    new StringContent(
                        JsonSerializer.Serialize(obj),
                        Encoding.UTF8, "application/json"));

                if (response.StatusCode != HttpStatusCode.OK)
                    return false;
            }

            return true;
        }

        private bool CheckConnection()
        {
            bool isConnected = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                IEnumerable<ConnectionProfile> profiles = Connectivity.ConnectionProfiles;

                foreach (ConnectionProfile profile in profiles)
                {
                    if (profile.ToString().Equals(ConnectionProfile.Cellular.ToString()) ||
                        profile.ToString().Equals(ConnectionProfile.WiFi.ToString()))
                    {
                        isConnected = true;
                    }
                }
            }
            else 
            {
                isConnected = false;
            }

            return isConnected;
        }

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
    }
}
