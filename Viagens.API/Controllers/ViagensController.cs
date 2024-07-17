using Microsoft.AspNetCore.Mvc;
using Compartilhado.Interface;
using Compartilhado.Models;

namespace Viagens.API.Controllers;
[Route("api/[controller]")]
public class ViagensController(IServiceReserva _serviceReserva) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var reservas = await _serviceReserva.ObterReservas();

        return Ok(reservas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetId(int id)
    {
        var reserva = await _serviceReserva.ObterReservaId(id);

        return Ok(reserva);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Reserva reserva)
    {
        ArgumentNullException.ThrowIfNull(reserva);

        var reservaInserida = await _serviceReserva.InserirReserva(reserva);

        return Ok(reservaInserida);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Reserva reserva)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(reserva);

            await _serviceReserva.AtualizarReserva(reserva);

            return Ok(reserva);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao atualizar reserva. | {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _serviceReserva.DeletarReserva(id);

            return Ok($"Reserva com Id: {id} deletada com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Não foi possível deletar a reserva com Id: {id} | {ex.Message}");
        }
    }
}


