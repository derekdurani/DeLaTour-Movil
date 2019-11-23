using DeLaTourAndroid.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeLaTourAndroid.Controlador
{
    class UsuarioController
    {
        public async Task<Usuario> Login(UsuarioController usuario)
        {
            Usuario _usuario = new Usuario();
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Helpers.apiUrl + "login-usuario", content);
            string jsonString = await response.Content.ReadAsStringAsync();
            return _usuario = JsonConvert.DeserializeObject<Usuario>(jsonString);
        }

        public async Task<Usuario> Registrar(UsuarioController usuario)
        {
            Usuario _usuario = new Usuario();
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Helpers.apiUrl + "save-usuario", content);
            string jsonString = await response.Content.ReadAsStringAsync();
            return _usuario = JsonConvert.DeserializeObject<Usuario>(jsonString);
        }
    }
}
