using Newtonsoft.Json;
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
        private readonly string _apiKey = "6654c90249ad3051153260lxz8237d3";

        public datConversion()
        {
            _client = new HttpClient();
        }

        public async Task<string> ObtenerDireccion(double lat, double lng)
        {
            string url = $"https://geocode.maps.co/reverse?lat={lat}&lon={lng}&api_key={_apiKey}";

            using (var response = await _client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();

                var json = JObject.Parse(jsonResponse);

                // Ajustar el análisis JSON basado en la nueva estructura de la respuesta
                string formattedAddress = json["display_name"]?.ToString();

                return formattedAddress ?? "";
            }
        }

        public async Task<Tuple<double, double>> ConvertirDireccionALatitudLongitud(string direccion)
        {
            try
            {
                string url = $"https://geocode.maps.co/search?q={direccion}&api_key={_apiKey}";

                using (var response = await _client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    var json = JArray.Parse(jsonResponse);

                    if (json.Count > 0)
                    {
                        var location = json[0];
                        double latitud = Convert.ToDouble(location["lat"]);
                        double longitud = Convert.ToDouble(location["lon"]);
                        return new Tuple<double, double>(latitud, longitud);
                    }

                    // Si no se pueden obtener las coordenadas de latitud y longitud, devolver null
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                throw;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al analizar la respuesta JSON: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error desconocido: {ex.Message}");
                throw;
            }
        }
    }

}
