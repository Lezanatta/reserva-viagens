using Compartilhado.Interface;
using Compartilhado.Models;

namespace Compartilhado.Services;

public class ServicePagamento(IRepositoryPagamento _repository) : IServicePagamento
{
    public async Task<Pagamento> AtualizarInformacoesPagamento(Pagamento pagamento) => await _repository.AtualizarPagamento(pagamento);

    public async Task RealizarNovoPagamento(Pagamento pagamento) => await _repository.RealizarPagamento(pagamento);
}
