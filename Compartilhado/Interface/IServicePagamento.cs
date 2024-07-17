using Compartilhado.Models;

namespace Compartilhado.Interface;

public interface IServicePagamento
{
    Task RealizarNovoPagamento(Pagamento pagamento);
    Task<Pagamento> AtualizarInformacoesPagamento(Pagamento pagamento);
}
