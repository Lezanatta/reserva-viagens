using Microsoft.EntityFrameworkCore;
using Compartilhado.Context;
using Compartilhado.Interface;
using Compartilhado.Models;

namespace Compartilhado.Repository;
public class RepositoryReservas(ViagensContext _context) : IRepositoryReservas
{
    public async Task RealizarCheckin(Reserva reserva)
    {
        if(reserva is null) throw new Exception();

        await _context.Reserva.Where(r => r.IdReserva == reserva.IdReserva && r.Checkin == 0).
                    ExecuteUpdateAsync(setters => setters.SetProperty(r => r.Checkin, 1)
                    .SetProperty(r => r.DataCheckin, DateTime.Now));
    }

    public async Task AtualizarReserva(Reserva reserva)
    {
        if (reserva is null) throw new Exception();

        await _context.Reserva.Where(r => r.IdReserva == reserva.IdReserva).
                ExecuteUpdateAsync(setters => setters.SetProperty(r => r.IdPagamento, reserva.IdPagamento)
                    .SetProperty(r => r.IdEstado, reserva.IdEstado)
                    .SetProperty(r => r.DataReserva, reserva.DataReserva));
    }

    public async Task<List<Reserva>> GetReservas() => await _context.Reserva.ToListAsync();

    public async Task<Reserva> PersistirReserva(Reserva reserva)
    {
        _context.Reserva.Add(reserva);

        await _context.SaveChangesAsync();

        return reserva;
    }

    public async Task<Reserva> ObterReservaId(int id)
    {
        var reserva = await _context.Reserva.Where(r => r.IdReserva == id).FirstOrDefaultAsync();

        if(reserva is null) throw new ArgumentException("Id não encontrado!");

        return reserva;
    }

    public async Task DeletarReservaId(int id)
    {
        var reserva = await _context.Reserva.Where(r => r.IdReserva == id).FirstOrDefaultAsync();

        if (reserva is null) throw new ArgumentException("Id não encontrado!");

        _context.Reserva.Remove(reserva);

        await _context.SaveChangesAsync();
    }
}
