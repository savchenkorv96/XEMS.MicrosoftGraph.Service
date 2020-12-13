using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

namespace XEMS.MicrosoftGraph.Service.AppStart.ConfigureServices
{
    /// <summary>
    /// </summary>
    public static class ConfigureServicesControllers
    {
        /// <summary>
        ///     Configure services
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.Converters.Add(new StringEnumConverter()));
        }
    }
}