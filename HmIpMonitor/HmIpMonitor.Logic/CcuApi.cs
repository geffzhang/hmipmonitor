using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using HmIpMonitor.Contracts;
using HmIpMonitor.Contracts.CcuApi;
using Samhammer.DependencyInjection.Attributes;

namespace HmIpMonitor.Logic
{
    [Inject()]
    public class CcuApi : ICcuApi
    {
        public CcuValue GetDataFor(string deviceId, string channel, string parameter)
        {
            return FetchData<CcuValue>(deviceId, $"{channel}/{parameter}/~pv");
        }

        public CcuDevice GetDeviceDataFor(string deviceId)
        {
            return FetchData<CcuDevice>(deviceId);
        }

        private T FetchData<T>(string deviceId, string partialUrl = null)
        where T: class
        {
            try
            {
                var client = new HttpClient();
                var data = client.GetFromJsonAsync<T>(
                    $"http://192.168.2.145:2121/device/{deviceId}/{partialUrl ?? string.Empty}");
                var result = data.Result;
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine("error while trying to fetch data from ccu: " + e);
                return null;
            }
        }
    }
}