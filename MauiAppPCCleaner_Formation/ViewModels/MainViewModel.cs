using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppPCCleaner_Formation.Infrastructure.System;
using MauiAppPCCleaner_Formation.Models;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace MauiAppPCCleaner_Formation.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        #region private readonly properties
        private readonly IOptions<Config> _config;
        private readonly bool _IsCharging = true;
        #endregion

        #region public properties
        // -- propriété de la progress bar
        [ObservableProperty]
        public partial bool IsVisibleProgressBar { get; set; } = false;
        [ObservableProperty]
        public partial bool IsEnableProgressBar { get; set; } = false;
        [ObservableProperty]
        public partial int ProgressBarValue { get; set; } = 0;

        // -- propriété pour l'affichage du récapitulatif de nettoyage
        [ObservableProperty]
        public partial bool IsVisibleRecap { get; set; } = false;
        [ObservableProperty]
        public partial bool IsEnableRecap { get; set; } = false;

        // -- information du header
        [ObservableProperty]
        public partial string Os { get; set; }

        [ObservableProperty]
        public partial string Cpu { get; set; }

        [ObservableProperty]
        public partial string Gpu { get; set; }

        [ObservableProperty]
        public partial string Version { get; set; }

        // -- checkox des options de nettoyage
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

        // -- texte affiché après le nettoyage avec le récapitulatif
        [ObservableProperty]
        public partial string TextRecap { get; set; } = string.Empty;

        [ObservableProperty]
        public partial ObservableCollection<Rapport> Rapports { get; set; } = [];
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

            // - Pour éviter que l'enregistrement se fasse lors du chargement initial des checkbox
            _IsCharging = false;

        }
        #endregion


        #region public methods 
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


        /// <summary>
        /// method de nettoyage relié au bouton click nettoyage
        /// </summary>
        [RelayCommand]
        public void ClickedNettoyage()
        {
            try
            {
                IsVisibleProgressBar = true;
                IsEnableProgressBar = true;
                TextRecap = "Test " + IsCheckedVignettes + " " + IsCheckedNavigateur + " " + IsCheckedTemporaires + " " + IsCheckedCorbeille
                    + " " + IsCheckedWinUpdate + " " + IsCheckedLivraison + " " + IsCheckedErreurs + " " + IsCheckedWindows + " " + IsCheckedShaders
                    + " " + IsCheckedWindowsOld;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

        #region private methods
        /// <summary>
        /// Methode qui enregistre les informations du système en mémoire en sérialization d'un object
        /// </summary>
        //[RelayCommand]
        public void SaveOptionsNettoyage(string name, bool valeur)
        {
            try
            {
                // -- utilisation d'une plateforme pour enregistrer les preferences d'une application 
                // -- marche sur toutes les plateformes
                Preferences.Set(name, valeur);

                /*OptionsNettoyage options = new()
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
                // -- FileSystem.AppDataDirectory sous windows -> C:\Users\loulo\AppData\Local\User Name\com.companyname.mauiapppccleaner_formation\Data 
                string path = Path.Combine(FileSystem.AppDataDirectory, _config.Value.SaveOptionNettoyage);

                File.WriteAllText(path, json);*/

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Méthod qui lit le fichier json des options de nettoyage et l'affiche pour dans le menu
        /// </summary>
        private void LoadOptionsNettoyage()
        {
            try
            {
                IsCheckedVignettes = Preferences.Get("IsCheckedVignettes", true);
                IsCheckedNavigateur = Preferences.Get("IsCheckedNavigateur", true);
                IsCheckedTemporaires = Preferences.Get("IsCheckedTemporaires", true);
                IsCheckedCorbeille = Preferences.Get("IsCheckedCorbeille", true);
                IsCheckedWinUpdate = Preferences.Get("IsCheckedWinUpdate", true);
                IsCheckedLivraison = Preferences.Get("IsCheckedLivraison", true);
                IsCheckedErreurs = Preferences.Get("IsCheckedErreurs", true);
                IsCheckedWindows = Preferences.Get("IsCheckedWindows", true);
                IsCheckedShaders = Preferences.Get("IsCheckedShaders", true);
                IsCheckedWindowsOld = Preferences.Get("IsCheckedWindowsOld", true);

                /*if (_config.Value.SaveOptionNettoyage is null) return;
                string path = Path.Combine(FileSystem.AppDataDirectory, _config.Value.SaveOptionNettoyage);
                string json = File.ReadAllText(path);

                OptionsNettoyage? options = JsonSerializer.Deserialize<OptionsNettoyage>(json);
                options ??= new();
                
                IsCheckedVignettes = options.IsCheckedVignettes;
                IsCheckedNavigateur = options.IsCheckedNavigateur;
                IsCheckedTemporaires = options.IsCheckedTemporaires;
                IsCheckedCorbeille = options.IsCheckedCorbeille;
                IsCheckedWinUpdate = options.IsCheckedWinUpdate;
                IsCheckedLivraison = options.IsCheckedLivraison;
                IsCheckedErreurs = options.IsCheckedErreurs;
                IsCheckedWindows = options.IsCheckedWindows;
                IsCheckedShaders = options.IsCheckedShaders;
                IsCheckedWindowsOld = options.IsCheckedWindowsOld;*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion


        #region partial method 
        /// <summary>
        /// avec toolkit pour eviter event OnEventchanging il est possible de faire appel à cette méthod
        /// On{nom de la propriété suivi}Changed / 
        /// </summary>
        /// <param name="value">correspond Ischecked de la check box</param>
        partial void OnIsCheckedVignettesChanged(bool value)
        {
            if(!_IsCharging) SaveOptionsNettoyage("IsCheckedVignettes", IsCheckedVignettes);
        }

        partial void OnIsCheckedNavigateurChanged(bool value)
        {
            if (!_IsCharging)  SaveOptionsNettoyage("IsCheckedNavigateur", IsCheckedNavigateur);
        }

        partial void OnIsCheckedTemporairesChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage("IsCheckedTemporaires", IsCheckedTemporaires);
        }
        partial void OnIsCheckedCorbeilleChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage("IsCheckedCorbeille", IsCheckedCorbeille);
        }

        partial void OnIsCheckedWinUpdateChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage("IsCheckedWinUpdate", IsCheckedWinUpdate);
        }

        partial void OnIsCheckedLivraisonChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage("IsCheckedLivraison", IsCheckedLivraison);
        }

        partial void OnIsCheckedErreursChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage("IsCheckedErreurs", IsCheckedErreurs);
        }

        partial void OnIsCheckedShadersChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage("IsCheckedShaders", IsCheckedShaders);
        }

        partial void OnIsCheckedWindowsOldChanged(bool value)
        {
            if (!_IsCharging) SaveOptionsNettoyage("IsCheckedWindowsOld", IsCheckedWindowsOld);
        }
        #endregion

    }
}
