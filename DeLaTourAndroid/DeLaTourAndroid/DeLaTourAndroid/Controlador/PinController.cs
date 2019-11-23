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
        public async Task<Pin> Registrar(Pin pin)
        {
            Pin _pin = new Pin();
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(pin);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Helpers.apiUrl + "save-usuario", content);
            string jsonString = await response.Content.ReadAsStringAsync();
            return _pin = JsonConvert.DeserializeObject<Pin>(jsonString);
        }

        public async Task<List<Pin>> GetPinesAgregados()
        {
            List<Pin> _pines = new List<Pin>();
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(Helpers.apiUrl + "pines-agregados");
            string jsonString = await response.Content.ReadAsStringAsync();
            return _pines = JsonConvert.DeserializeObject<List<Pin>>(jsonString);
        }

        public async Task<List<Pin>> GetPinesRecomendados()
        {
            List<Pin> _pines = new List<Pin>();
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(Helpers.apiUrl + "pines-recomendados");
            string jsonString = await response.Content.ReadAsStringAsync();
            return _pines = JsonConvert.DeserializeObject<List<Pin>>(jsonString);
        }
    }
}
