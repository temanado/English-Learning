using English_Learning.Services;
using English_Learning.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace English_Learning.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreferencesPage : ContentPage
    {
        public PreferencesPage()
        {
            InitializeComponent();
            BindingContext = new PreferencesVievModel(new DialogService());//верно?
        }
    }
}