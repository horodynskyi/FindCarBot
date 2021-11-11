using System;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace FindCarBot.WEB
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAutoRiaService, AutoRiaService>();
            services.AddHttpClient();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "FindCarBot.WEB", Version = "v1"});
            });
            services
                .AddScoped<ICommandService, CommandService>()
                .AddTelegramBotClient(_configuration)
                .AddControllers()
                .AddNewtonsoftJson(options => 
                {
                    options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
           
            app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FindCarBot v1"));
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}