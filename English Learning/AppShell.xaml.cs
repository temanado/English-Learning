using English_Learning.ViewModels;
using English_Learning.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace English_Learning
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(PreferencesPage), typeof(PreferencesPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(ArchivePage), typeof(ArchivePage));
            //Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
        }

    }
}
