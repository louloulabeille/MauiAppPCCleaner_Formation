using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Versioning;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace MauiAppPCCleaner_Formation.Infrastructure.System
{
    public class InfoSystem
    {
        #region public properties
        //public string? Os { get; set; }
        //public string? Cpu { get; set; }
        //public string? Gpu { get; set; }
        #endregion


        #region public method
        /// <summary>
        /// retourne la version du système d'exploitation
        /// </summary>
        /// <returns></returns>
        [SupportedOSPlatform("windows")]
        public static string GetVersion()
        {
            try
            {
                return Environment.OSVersion.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Error system version";
            }
        }

        /// <summary>
        /// recherche dans la base de registre de windows le nom du processeur
        /// et le retourne, sinon retourne un message d'erreur
        /// </summary>
        /// <returns></returns>
        public static string GetCpu()
        {
            try
            {
                RegistryKey? processor_name = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0",RegistryKeyPermissionCheck.ReadSubTree);
                if (processor_name is not null)
                {
                    return processor_name.GetValue("ProcessorNameString")?.ToString() ?? "Error system Cpu" ;
                }
                return "Error system Cpu";
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Error system Cpu";
            }
        }

        /// <summary>
        /// recherche dans la base de registre de windows le nom de la carte graphique
        /// et le retourne sinon retourne un message d'erreur
        /// </summary>
        /// <returns></returns>
        public static string GetGpu()
        {
            try
            {
                RegistryKey? gpu_name = Registry.LocalMachine.OpenSubKey(@"SYSTEM\ControlSet001\Control\Class\{4d36e968-e325-11ce-bfc1-08002be10318}\0000", RegistryKeyPermissionCheck.ReadSubTree);
                if(gpu_name is not null)
                {
                    return gpu_name.GetValue("DriverDesc")?.ToString() ?? "Error system Gpu";
                }
                 return "Error system Gpu";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Error system Gpu";
            }
        }
        #endregion

    }
}
    