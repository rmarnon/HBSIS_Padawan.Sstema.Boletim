using HBSIS_Padawan.Sistema.Boletim.BusinessRule;
using HBSIS_Padawan.Sistema.Boletim.BusinessRule.Interfaces;
using HBSIS_Padawan.Sistema.Boletim.Repositories.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HBSIS_Padawan.Sistema.Boletim.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "HBSIS_Padawan Sistema Boletim",
                    Version = "V1"
                });
            });

            services.AddDbContext<ApplicationContext>();

            services.AddTransient<IUser, UsuarioBusinessRule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseSwagger();
            app.UseSwaggerUI( s => 
            {
                s.SwaggerEndpoint("/swagger/V1/swagger.json", "HBSIS_Padawan Sistema Boletim");
                s.DocExpansion(DocExpansion.None);
            });
        }
    }
}
