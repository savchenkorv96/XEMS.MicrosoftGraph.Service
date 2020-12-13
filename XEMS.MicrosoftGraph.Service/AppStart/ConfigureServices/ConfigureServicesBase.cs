using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using Npgsql;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using XEMS.MicrosoftGraph.Service.Core.Activities;
using XEMS.MicrosoftGraph.Service.Core.Service;
using XEMS.MicrosoftGraph.Service.Core.UseCases;
using XEMS.MicrosoftGraph.Service.DataAccess.Contracts;
using XEMS.MicrosoftGraph.Service.DataAccess.Contracts.Provider;
using XEMS.MicrosoftGraph.Service.DataAccess.Provider;
using XEMS.MicrosoftGraph.Service.DataAccess.Repository;

namespace XEMS.MicrosoftGraph.Service.AppStart.ConfigureServices
{
    /// <summary>
    ///     ASP.NET Core services registration and configurations
    /// </summary>
    public static class ConfigureServicesBase
    {
        /// <summary>
        ///     ConfigureServices Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.Seq("http://seq:5341")
                .CreateLogger();
            services.AddTransient(x => logger);

            var provider = new PostgreSqlProvider(configuration["PostgreSQL:DefaultConnection"]);
            services.AddTransient<IDbProvider<NpgsqlConnection>>(x => provider);

            var authServiceRepository = new AuthServiceRepository(provider, logger);
            services.AddTransient<IAuthServiceRepository>(x => authServiceRepository);

            var authService = new AuthService(authServiceRepository, logger);
            services.AddTransient(x => authService);

            var confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(configuration["MicrosoftAPI:ApiID"])
                .WithTenantId(configuration["MicrosoftAPI:TenantId"])
                .WithClientSecret(configuration["MicrosoftAPI:ClientSecret"])
                .Build();
            var authenticationProvider = new ClientCredentialProvider(confidentialClientApplication);
            var graphServiceClient = new GraphServiceClient(authenticationProvider);
            services.AddTransient(x => graphServiceClient);

            var activityFactory = new ActivitiesFactory(graphServiceClient, logger);
            services.AddTransient(x => activityFactory);

            var useCaseFactory = new UseCaseFactory(activityFactory, logger);
            services.AddTransient(x => useCaseFactory);

            var userService = new UserService(useCaseFactory, logger);
            services.AddTransient(x => userService);

            var groupService = new GroupService(useCaseFactory, logger);
            services.AddTransient(x => groupService);

            var teamService = new TeamService(useCaseFactory, logger);
            services.AddTransient(x => teamService);
        }
    }
}