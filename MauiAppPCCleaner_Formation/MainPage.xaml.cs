using MauiAppPCCleaner_Formation.Infrastructure.System;
using MauiAppPCCleaner_Formation.ViewModels;
using System.Text.Json;

namespace MauiAppPCCleaner_Formation
{
    public partial class MainPage : ContentPage
    {

        public MainPage( MainViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }

        /*protected override void OnParentSet()
        {
            base.OnParentSet();
            if(Parent is Window window)
            {
                window.Destroying += (s, e) =>
                {
                    if (BindingContext is MainViewModel vm) vm.SaveOptionsNettoyage(s, e);
                };
            }
        }*/

    }
}
