using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CapaDatos
{
    public class datConversion
    {
        private readonly HttpClient _client;

        public datConversion()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "32aadede91msh175e095d3a9b556p104517jsn922d679d6095");
            _client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "map-geocoding.p.rapidapi.com");
        }

        public async Task<string> ObtenerDireccion(double lat, double lng)
        {
            string url = $"https://map-geocoding.p.rapidapi.com/json?latlng={lat}%2C{lng}";

            using (var response = await _client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Parsear la respuesta JSON utilizando JObject
                var json = JObject.Parse(jsonResponse);

                // Acceder a los datos deseados
                var results = json["results"];
                string formattedAddress = "";

                foreach (var result in results)
                {
                    formattedAddress = result["formatted_address"].ToString();

                    var addressComponents = result["address_components"];
                    string streetNumber = GetAddressComponentValue(addressComponents, "street_number");
                    string routeName = GetAddressComponentValue(addressComponents, "route");
                    string localityName = GetAddressComponentValue(addressComponents, "locality");
                    string postalCode = GetAddressComponentValue(addressComponents, "postal_code");
                    string countryName = GetAddressComponentValue(addressComponents, "country");

                    // Construir la dirección
                    string direccion = "";
                    if (!string.IsNullOrEmpty(streetNumber))
                    {
                        direccion += $"{streetNumber} ";
                    }
                    direccion += routeName;

                    // Si la localidad está disponible, agregarla solo si no se repite
                    if (!string.IsNullOrEmpty(localityName) && !direccion.Contains(localityName))
                    {
                        direccion += $", {localityName}";
                    }

                    // Agregar el código postal y el país
                    if (!string.IsNullOrEmpty(postalCode))
                    {
                        direccion += $", {postalCode}";
                    }
                    if (!string.IsNullOrEmpty(countryName))
                    {
                        direccion += $", {countryName}";
                    }

                    // Devolver los datos que necesitas
                    return $"{direccion}";
                }

                // Si no se encuentra ninguna dirección, devolver vacío
                return "";
            }
        }

        // Método auxiliar para obtener el valor de un componente de dirección específico
        private string GetAddressComponentValue(JToken addressComponents, string type)
        {
            foreach (var component in addressComponents)
            {
                var types = component["types"].ToObject<List<string>>();
                if (types.Contains(type))
                {
                    return component["long_name"].ToString();
                }
            }
            return "";
        }

        public async Task<Tuple<double, double>> ConvertirDireccionALatitudLongitud(string direccion)
        {
            string url = $"https://map-geocoding.p.rapidapi.com/json?address={direccion}";

            using (var response = await _client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();

                var json = JObject.Parse(jsonResponse);

                // Verificar el estado de la respuesta
                if (json["status"].ToString() == "OK")
                {
                    var results = json["results"];
                    if (results.HasValues)
                    {
                        var location = results[0]["geometry"]["location"];
                        double latitud = Convert.ToDouble(location["lat"]);
                        double longitud = Convert.ToDouble(location["lng"]);
                        return new Tuple<double, double>(latitud, longitud);
                    }
                }
            }

            // Si no se pueden obtener las coordenadas de latitud y longitud, devolver null
            return null;
        }

    }
}
