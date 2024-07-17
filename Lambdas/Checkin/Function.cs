using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Compartilhado;
using Compartilhado.Interface;
using Compartilhado.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CheckinFn;

public class Function
{
    private readonly IServiceReserva _service;
    public Function()
    {
        var dependencia = new Dependencia(new ServiceCollection());

        var serviceProvider = dependencia.ObterServicos();

        _service = serviceProvider?.GetService<IServiceReserva>() ??
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

        var reserva = JsonConvert.DeserializeObject<Reserva>(message.Body);

        if(reserva is null) throw new InvalidOperationException("reserva não preenchida corretamente!");

        await _service.RealizarCheckin(reserva);

        context.Logger.LogInformation($"Checkin realizado com sucesso!");
    }
}