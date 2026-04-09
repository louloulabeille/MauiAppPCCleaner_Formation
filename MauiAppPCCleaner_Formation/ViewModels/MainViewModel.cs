using CommunityToolkit.Mvvm.ComponentModel;
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
    }
}
