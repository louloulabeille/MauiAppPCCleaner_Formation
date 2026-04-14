using MauiAppPCCleaner_Formation.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MauiAppPCCleaner_Formation.Infrastructure.System
{
    public class CleanSystem
    {
        #region private const Path 
        private const string _temp = @"C:\Windows\Temp";
        private const string _winUpdate = @"C:\Windows\SoftwareDistribution\Download";
        private const string _errors = @"C:\ProgramData\Microsoft\Windows\WER";
        private const string _logs = @"C:\Windows\System32\winevt\Logs";
        #endregion

        #region public properties
        private static long Taille = 0;  // -- en ko 
        #endregion

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
            try
            {
                //int retour = CleanSystem.SHEmptyRecycleBin(IntPtr.Zero, string.Empty, ((uint)RecycleFlags.SHERB_NOCONFIRMATION) | ((uint)RecycleFlags.SHERB_NOPROGRESSUI));
                int retour = CleanSystem.SHEmptyRecycleBin(IntPtr.Zero, string.Empty, ((uint)RecycleFlags.SHERB_NOCONFIRMATION));
                return new Rapport() { Title = "Vider la Corbeille", Message = retour == 0 ? "Corbeille vidée" : "Problème au niveau de la suppression des fichiers ou corbeille vide." };

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Rapport()
                {
                    Title = "Vider la Corbeille",
                    Message = "Erreur:  " + ex.Message,
                };
            }
        }

        /// <summary>
        /// method qui va supprimer les fichiers du répertoire temporaire Temp
        /// C:\Windows\Temp
        /// </summary>
        /// <returns></returns>
        public static Rapport CleanFichierTemporaire()
        {
            try
            {
                if (Directory.Exists(_temp))
                {
                    DirectoryInfo directory = new(_temp);
                    RapportDirectory rapportDirectory = new()
                    {
                        Taille = RepSize(directory)
                    };

                    Delete(directory, ref rapportDirectory);
                    Taille += rapportDirectory.Taille;
                    return new Rapport() { Title = "Fichiers temporaires", Message = rapportDirectory.ToString()};
                }

                return new Rapport()
                {
                    Title   = "Fichiers temporaires",
                    Message = "Répertoire temporaire introuvable.",
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Rapport()
                {
                    Title = "Fichiers temporaires",
                    Message = "Erreur : " + ex.Message,
                };
            }
            
        }

        /// <summary>
        /// method qui supprime les fichiers de téléchargements de windows update
        /// pour la mise en place des mises à jour
        /// </summary>
        /// <returns></returns>
        public static Rapport CleanWinUpdate()
        {
            try
            {
                if (Directory.Exists(_winUpdate))
                {
                    DirectoryInfo directory = new(_winUpdate);
                    RapportDirectory rapportDirectory = new ()
                    {
                        Taille = RepSize(directory),
                    };

                    Delete(directory, ref rapportDirectory);
                    Taille += rapportDirectory.Taille;
                    return new Rapport() { Title = "Fichiers Win update", Message = rapportDirectory.ToString() };
                }

                return new Rapport()
                {
                    Title = "Fichiers Win update",
                    Message = "Répertoire de téléchargement des mises à jour windows introuvable.",
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Rapport()
                {
                    Title = "Fichiers Win update",
                    Message = "Erreur : " + ex.Message,
                };
            }
        }

        /// <summary>
        /// method qui supprime les fichiers des rapport d'erreurs
        /// </summary>
        /// <returns></returns>
        public static Rapport CleanRapportErreurs()
        {
            try
            {
                if (Directory.Exists(_errors))
                {
                    DirectoryInfo directory = new(_errors);
                    RapportDirectory rapportDirectory = new ()
                    {
                        Taille = RepSize(directory),
                    };

                    Delete(directory, ref rapportDirectory);
                    Taille += rapportDirectory.Taille;
                    return new Rapport() { Title = "Rapport d'erreurs", Message = rapportDirectory.ToString() };
                }

                return new Rapport()
                {
                    Title = "Rapport d'erreurs",
                    Message = "Répertoire des rapports d'erreurs de windows est introuvable.",
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Rapport()
                {
                    Title = "Rapport d'erreurs",
                    Message = "Erreur : " + ex.Message,
                };
            }
        }

        /// <summary>
        /// method qui supprime les fichiers de logs de windows
        /// </summary>
        /// <returns></returns>
        public static Rapport CleanLogsWindows()
        {
            try
            {
                if (Directory.Exists(_logs))
                {
                    DirectoryInfo directory = new(_logs);
                    RapportDirectory rapportDirectory = new()
                    {
                        Taille = RepSize(directory),
                    };

                    Delete(directory, ref rapportDirectory);
                    Taille += rapportDirectory.Taille;
                    return new Rapport() { Title = "Log Windows", Message = rapportDirectory.ToString() };
                }

                return new Rapport()
                {
                    Title = "Log Windows",
                    Message = "Répertoire des logs de windows est introuvable.",
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Rapport()
                {
                    Title = "Log Windows",
                    Message = "Erreur : " + ex.Message,
                };
            }
        }
        #endregion


        #region private method

        /// <summary>
        /// supprime les fichiers et les répertoires du répertoire passé en paramètre
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="rapportDirectory"></param>
        private static void Delete(DirectoryInfo directory, ref RapportDirectory rapportDirectory)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
                rapportDirectory.NbFiles++;
            }

            foreach (DirectoryInfo rep in directory.GetDirectories())
            {
                Delete(rep, ref rapportDirectory);
                rapportDirectory.NbDirectory++;
            }
        }

        /// <summary>
        /// Calcule la taille d'un repertoire windows avec les sous-répertoires
        /// </summary>
        /// <param name="rep"></param>
        /// <returns></returns>
        private static long RepSize(DirectoryInfo rep)
        {
            long result = 0;

            //calcul en bit et conversion en sortie en Mo
            foreach (FileInfo file in rep.GetFiles())
            {
                result += file.Length;
            }

            foreach (DirectoryInfo directory in rep.GetDirectories())
            {
                result += RepSize(directory);
            }

            return result/8000;
        }

        #endregion

        #region public method 

        /// <summary>
        /// retourne la taille des fichiers supprimer
        /// </summary>
        /// <returns></returns>
        public static long GetTaille()
        {
            return Taille;
        }

        #endregion
    }



    #region Enumerable de option pour le vidage de la corbeille
    /// <summary>
    /// enumération pour l'option de la méthode de vider la corbeille
    /// </summary>
    enum RecycleFlags : uint
    {
        SHERB_NOCONFIRMATION = 0x00000001, // No empty confirmation
        SHERB_NOPROGRESSUI = 0x00000002, // No progress tracking
        SHERB_NOSOUND = 0x00000004 // No sound on completion
    }
    #endregion
}
