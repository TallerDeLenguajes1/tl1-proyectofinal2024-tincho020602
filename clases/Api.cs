namespace datosApi;

using System.Text.Json;
using System.Text.Json.Serialization;

class ConsumirApi
{
    public async Task<string>  ObtenerInsulto()
    {
        string ruta = "https://evilinsult/generate_insult.php?lang=es&type=json";
        try
        {
            // Instancio un nuevo cliente HTTP
            HttpClient cliente = new HttpClient();
            // Envió una solicitud asincrónica del tipo get a la API
            HttpResponseMessage respuesta = await cliente.GetAsync(ruta);
            // Aseguro un código de respuesta de éxito, caso contrario, lanza una excepción
            respuesta.EnsureSuccessStatusCode();
            // Si el código de respuesta fue correcto, transformo el dato obtenido a un string
            string responseBody = await respuesta.Content.ReadAsStringAsync();
            // Deserealizo el json
            var apiResponse = JsonSerializer.Deserialize<InsultosRoot>(
                responseBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            // Si la deserealización no da null, muestro por pantalla los datos
            return apiResponse.Insult;
        }
        catch (Exception ex)
        {
            string[] insultosRespaldo = {"tarado", "anda payá bobo","peruano"};
            return insultosRespaldo[new Random().Next(insultosRespaldo.Count())];
        }
    }
}

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class InsultosRoot
{
    [JsonPropertyName("insult")]
    public string Insult { get; set; }
}
