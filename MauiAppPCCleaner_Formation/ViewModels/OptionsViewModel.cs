using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppPCCleaner_Formation.Infrastructure.System;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

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
        [ObservableProperty]
        public partial bool IsCheckedNotification { get; set; } = false;

        [ObservableProperty]
        public partial List<string> ListWindows { get; set; } = [];
        [ObservableProperty]
        public partial string SelectedWindows { get; set; } = "Nettoyage";
        #endregion

        #region constructeur
        public OptionsViewModel (IOptions<Config> config)
        {
            _config = config;
            Version = _config.Value.Version;
            ChargingOptions();
            ChargingWindows();
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

        //public ICommand OpenLienUrl => new Command<string>(async (url) => await Launcher.OpenAsync(url));

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

        /// <summary>
        /// enregistrement de l'options de mise en place de la notification
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [RelayCommand]
        public void CheckedNotification(bool path)
        {
            try
            {
                Preferences.Set("CheckedNotification", path);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Methode implémenter dans toolkit sur event handler selectedonchanged
        /// </summary>
        /// <param name="value"></param>
        [RelayCommand]
        partial void OnSelectedWindowsChanged(string value)
        {
            try
            {
                Preferences.Set("DefaultWindows", value);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region private method
        /// <summary>
        /// method qui charge les options à l'ouverture de la fenêtre
        /// </summary>
        private void ChargingOptions()
        {
            IsCheckedNotification = Preferences.Get("CheckedNotification", false);
        }

        /// <summary>
        /// method qui charge les fenêtre existante dans l'application
        /// </summary>
        private void ChargingWindows()
        {
            ListWindows.Add("Nettoyage");
            ListWindows.Add("Ram");
            ListWindows.Add("Outils");
            ListWindows.Add("Options");
            ListWindows.Add("Maj");

            // -- sélectionne la page par défaut dans la liste
            SelectedWindows = Preferences.Get("DefaultWindows", "Nettoyage");
        }

        #endregion

    }
}
