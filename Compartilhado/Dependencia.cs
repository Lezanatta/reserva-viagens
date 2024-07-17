using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Compartilhado.Context;
using Compartilhado.Interface;
using Compartilhado.Repository;
using Compartilhado.Services;

namespace Compartilhado;
public class Dependencia(IServiceCollection services)
{
    public IServiceCollection? ServiceCollection { get; } = services;

    public IServiceProvider? ObterServicos()
    {
        var conf = GetConfiguration();

        var connectionString = conf.GetConnectionString("DefaultConnection");

        ServiceCollection?.AddDbContext<ViagensContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35))));
        ServiceCollection?.AddScoped<IRepositoryReservas, RepositoryReservas>();
        ServiceCollection?.AddScoped<IServiceReserva, ServiceReserva>();
        ServiceCollection?.AddScoped<IServicePagamento, ServicePagamento>();
        ServiceCollection?.AddScoped<IRepositoryPagamento, RepositoryPagamento>();

        return ServiceCollection?.BuildServiceProvider();
    }

    private static IConfiguration GetConfiguration()
    {
        string assemblyPath = Assembly.GetExecutingAssembly().Location;
        string projectDirectory = Path.GetDirectoryName(assemblyPath);

#if DEV
        var caminhoProjetoCompartilhado = Path.GetFullPath(Path.Combine(assemblyPath, "../../../../../Compartilhado"));

#else
        var caminhoProjetoCompartilhado = Path.GetFullPath(Path.Combine(projectDirectory));

#endif
        return new ConfigurationBuilder()
                .SetBasePath(caminhoProjetoCompartilhado)
                .AddJsonFile("config.json", optional: false, reloadOnChange: true)
                .Build();
    }
}
