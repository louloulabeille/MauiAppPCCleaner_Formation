using System;
using System.Collections.Generic;
using System.Management;
using System.Text;
using System.Runtime.Versioning;
using CommunityToolkit.Mvvm.Input;

namespace MauiAppPCCleaner_Formation.Infrastructure.System
{
    public class InfoSystem
    {
        #region public properties
        public string? Os { get; set; }
        public string? Cpu { get; set; }
        public string? Gpu { get; set; }
        #endregion


        public InfoSystem()
        {
            if (OperatingSystem.IsWindows())
            {
                DownloadSystemInfo();
            }
        }


        #region private methods
        [SupportedOSPlatform("windows")]
        private void DownloadSystemInfo()
        {
            ManagementObjectSearcher osObject = new ("SELECT * FROM Win32_OperatingSystem");
            
            foreach (ManagementObject mo in osObject.Get())
            {
                Console.WriteLine("Name - {0}", mo["Name"]);
                
                Os = mo["Name"]?.ToString()?.Split('|')[0] ?? string.Empty;

            }
            ManagementObjectSearcher cpuObject = new ("select * from Win32_Processor");
            foreach (ManagementObject mo in cpuObject.Get())
            {
                Cpu = mo["Name"].ToString();
                /*Console.WriteLine("Name - {0}", mo["Name"]);
                Console.WriteLine("Status - {0}", mo["Status"]);
                Console.WriteLine("Caption - {0}", mo["Caption"]);
                Console.WriteLine("DeviceID - {0}", mo["DeviceID"]);
                Console.WriteLine("AdapterRAM - {0}", mo["AdapterRAM"]);
                Console.WriteLine("AdapterDACType - {0}", mo["AdapterDACType"]);
                Console.WriteLine("Monochrome - {0}", mo["Monochrome"]);
                Console.WriteLine("InstalledDisplayDrivers - {0}", mo["InstalledDisplayDrivers"]);
                Console.WriteLine("DriverVersion - {0}", mo["DriverVersion"]);
                Console.WriteLine("VideoProcessor - {0}", mo["VideoProcessor"]);
                Console.WriteLine("VideoArchitecture - {0}", mo["VideoArchitecture"]);
                Console.WriteLine("VideoMemoryType - {0}", mo["VideoMemoryType"]);*/
            }
            ManagementObjectSearcher gpuObject = new ("SELECT * FROM Win32_VideoController");
            foreach (ManagementObject mo in gpuObject.Get())
            {
                Gpu = mo["Name"].ToString();
                /*Console.WriteLine("Name - {0}", mo["Name"]);
                Console.WriteLine("Status - {0}", mo["Status"]);
                Console.WriteLine("Caption - {0}", mo["Caption"]);
                Console.WriteLine("DeviceID - {0}", mo["DeviceID"]);
                Console.WriteLine("AdapterRAM - {0}", mo["AdapterRAM"]);
                Console.WriteLine("AdapterDACType - {0}", mo["AdapterDACType"]);
                Console.WriteLine("Monochrome - {0}", mo["Monochrome"]);
                Console.WriteLine("InstalledDisplayDrivers - {0}", mo["InstalledDisplayDrivers"]);
                Console.WriteLine("DriverVersion - {0}", mo["DriverVersion"]);
                Console.WriteLine("VideoProcessor - {0}", mo["VideoProcessor"]);
                Console.WriteLine("VideoArchitecture - {0}", mo["VideoArchitecture"]);
                Console.WriteLine("VideoMemoryType - {0}", mo["VideoMemoryType"]);*/
            }

            //Os = Environment.OSVersion.ToString();

            
            // -- ici on simule le téléchargement des infos système
            // Get the WMI class
            //ManagementClass managementClass = new("Win32_Processor");

            // Loop through the WMI class instances and print the processor information found
            /*foreach (ManagementObject managementObject in managementClass.GetInstances())
            {
                Console.WriteLine("--- Processor information ---");
                Console.WriteLine($"Name: {managementObject["Name"]}");
                Console.WriteLine($"Architecture: {managementObject["Architecture"]}");
            }*/
            // Platform not supported: provide safe fallback values
            //Cpu = DeviceInfo.Current.Model;
            //Gpu = DeviceInfo.Current.Manufacturer;
            
        }
        #endregion
    }
}
    