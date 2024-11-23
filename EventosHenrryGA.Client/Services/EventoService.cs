using EventosHenrryGA.Entities;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace EventosHenrryGA.Client.Services
{
    public class EventoService
    {
        private readonly HttpClient http;
        private readonly IJSRuntime jsRuntime;  // Inyectar IJSRuntime

        // Constructor actualizado con la inyección de IJSRuntime
        public EventoService(HttpClient _http, IJSRuntime _jsRuntime)
        {
            this.http = _http;
            this.jsRuntime = _jsRuntime;  // Asignamos IJSRuntime
        }

        public async Task<List<EventoCLS>> GetEventos()
        {
            return await http.GetFromJsonAsync<List<EventoCLS>>("api/evento");
        }

        public async Task<EventoCLS> GetEvento(int idEvento)
        {
            return await http.GetFromJsonAsync<EventoCLS>($"api/evento/{idEvento}");
        }

        public async Task<List<TipoEventoCLS>> GetTipoEventos()
        {
            return await http.GetFromJsonAsync<List<TipoEventoCLS>>("api/tipoevento");
        }

        public async Task<string> GetArchivo(int idEvento)
        {
            try
            {
                var response = await http.GetFromJsonAsync<byte[]>($"api/evento/recuperarArchivo/{idEvento}");
                if (response == null)
                {
                    return "";
                }
                else
                {
                    return Convert.ToBase64String(response);
                }
            }
            catch (Exception)
            {
                return "";
            }

        }

        public async Task<bool> SaveEvento(EventoCLS objEvento)
        {
            try
            {
                var response = await http.PostAsJsonAsync("api/evento", objEvento);

                // Si la respuesta es exitosa, devuelve true
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    // Si la respuesta no fue exitosa, puedes devolver false
                    return false;
                }
            }
            catch (Exception ex)
            {
                // En caso de excepción, también devuelve false
                return false;
            }
        }

        public async Task<string> DeleteEvento(int idEvento)
        {
            try
            {
                var response = await http.DeleteAsync($"api/evento/{idEvento}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                else
                {
                    return $"Error: {await response.Content.ReadAsStringAsync()}";
                }

            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
            
        }


        public async Task Descargar(int idEvento, string nombreEvento)
        {
            var archivoBase64 = await GetArchivo(idEvento);
            if (archivoBase64 == "")
            {
                await jsRuntime.InvokeVoidAsync("alert", "No se pudo descargar el archivo");
                return;
            }

            await jsRuntime.InvokeVoidAsync("descargarArchivo", archivoBase64, nombreEvento); // Usamos jsRuntime para llamar a la función de JavaScript
        }
    }
}
