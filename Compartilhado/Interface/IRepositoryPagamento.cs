using Compartilhado.Models;

namespace Compartilhado.Interface;
public interface IRepositoryPagamento
{
    Task RealizarPagamento(Pagamento pagamento);
    Task<Pagamento> AtualizarPagamento(Pagamento pagamento);
}
