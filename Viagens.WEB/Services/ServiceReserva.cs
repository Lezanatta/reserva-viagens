using System.Net.Http.Headers;
using System.Text.Json;
using Viagens.WEB.Interface;
using Viagens.WEB.Models;

namespace Viagens.WEB.Services;
public class ServiceReserva(HttpClient _client) : IServiceReserva
{
    public const string _caminhoBaseAPI = "Prod/api/viagens";
    private static readonly MediaTypeHeaderValue _contentType = new("application/json");
    public async Task<List<Reserva>> ConsultarReservas()
    {
        var response = await _client.GetAsync(_caminhoBaseAPI);

        if (!response.IsSuccessStatusCode) throw new ApplicationException($"Erro ao acessar a api! Erro: {response.ReasonPhrase}");

        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        return JsonSerializer.Deserialize<List<Reserva>>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task EditarReserva(Reserva reserva)
    {
        var dataAsString = JsonSerializer.Serialize(reserva);

        var content = new StringContent(dataAsString);

        content.Headers.ContentType = _contentType;

        var response = await _client.PutAsync(_caminhoBaseAPI, content);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong when calling API");
    }

    public async Task ExcluirReserva(int id)
    {
        var response = await _client.DeleteAsync($"{_caminhoBaseAPI}/{id}");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong when calling API");
    }

    public async Task<Reserva> ObterReservaId(int id)
    {
        var response = await _client.GetAsync($"{_caminhoBaseAPI}/{id}");

        if (!response.IsSuccessStatusCode) throw new ApplicationException($"Erro ao acessar a api! Erro: {response.ReasonPhrase}");

        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        return JsonSerializer.Deserialize<Reserva>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}
