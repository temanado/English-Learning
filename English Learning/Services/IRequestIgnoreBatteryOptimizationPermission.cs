using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace English_Learning.Services
{
    public interface IRequestIgnoreBatteryOptimizationPermission
    {
        Task<bool> CheckStatusAsync();
        Task<bool> RequestAsync();
    }

}
