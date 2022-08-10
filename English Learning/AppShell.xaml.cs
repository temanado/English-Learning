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
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));//HomePage
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));//NewItemPage
            Routing.RegisterRoute(nameof(ArchivePage), typeof(ArchivePage));//ArchivePage
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));//ItemsPage
        }

    }
}
