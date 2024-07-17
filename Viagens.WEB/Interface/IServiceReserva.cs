using Viagens.WEB.Models;

namespace Viagens.WEB.Interface;
public interface IServiceReserva
{
    Task<List<Reserva>> ConsultarReservas();
    Task<Reserva> ObterReservaId(int id);
    Task ExcluirReserva(int id);
    Task EditarReserva(Reserva reserva);
}
