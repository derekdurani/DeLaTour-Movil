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
        public async Task<usuarios> Login(usuarios usuario)
        {
            usuarios _usuario = new usuarios();
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Helpers.apiUrl + "login-usuario", content);
            string jsonString = await response.Content.ReadAsStringAsync();
            char[] Chars = { '[', ']' };
            string newJsonString = jsonString.Trim(Chars);
            return _usuario = JsonConvert.DeserializeObject<usuarios>(newJsonString);
        }

        public async Task<usuarios> Registrar(usuarios usuario)
        {
            usuarios _usuario = new usuarios();
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(usuario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Helpers.apiUrl + "save-usuario", content);
            string jsonString = await response.Content.ReadAsStringAsync();
            return _usuario = JsonConvert.DeserializeObject<usuarios>(jsonString);
        }
    }
}
