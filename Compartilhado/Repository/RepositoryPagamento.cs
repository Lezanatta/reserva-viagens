using Compartilhado.Context;
using Compartilhado.Interface;
using Compartilhado.Models;
using Microsoft.EntityFrameworkCore;

namespace Compartilhado.Repository;
public class RepositoryPagamento(ViagensContext _context) : IRepositoryPagamento
{
    public async Task<Pagamento> AtualizarPagamento(Pagamento pagamento)
    {
        if(pagamento is null) throw new ArgumentException(nameof(pagamento));

        await _context.Pagamento.Where(pag => pag.IdPagamento == pagamento.IdPagamento).
            ExecuteUpdateAsync(set => set.SetProperty(pag => pag.DataPagamento, pagamento.DataPagamento)
                .SetProperty(pag => pag.ValorPago, pagamento.ValorPago)
                .SetProperty(pag => pag.ValorTotal, pagamento.ValorTotal)
                .SetProperty(pag => pag.Parcelas, pagamento.Parcelas)
                .SetProperty(pag => pag.ParcelaAtual, pagamento.ParcelaAtual));

        return pagamento;
    }

    public async Task RealizarPagamento(Pagamento pagamento)
    {
        if (pagamento is null) throw new ArgumentException(nameof(pagamento));

        _context.Pagamento.Add(pagamento);

        await _context.SaveChangesAsync();
    }
}
