using Microsoft.AspNetCore.Builder;

namespace XEMS.MicrosoftGraph.Service.AppStart.Configures
{
    public static class ConfigureEndpoints
    {
        /// <summary>
        ///     Configure Routing
        /// </summary>
        /// <param name="app"></param>
        public static void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseHsts();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}