using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppPCCleaner_Formation.Infrastructure.System;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiAppPCCleaner_Formation.ViewModels
{
    public partial class MajViewModel : ObservableObject
    {
        #region private readonly properties
        private readonly IOptions<Config> _config;
        #endregion

        #region public properties informations du header ObservableProperty
        // -- information du header
        [ObservableProperty]
        public partial string Os { get; set; }

        [ObservableProperty]
        public partial string Cpu { get; set; }

        [ObservableProperty]
        public partial string Gpu { get; set; }

        [ObservableProperty]
        public partial string Version { get; set; }
        #endregion

        #region public properties de Maj
        // -  ProgressBar
        [ObservableProperty]
        public partial double ProgressRingValue { get; set; } = 0;
        [ObservableProperty]
        public partial bool IsIndeterminateProgressRing { get; set; } = true;
        [ObservableProperty]
        public partial bool IsvisibleRechercheMaj { get; set; } = true;


        // - Message à afficher pas de Maj
        [ObservableProperty]
        public partial bool IsVisibleNotMaj { get; set; } = false;
        
        [ObservableProperty]    // -- mettre à jour avec la version distante
        public partial string VersionActuelleApp { get; set; } = "Version actuelle : ";

        // - Message mise a jour a faire
        [ObservableProperty]
        public partial bool IsVisibleUpdateTitle { get; set; } = false;

        #endregion

        #region constructeur
        public MajViewModel (IOptions<Config> config)
        {
            _config = config;
            Os = InfoSystem.GetVersion();
            Cpu = InfoSystem.GetCpu();
            Gpu = InfoSystem.GetGpu();
            Version = _config.Value.Version;

            Init();
            Update();
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

        #region public method Command Maj

        /// <summary>
        /// ouvre lien Url
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task OpenUrl()
        {
            try
            {
                Uri uriX = new(_config.Value.UrlInfo);
                await Browser.Default.OpenAsync(uriX, BrowserLaunchMode.SystemPreferred);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        /// <summary>
        /// Method quji va télécharger la derniere mise a jour
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task DownloadMaj()
        {
            try
            {
                Uri uriX = new("http://louloulabeille.alwaysdata.net/version/K-Lite_Codec_Pack_1966_Standard.exe");
                await Browser.Default.OpenAsync(uriX, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion

        #region private method
        /// <summary>
        /// intialise certains champs au démarage
        /// </summary>
        private void Init()
        {
            VersionActuelleApp += _config.Value.Version;
        }

        /// <summary>
        /// Vérifie si la mise jour est Ok
        /// </summary>
        private async void Update() {

            MiseAJour miseAJour = new(_config);

            if(miseAJour.Equals(_config.Value.Version))
            {
                IsVisibleNotMaj = true;
            }
            else
            {
                IsVisibleUpdateTitle = true;
            }
            IsIndeterminateProgressRing = false;
            IsvisibleRechercheMaj = false;

        }
        #endregion

    }
}
