using MauiAppPCCleaner_Formation.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MauiAppPCCleaner_Formation.Infrastructure.System
{
    public class CleanSystem
    {
        #region import Dll c++ de windows pour vider la corbeille
        [DllImport("shell32.dll",CharSet = CharSet.Unicode)]
        //[LibraryImport("shell32",EntryPoint = "SHEmptyRecycleBin", StringMarshalling = StringMarshalling.Custom)]
        private static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, uint dwFlags);

        #endregion

        #region methode public de nettoyage du system
        /// <summary>
        /// Vide la corbeille de windows
        /// </summary>
        /// <returns></returns>
        public static Rapport CleanCorbeille()
        {
            //int retour = CleanSystem.SHEmptyRecycleBin(IntPtr.Zero, string.Empty, ((uint)RecycleFlags.SHERB_NOCONFIRMATION) | ((uint)RecycleFlags.SHERB_NOPROGRESSUI));
            int retour = CleanSystem.SHEmptyRecycleBin(IntPtr.Zero, string.Empty, ((uint)RecycleFlags.SHERB_NOCONFIRMATION) );
            return new Rapport() { Title = "Vider la Corbeille", Message = retour == 0? "Corbeille vidée":"Problème au niveau de la suppression des fichiers dans la corbeille ou corbeille vide." };
        }

        #endregion
    }

    /// <summary>
    /// enumération pour l'option de la méthode de vider la corbeille
    /// </summary>
    enum RecycleFlags : uint
    {
        SHERB_NOCONFIRMATION = 0x00000001, // No empty confirmation
        SHERB_NOPROGRESSUI = 0x00000002, // No progress tracking
        SHERB_NOSOUND = 0x00000004 // No sound on completion
    }
}
