using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppPCCleaner_Formation.Infrastructure.System;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MauiAppPCCleaner_Formation.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        #region private readonly properties
        private readonly IOptions<Config> _config;
        private bool _IsCharging = true;
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

        // -- checkox des options de nétoyage
        [ObservableProperty]
        public partial bool IsCheckedVignettes { get; set; }
        [ObservableProperty]
        public partial bool IsCheckedNavigateur { get; set; }
        [ObservableProperty]
        public partial bool IsCheckedTemporaires { get; set; }
        [ObservableProperty]
        public partial bool IsCheckedCorbeille { get; set; }
        [ObservableProperty]
        public partial bool IsCheckedWinUpdate { get; set; }
        [ObservableProperty]
        public partial bool IsCheckedLivraison { get; set; }
        [ObservableProperty]
        public partial bool IsCheckedErreurs { get; set; }
        [ObservableProperty]
        public partial bool IsCheckedWindows { get; set; }
        [ObservableProperty]
        public partial bool IsCheckedShaders { get; set; }
        [ObservableProperty]
        public partial bool IsCheckedWindowsOld { get; set; }
        #endregion

        #region Constructeur
        public MainViewModel(IOptions<Config> config)
        {
            _config = config;

            Os = InfoSystem.GetVersion();
            Cpu = InfoSystem.GetCpu();
            Gpu = InfoSystem.GetGpu();
            Version = _config.Value.Version;

            LoadOptionsNettoyage();

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

        #region private methods
        /// <summary>
        /// Methode qui enregistre les informations du système en mémoire
        /// </summary>
        //[RelayCommand]
        public async Task SaveOptionsNettoyage()
        {
            try
            {
                OptionsNettoyage options = new()
                {
                    IsCheckedVignettes = IsCheckedVignettes,
                    IsCheckedNavigateur = IsCheckedNavigateur,
                    IsCheckedTemporaires = IsCheckedTemporaires,
                    IsCheckedCorbeille = IsCheckedCorbeille,
                    IsCheckedWinUpdate = IsCheckedWinUpdate,
                    IsCheckedLivraison = IsCheckedLivraison,
                    IsCheckedErreurs = IsCheckedErreurs,
                    IsCheckedWindows = IsCheckedWindows,
                    IsCheckedShaders = IsCheckedShaders,
                    IsCheckedWindowsOld = IsCheckedWindowsOld,
                };
                string json = JsonSerializer.Serialize(options);
                if (_config.Value.SaveOptionNettoyage is null) return;
                string path = Path.Combine(FileSystem.AppDataDirectory, _config.Value.SaveOptionNettoyage);

                File.WriteAllText(path, json);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async void LoadOptionsNettoyage()
        {
            try
            {
                if (_config.Value.SaveOptionNettoyage is null) return;
                string path = Path.Combine(FileSystem.AppDataDirectory, _config.Value.SaveOptionNettoyage);
                string json = File.ReadAllText(path);

                OptionsNettoyage? options = JsonSerializer.Deserialize<OptionsNettoyage>(json);
                if (options is not null)
                {
                    IsCheckedVignettes      = options.IsCheckedVignettes;
                    IsCheckedNavigateur     = options.IsCheckedNavigateur;
                    IsCheckedTemporaires    = options.IsCheckedTemporaires;
                    IsCheckedCorbeille      = options.IsCheckedCorbeille;
                    IsCheckedWinUpdate      = options.IsCheckedWinUpdate;
                    IsCheckedLivraison      = options.IsCheckedLivraison;
                    IsCheckedErreurs        = options.IsCheckedErreurs;
                    IsCheckedWindows        = options.IsCheckedWindows;
                    IsCheckedShaders        = options.IsCheckedShaders;
                    IsCheckedWindowsOld     = options.IsCheckedWindowsOld;
                }
                else
                {
                    IsCheckedVignettes  = true;
                    IsCheckedNavigateur = true;
                    IsCheckedTemporaires = true;
                    IsCheckedCorbeille  = true;
                    IsCheckedWinUpdate  = true;
                    IsCheckedLivraison  = true;
                    IsCheckedErreurs    = true;
                    IsCheckedWindows    = true;
                    IsCheckedShaders    = true;
                    IsCheckedWindowsOld = true;
                }
                _IsCharging = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion


        #region partial method lors de la modification de la checkbox des vignettes
        partial void OnIsCheckedVignettesChanged(bool value)
        {
            if(!_IsCharging) SaveOptionsNettoyage();
        }

        partial void OnIsCheckedNavigateurChanged(bool value)
        {
            if (!_IsCharging)  SaveOptionsNettoyage();
        }

        partial void OnIsCheckedTemporairesChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage();
        }
        partial void OnIsCheckedCorbeilleChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage();
        }

        partial void OnIsCheckedWinUpdateChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage();
        }

        partial void OnIsCheckedLivraisonChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage();
        }

        partial void OnIsCheckedErreursChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage();
        }

        partial void OnIsCheckedShadersChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage();
        }

        partial void OnIsCheckedWindowsOldChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage();
        }
        #endregion

    }
}
