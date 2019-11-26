using DeLaTourAndroid.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeLaTourAndroid.Controlador
{
    class PinController
    {
        public async Task<pines> Registrar(pines pin)
        {
            pines _pin = new pines();
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(pin);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Helpers.apiUrl + "save-pin", content);
            string jsonString = await response.Content.ReadAsStringAsync();
            return _pin = JsonConvert.DeserializeObject<pines>(jsonString);
        }

        public async Task<pines> GetPinNombre(pines pin)
        {
            pines _pin = new pines();
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(pin);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Helpers.apiUrl + "pin/nombre", content);
            string jsonString = await response.Content.ReadAsStringAsync();
            char[] Chars = { '[', ']' };
            string newJsonString = jsonString.Trim(Chars);
            return _pin = JsonConvert.DeserializeObject<pines>(newJsonString);
        }

        public async Task<List<pines>> GetPinesAgregados()
        {
            List<pines> _pines = new List<pines>();
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(Helpers.apiUrl + "pines-agregados");
            string jsonString = await response.Content.ReadAsStringAsync();
            return _pines = JsonConvert.DeserializeObject<List<pines>>(jsonString);
        }

        public async Task<List<pines>> GetPinesRecomendados()
        {
            List<pines> _pines = new List<pines>();
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(Helpers.apiUrl + "pines-recomendados");
            string jsonString = await response.Content.ReadAsStringAsync();
            return _pines = JsonConvert.DeserializeObject<List<pines>>(jsonString);
        }
    }
}
