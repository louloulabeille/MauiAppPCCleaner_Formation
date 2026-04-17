using MauiAppPCCleaner_Formation.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MauiAppPCCleaner_Formation.Infrastructure.ExtendMethod
{
    public static class ViewModelsPages
    {
        extension(IServiceCollection collection)
        {
            /// <summary>
            /// injection dépendance des pages et des ViewModel
            /// </summary>
            /// <returns></returns>
            public IServiceCollection AddViewModelsPages()
            {
                collection.AddTransient<MainPage>();
                collection.AddTransient<MainViewModel>();

                collection.AddTransient<RamPage>();
                collection.AddTransient<RamViewModel>();

                collection.AddTransient<OptionsPage>();
                collection.AddTransient<OptionsViewModel>();

                collection.AddTransient<OutilsPage>();
                collection.AddTransient<OutilsViewModel>();

                collection.AddTransient<MajPage>();
                collection.AddTransient<MajViewModel>();

                return collection;
            }
        }

    }
}
