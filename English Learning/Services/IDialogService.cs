using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace English_Learning.Services
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
    }
}
