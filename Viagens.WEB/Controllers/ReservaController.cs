using Microsoft.AspNetCore.Mvc;
using Viagens.WEB.Interface;
using Viagens.WEB.Models;

namespace Viagens.WEB.Controllers;
public class ReservaController (IServiceReserva _serviceReserva) : Controller
{
    public IActionResult Index() => View();

    public async Task<IActionResult> ObterReservas()
    {
        var reservas = await _serviceReserva.ConsultarReservas();

        return View("Index", reservas.AsEnumerable());
    }

    public async Task<IActionResult> Editar(int id)
    {
        var reserva = await _serviceReserva.ObterReservaId(id);

        return View("Editar", reserva);
    }

    public async Task<IActionResult> AtualizarReserva(Reserva reserva)
    {
        try
        {
            await _serviceReserva.EditarReserva(reserva);
            TempData["Message"] = "Reserva atualizada com sucesso!";
            TempData["MessageType"] = "success"; 
        }
        catch (Exception)
        {
            TempData["Message"] = "Ocorreu um erro ao atualizar a reserva.";
            TempData["MessageType"] = "error";
        }

        return RedirectToAction("ObterReservas");
    }

    public async Task<IActionResult> ExcluirReserva(int id)
    {
        await _serviceReserva.ExcluirReserva(id);

        return View();
    }
}
