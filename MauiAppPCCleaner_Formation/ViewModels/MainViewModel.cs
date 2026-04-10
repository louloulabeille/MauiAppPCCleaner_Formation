using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppPCCleaner_Formation.Infrastructure.System;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace MauiAppPCCleaner_Formation.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {


        #region public properties 
        [ObservableProperty]
        public partial string Os { get; set; }
        
        [ObservableProperty]
        public partial string Cpu { get; set; }

        [ObservableProperty]
        public partial string Gpu { get; set; }
        #endregion

        #region Constructeur
        public MainViewModel()
        {
            InfoSystem info = new();
            Os = info.Os ?? string.Empty;
            Cpu = info.Cpu ?? string.Empty;
            Gpu = info.Gpu ?? string.Empty;
        }
        #endregion


        #region public methods 
        /// <summary>
        /// ouverture verrs le site internet du produit
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task ClickedInfo()
        {
            await Browser.Default.OpenAsync("", BrowserLaunchMode.SystemPreferred);
        }

        #endregion

    }
}
