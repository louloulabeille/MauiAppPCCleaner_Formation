using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppPCCleaner_Formation.Infrastructure.System;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiAppPCCleaner_Formation.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        #region private readonly properties
        private readonly IOptions<Config> _config;
        #endregion

        #region public properties 
        [ObservableProperty]
        public partial string Os { get; set; }
        
        [ObservableProperty]
        public partial string Cpu { get; set; }

        [ObservableProperty]
        public partial string Gpu { get; set; }

        [ObservableProperty]
        public partial string Version { get; set; }
        #endregion

        #region Constructeur
        public MainViewModel(IOptions<Config> config )
        {
            _config = config;
            
            Os = InfoSystem.GetVersion();
            Cpu = string.Empty;
            Gpu = string.Empty;

            Version = _config.Value.Version;
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
            try
            {
                Uri url = new(_config.Value.UrlInfo);
                await Browser.Default.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        #endregion

    }
}
