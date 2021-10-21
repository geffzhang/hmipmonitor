using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using HmIpMonitor.Models;

namespace HmIpMonitor.Logic
{
    public class CcuApi
    {
        public CcuValueDto GetDataFor(string deviceId, string channel, string parameter)
        {
            try
            {
                var client = new HttpClient();
                var data = client.GetFromJsonAsync<CcuValueDto>(
                    $"http://ccu3-webui.speedport.ip:2121/device/{deviceId}/{channel}/{parameter}/~pv");
                var result = data.Result;
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine("error while trying to fetch data from ccu: " + e);
                return null;
            }
        }

        public CcuDeviceDto GetDeviceDataFor(string deviceId)
        {
            try
            {
                var client = new HttpClient();
                var data = client.GetFromJsonAsync<CcuDeviceDto>(
                    $"http://ccu3-webui.speedport.ip:2121/device/{deviceId}");
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