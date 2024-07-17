using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Compartilhado.Interface;
using Compartilhado;
using Microsoft.Extensions.DependencyInjection;
using Compartilhado.Models;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PagamentoFn;

public class Function
{
    private readonly IServicePagamento _service;
    public Function()
    {
        var dependencia = new Dependencia(new ServiceCollection());

        var serviceProvider = dependencia.ObterServicos();

        _service = serviceProvider?.GetService<IServicePagamento>() ??
            throw new InvalidOperationException("Serviço não encontrado!");
    }

    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        if (evnt.Records.Count > 1) throw new InvalidOperationException("Somente uma mensagem pode ser tratada por vez!");

        var msg = evnt.Records.FirstOrDefault();

        if (msg is null) return;

        await ProcessMessageAsync(msg, context);
    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        context.Logger.LogInformation($"Processando mensagem: {message.Body}");

        var tipoPagamento = JsonConvert.DeserializeObject<OpcaoPagamento>(message.Body);

        if (tipoPagamento is null) throw new InvalidOperationException("reserva não preenchida corretamente!");

        context.Logger.LogInformation($"Objeto pagamento enum: {tipoPagamento.EnumPagamento}");
        context.Logger.LogInformation($"Objeto Pagamento: {tipoPagamento.Pagamento}");

        switch (tipoPagamento.EnumPagamento)
        {
            case EnumPagamento.Pagar:
                await RealizarPagamento(tipoPagamento.Pagamento, context);
                break;

            case EnumPagamento.AtualizarInfoPagamento:
                await AtualizarPagamento(tipoPagamento.Pagamento, context);
                break;

            default:
                throw new ArgumentException($"Opção de operação de pagamento {tipoPagamento.EnumPagamento} informada incorretamente ou não existe!");
        }
    }


    private async Task AtualizarPagamento(Pagamento pagamento, ILambdaContext context)
    {
        await _service.AtualizarInformacoesPagamento( pagamento );

        context.Logger.LogInformation($"Pagamento atualizado com sucesso!");
    }

    private async Task RealizarPagamento(Pagamento pagamento, ILambdaContext context)
    {
        await _service.RealizarNovoPagamento(pagamento);

        context.Logger.LogInformation($"Pagamento realizado com sucesso");
    }
}