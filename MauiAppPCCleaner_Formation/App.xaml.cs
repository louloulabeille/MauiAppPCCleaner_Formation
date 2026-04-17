using MauiAppPCCleaner_Formation.Infrastructure.System;
using MauiAppPCCleaner_Formation.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MauiAppPCCleaner_Formation
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}