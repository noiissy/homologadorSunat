﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Homologador.Fe.Auth
{
    public class SunatApi : SunatAuth
    {
        public SunatApi(string ruc, string user, string password)
            : base(ruc, user, password)
        {
        }

        /*
        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> Login()
        {


            using (var client = GetClient())
            {
                var content = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("username", _ruc + _user), 
                    new KeyValuePair<string, string>("password", _password), 
                    new KeyValuePair<string, string>("captcha", string.Empty), 
                    new KeyValuePair<string, string>("params", "*&*&/cl-ti-itmenu/MenuInternet.htm&b64d26a8b5af091923b23b6407a1c1db41e733a6"), 
                    new KeyValuePair<string, string>("exe", string.Empty) 
                });


                var r = await client.PostAsync("https://e-menu.sunat.gob.pe/cl-ti-itmenu/AutenticaMenuInternet.htm", content);
                //if (r.Headers.Contains("Location"))
                if (r.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }*/

        /// <summary>
        /// Gets the solicitudes.
        /// </summary>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<string> GetSolicitudes()
        {
            using (var client = CreatClient())
            {
                var r = await client.GetAsync("https://ww1.sunat.gob.pe/cl-ti-itconestsol/Consulta.htm?accion=consultaTodasSolicitudes&numRUC=" + Ruc +"&indTipoContrib=0");
                if (!r.IsSuccessStatusCode) return null;
                
                return await r.Content.ReadAsStringAsync();
            }    
        }

        public async Task<string> GetPruebas(string numProceso)
        {
            using (var client = CreatClient())
            {
                var content = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("accion", "consultarEtapa"),
                    new KeyValuePair<string, string>("numProceso", numProceso),
                    new KeyValuePair<string, string>("numEtapa", "2")
                });
                var r = await client.PostAsync("https://ww1.sunat.gob.pe/cl-ti-itconestsol/Consulta.htm", content);
                if (!r.IsSuccessStatusCode) return null;

                return await r.Content.ReadAsStringAsync();
            }
        }

    }
}