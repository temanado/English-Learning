using English_Learning.Services;
using English_Learning.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace English_Learning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomePageViewModel _viewModel; 

        public HomePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new HomePageViewModel(DependencyService.Get<IDialogService>(), DependencyService.Get<IRequestIgnoreBatteryOptimizationPermission>());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
