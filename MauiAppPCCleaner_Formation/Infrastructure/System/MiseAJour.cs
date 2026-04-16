using MauiAppPCCleaner_Formation.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MauiAppPCCleaner_Formation.Infrastructure.System
{
    public class MiseAJour
    {
        #region private readonly properties
        private readonly VersionLogiciel? _versionDistante;
        private readonly IOptions<Config> _config;  // -- config du logiciel installé
        #endregion

        #region constructeur
        public MiseAJour(IOptions<Config> config)
        {
            _config = config;
            _versionDistante = VersionAppUrl().Result;
        }
        #endregion


        #region Private method
        /// <summary>
        /// connexion vers le site pour verifier la version du logiciel et voir s'il faut faire une mise a jour
        /// </summary>
        /// <returns></returns>
        private async Task<VersionLogiciel?> VersionAppUrl()
        {
            try
            {
                HttpClient httpClient = new();

                if (string.IsNullOrEmpty(_config.Value.UrlVersion)) return null;

                using var result = httpClient.GetAsync(new Uri(_config.Value.UrlVersion));

                if (result.Result.IsSuccessStatusCode)
                {
                    var jsonResponse = await result.Result.Content.ReadAsStringAsync();
                    VersionLogiciel? element = JsonSerializer.Deserialize<VersionLogiciel>(jsonResponse, JsonOption.GetJsonOptions());

                    return element;

                }
                return null;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        #region public method
        /// <summary>
        /// method qui compare les 2 versions 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public bool Equals(string version)
        {
            if (_versionDistante is null) return false;
            return string.Equals(_versionDistante.Version,version);
        }

        public override string ToString()
        {
            return _versionDistante?.Version??string.Empty;
        }
        #endregion

    }
}
