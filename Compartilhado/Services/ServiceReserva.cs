using Compartilhado.Interface;
using Compartilhado.Models;

namespace Compartilhado.Services;

public class ServiceReserva(IRepositoryReservas _repository) : IServiceReserva
{
    public async Task<List<Reserva>> ObterReservas() => await _repository.GetReservas();
    public Task<Reserva> ObterReservaId(int id) => _repository.ObterReservaId(id);
    public async Task<Reserva> InserirReserva(Reserva reserva) => await _repository.PersistirReserva(reserva);
    public async Task AtualizarReserva(Reserva reserva) => await _repository.AtualizarReserva(reserva);
    public async Task DeletarReserva(int id) => await _repository.DeletarReservaId(id);
    public async Task RealizarCheckin(Reserva reserva) => await _repository.RealizarCheckin(reserva);
}
