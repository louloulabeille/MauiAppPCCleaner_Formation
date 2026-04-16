 using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppPCCleaner_Formation.Infrastructure.System;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;

namespace MauiAppPCCleaner_Formation.ViewModels
{
    public partial class OutilsViewModel : ObservableObject
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

        #region public properties de outils
        [ObservableProperty]
        public partial string LabelResultRestauration { get; set; } = string.Empty;

        [ObservableProperty]
        public partial Color TextColorRestauration { get; set; } = Colors.Black;

        #endregion

        #region Constructeur
        public OutilsViewModel (IOptions<Config> config)
        {
            _config = config;
            Os = InfoSystem.GetVersion();
            Cpu = InfoSystem.GetCpu();
            Gpu = InfoSystem.GetGpu();
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
        /// <summary>
        /// method qui va créer un poit de restauration windows
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task ClickedCreateRestauration()
        {
            try
            {
                TextColorRestauration = Colors.Black;
                LabelResultRestauration = string.Empty;
                dynamic? restPoint = Interaction.GetObject("winmgmts:\\\\.\\root\\default:Systemrestore");

                if (restPoint is not null)
                {
                    if (restPoint.CreateRestorePoint("Pc Cleaner restore point", 0, 100) == 0)
                        LabelResultRestauration = "Point de restauration créé !";
                    else
                        LabelResultRestauration = "Echec lors de la création du point de restauration.";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                LabelResultRestauration = ex.Message;
                TextColorRestauration = Colors.Red;
            }
        }
        #endregion

    }
}
