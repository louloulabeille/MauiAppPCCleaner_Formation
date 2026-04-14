using System;
using System.Collections.Generic;
using System.Text;

namespace MauiAppPCCleaner_Formation.Models
{
    public class RapportDirectory
    {
        public int NbFiles { get; set; } = 0;
        public int NbDirectory { get; set; } = 0;
        public long Taille { get; set; } = 0;

        #region public metho override
        public override string ToString()
        {
            return "Nombre de fichiers supprimer : " + NbFiles + "\n"
                        + "Nombre de répertoires supprimer : " + NbDirectory + "\n"
                        + "Taille : " + Taille + "ko.";
        }
        #endregion
    }
}
