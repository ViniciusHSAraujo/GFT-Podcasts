using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFT_Podcasts.Database;
using GFT_Podcasts.Models;
using GFT_Podcasts.Repositories;
using GFT_Podcasts.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GFT_Podcasts {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<ApplicationDbContext>
                (options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IPodcastRepository, PodcastRepository>();
            services.AddTransient<IEpisodioRepository, EpisodioRepository>();

            services.AddResponseCompression();

            services.AddSwaggerGen
            (c => {
                c.SwaggerDoc
                ("v1", new OpenApiInfo {
                    Version = "v1", Title = "GFT Podcast", Description = "API de gerenciamento de podcasts.",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseResponseCompression();


            app.UseSwagger();

            app.UseSwaggerUI
            (c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GFT Podcast");
            });

            app.UseEndpoints
            (endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}