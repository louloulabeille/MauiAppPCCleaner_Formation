using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppPCCleaner_Formation.Infrastructure.System;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiAppPCCleaner_Formation.ViewModels
{
    public partial class OptionsViewModel : ObservableObject
    {
        #region private readonly properties
        private readonly IOptions<Config> _config;
        #endregion

        #region public properties informations du header ObservableProperty
        // -- information du header
        [ObservableProperty]
        public partial string Os { get; set; } = InfoSystem.GetVersion();

        [ObservableProperty]
        public partial string Cpu { get; set; } = InfoSystem.GetCpu();

        [ObservableProperty]
        public partial string Gpu { get; set; } = InfoSystem.GetGpu();

        [ObservableProperty]
        public partial string Version { get; set; }
        #endregion

        #region public properties options 

        #endregion

        #region constructeur
        public OptionsViewModel (IOptions<Config> config)
        {
            _config = config;
            Version = _config.Value.Version;
        }
        #endregion

        #region command clicked button menu
        [RelayCommand]
        public async Task ClickedWindowsRam()
        {
            //await Shell.Current.GoToAsync("Test");
            await Shell.Current.Navigation.PushAsync(new RamPage(new RamViewModel(_config)));
        }

        [RelayCommand]
        public async Task ClickedWindowsNettoyage()
        {
            // recharge la page ne pas faire Current.GoToSync sinon plantage quand il s'appelle lui même
            await Shell.Current.Navigation.PushAsync(new MainPage(new MainViewModel(_config)));
        }

        [RelayCommand]
        public async Task ClickedWindowsOutils()
        {
            await Shell.Current.Navigation.PushAsync(new OutilsPage(new OutilsViewModel(_config)));
        }

        [RelayCommand]
        public async Task ClickedWindowsOptions()
        {
            await Shell.Current.Navigation.PushAsync(new OptionsPage(new OptionsViewModel(_config)));
        }

        [RelayCommand]
        public async Task ClickedWindowsMaj()
        {
            await Shell.Current.Navigation.PushAsync(new MajPage(new MajViewModel(_config)));
        }

        /// <summary>
        /// ouverture vers le site internet du produit
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

        #region public method command Options
        /// <summary>
        /// prend en paramètre url ou autre pour connaitre le lien qu'il faut ouvrir
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [RelayCommand]
        public async Task ClickedWeb(string url)
        {
            try
            {
                switch (url)
                {
                    case "PCCleaner":
                        await ClickedInfo();
                        break;
                    case "Twitter":
                        Uri uriX = new(_config.Value.UrlTwitter ?? "https://x.com/");
                        await Browser.Default.OpenAsync(uriX, BrowserLaunchMode.SystemPreferred);
                        break;
                    case "Youtube":
                        Uri uriYou = new(_config.Value.UrlYoutube ?? "https://www.youtube.com/YouTube/fr-fr");
                        await Browser.Default.OpenAsync(uriYou, BrowserLaunchMode.SystemPreferred);
                        break;
                    case "GitHub":
                        Uri uriGit = new(_config.Value.UrlGitHub ?? "https://github.com/louloulabeille/MauiAppPCCleaner_Formation");
                        await Browser.Default.OpenAsync(uriGit, BrowserLaunchMode.SystemPreferred);
                        break;
                    default:
                        Uri uri = new(_config.Value.UrlFacebook?? "https://fr-fr.facebook.com/");
                        await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                        break;
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region private method

        #endregion

    }
}
