using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppPCCleaner_Formation.Infrastructure.System;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Text;

namespace MauiAppPCCleaner_Formation.ViewModels
{
    public partial class RamViewModel : ObservableObject
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

        #region public properties window Ram
        [ObservableProperty]
        public partial bool IsVisibleLAbelRam { get; set; } = false;
        
        [ObservableProperty]
        public partial bool IsIndeterminateProgressRing { get; set; } = false;

        [ObservableProperty]
        public partial string LabelRamTotale { get; set; } = "x Mb";

        [ObservableProperty]
        public partial string LabelRamUtilisée { get; set; } = "x Mb";

        [ObservableProperty]
        public partial string LabelRamLibre { get; set; } = "x Mb";

        [ObservableProperty]
        public partial double ProgressRingValue { get; set; } = 0;

        [ObservableProperty]
        public partial string LabelRamPourcent { get; set; } = "%";

        #endregion

        #region constructeur
        public RamViewModel (IOptions<Config> config)
        {
            _config = config;
            Os = InfoSystem.GetVersion();
            Cpu = InfoSystem.GetCpu();
            Gpu = InfoSystem.GetGpu();
            Version = _config.Value.Version;

            TakeRamUsage();
        }
        #endregion

        #region command clicked button Ram
        [RelayCommand]
        public async Task ClickedCleanRam()
        {
            IsVisibleLAbelRam = true;
            IsIndeterminateProgressRing = true;
            await OptimizeRam();  //-> optimisation de la ram
            TakeRamUsage(); // -> pour recalculer l'utilisation de la ram
            IsIndeterminateProgressRing = false;
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

        #region private method 
        /// <summary>
        /// calcul de Ram utilisé et affichage dans la fenêtre 
        /// </summary>
        private void TakeRamUsage()
        {
            try
            {
                ManagementObjectSearcher ramMonitor = new("Select TotalVisibleMemorySize,FreePhysicalMemory from Win32_OperatingSystem");
                ulong totalRam = 0;
                ulong freeRam = 0;
                ulong useRam = 0;

                foreach(ManagementObject oject in ramMonitor.Get()) {
                    totalRam = Convert.ToUInt64(oject["TotalVisibleMemorySize"]);
                    freeRam = Convert.ToUInt64(oject["FreePhysicalMemory"]);
                }

                useRam = totalRam - freeRam;
                int pourcentFreeRam = (int)Math.Round((double)(freeRam * 100) / totalRam,0);

                ProgressRingValue = (1 - ((double)pourcentFreeRam / 100));
                
                LabelRamPourcent = (100 - pourcentFreeRam).ToString() + " %";

                LabelRamTotale =$"{Math.Round((double)totalRam/1000,0)} Mo" ;
                LabelRamUtilisée = $"{Math.Round((double)useRam/1000,0)} Mo {100-pourcentFreeRam}%";
                LabelRamLibre = $"{Math.Round((double)freeRam/1000,0)} Mo {pourcentFreeRam}%" 
                ;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// method qui va optimizer la ram
        /// </summary>
        /// <returns></returns>
        private async Task OptimizeRam()
        {
            try
            {
                GC.Collect(1, GCCollectionMode.Forced);
                //GC.Collect();
                GC.WaitForPendingFinalizers();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            
        }


        #endregion
    }
}
