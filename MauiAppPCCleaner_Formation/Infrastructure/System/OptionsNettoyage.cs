using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiAppPCCleaner_Formation.Infrastructure.System
{
    public class OptionsNettoyage
    {
        public bool IsCheckedVignettes { get; set; } = true;
        public bool IsCheckedNavigateur { get; set; } = true;
        public bool IsCheckedTemporaires { get; set; } = true;
        public bool IsCheckedCorbeille { get; set; } = true;
        public bool IsCheckedWinUpdate { get; set; } = true;
        public bool IsCheckedLivraison { get; set; } = true;
        public bool IsCheckedErreurs { get; set; } = true;
        public bool IsCheckedWindows { get; set; } = true;
        public bool IsCheckedShaders { get; set; } = true;
        public bool IsCheckedWindowsOld { get; set; } = true;
    }
}
