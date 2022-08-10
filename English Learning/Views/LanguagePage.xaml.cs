using English_Learning.Models;
using English_Learning.ViewModels;
using English_Learning.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace English_Learning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LanguagePage : ContentPage
    {
        LanguageViewModel _viewModel;

        public LanguagePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new LanguageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}