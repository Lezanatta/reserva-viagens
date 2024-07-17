using Compartilhado;
using Compartilhado.Context;
using Compartilhado.Interface;

namespace Viagens.API;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        var dependencia = new Dependencia(services);

        var serviceProvider = dependencia.ObterServicos();

        if (serviceProvider is null) throw new InvalidOperationException("Provedor de servi�os n�o iniciado!");

        var serviceReserva = serviceProvider.GetService<IServiceReserva>() ??
            throw new InvalidOperationException("Servi�o n�o encontrado!");

        var repository = serviceProvider.GetService<IRepositoryReservas>() ??
            throw new InvalidOperationException("Servi�o n�o encontrado!");        
        
        var contexto = serviceProvider.GetService<ViagensContext>() ??
            throw new InvalidOperationException("Servi�o n�o encontrado!");

        services.AddSingleton(serviceReserva);
        services.AddSingleton(repository);
        services.AddSingleton(contexto);

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

#if DEBUG
#else
        app.UseHttpsRedirection();
#endif

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}