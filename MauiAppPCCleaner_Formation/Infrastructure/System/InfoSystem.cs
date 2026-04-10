using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Versioning;
using CommunityToolkit.Mvvm.Input;

namespace MauiAppPCCleaner_Formation.Infrastructure.System
{
    public class InfoSystem
    {
        #region public properties
        //public string? Os { get; set; }
        public string? Cpu { get; set; }
        public string? Gpu { get; set; }
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

        public static string GetCpu()
        {
            try
            {
                return string.Empty;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Error system Cpu";
            }
        }
        #endregion

    }
}
    