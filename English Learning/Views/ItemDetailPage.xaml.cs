using English_Learning.Services;
using English_Learning.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace English_Learning.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel(new DialogService());//верно?
        }
    }
}