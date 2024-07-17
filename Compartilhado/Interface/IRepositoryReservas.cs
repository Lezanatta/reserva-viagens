using Compartilhado.Models;

namespace Compartilhado.Interface;
public interface IRepositoryReservas
{
    Task AtualizarReserva(Reserva reserva);
    Task<List<Reserva>> GetReservas();
    Task<Reserva> ObterReservaId(int id);
    Task RealizarCheckin(Reserva reserva);
    Task<Reserva> PersistirReserva(Reserva reserva);
    Task DeletarReservaId(int id);
}
