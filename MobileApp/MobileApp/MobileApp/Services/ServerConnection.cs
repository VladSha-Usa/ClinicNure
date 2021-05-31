using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MobileApp.Services
{
    class ServerConnection<T>
    {
        bool isConnected;

        public ServerConnection()
        {
            isConnected = CheckConnection();
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
    }
}
