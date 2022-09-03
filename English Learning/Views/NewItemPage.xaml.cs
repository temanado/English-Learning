using English_Learning.Services;
using English_Learning.ViewModels;
using Xamarin.Forms;

namespace English_Learning.Views
{
    public partial class NewItemPage : ContentPage
    {
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel(new DialogService());//верно?
        }
    }
}