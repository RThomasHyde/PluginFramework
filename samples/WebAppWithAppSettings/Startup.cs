using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RThomasHyde.PluginFramework.Samples.Shared;
using RThomasHyde.PluginFramework.TypeFinding;

namespace WebAppWithAppSettings
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            TypeFinderOptions.Defaults.TypeFinderCriterias.Add(TypeFinderCriteriaBuilder.Create().Implements<IOperator>().Tag("MathOperator"));
            TypeFinderOptions.Defaults.TypeFinderCriterias.Add(TypeFinderCriteriaBuilder.Create().Tag("All"));
            services.AddPluginFramework();
            
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
