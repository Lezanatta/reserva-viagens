using Compartilhado.Models;

namespace Compartilhado.Interface;
public interface IServiceReserva
{
    Task<List<Reserva>> ObterReservas();
    Task<Reserva> ObterReservaId(int id);
    Task AtualizarReserva(Reserva reserva);
    Task RealizarCheckin(Reserva reserva);
    Task<Reserva> InserirReserva(Reserva reserva);
    Task DeletarReserva(int id);
}
