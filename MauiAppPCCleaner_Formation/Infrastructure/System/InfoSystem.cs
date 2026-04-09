using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.Runtime.Versioning;

namespace MauiAppPCCleaner_Formation.Infrastructure.System
{
    public class InfoSystem
    {
        #region public properties
        public string? Os { get; set; }
        public string? Cpu { get; set; }
        public string? Gpu { get; set; }
        #endregion

        [SupportedOSPlatform("windows")]
        public void DownloadSystemInfo()
        {
            if (OperatingSystem.IsWindows())
            {
                // Platform not supported: provide safe fallback values
                Os = Environment.OSVersion.ToString();
                Cpu = "Unavailable on this platform";
                Gpu = "Unavailable on this platform";
                return;
            }

            // -- ici on simule le téléchargement des infos système
            // Get the WMI class
            ManagementClass managementClass = new("Win32_Processor");

            // Loop through the WMI class instances and print the processor information found
            foreach (ManagementObject managementObject in managementClass.GetInstances())
            {
                Console.WriteLine("--- Processor information ---");
                Console.WriteLine($"Name: {managementObject["Name"]}");
                Console.WriteLine($"Architecture: {managementObject["Architecture"]}");
            }
        }
    }
}
    