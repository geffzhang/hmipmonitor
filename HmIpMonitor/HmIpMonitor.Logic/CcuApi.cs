using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using HmIpMonitor.Contracts;
using Samhammer.DependencyInjection.Attributes;

namespace HmIpMonitor.Logic
{
    [Inject()]
    public class CcuApi : ICcuApi
    {
        public CcuValueDto GetDataFor(string deviceId, string channel, string parameter)
        {
            return FetchData<CcuValueDto>(deviceId, $"{channel}/{parameter}/~pv");
        }

        public CcuDeviceDto GetDeviceDataFor(string deviceId)
        {
            return FetchData<CcuDeviceDto>(deviceId);
        }

        private T FetchData<T>(string deviceId, string partialUrl = null)
        where T: class
        {
            try
            {
                var client = new HttpClient();
                var data = client.GetFromJsonAsync<T>(
                    $"http://ccu3-webui.speedport.ip:2121/device/{deviceId}/{partialUrl ?? string.Empty}");
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