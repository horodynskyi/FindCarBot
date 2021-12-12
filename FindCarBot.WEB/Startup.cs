using System;
using FindCarBot.Domain.Abstractions;
using FindCarBot.Domain.Services;
using FindCarBot.IoC;
using FindCarBot.IoC.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;

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
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IHandleService, HandleService>();
            services.AddTransient<IConfigureResultService, ConfigureResultService>();
            services.AddTransient<ICallBackService, CallBackService>();
           
            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddStackExchangeRedisCache(opt =>
            {
               opt.Configuration = "localhost:6379";
                
            });
            var configuration = new ConfigurationOptions()
            {
                //for the redis pool so you can extent later if needed
                EndPoints = {  "localhost:6379" },
                AllowAdmin = true,
                //Password = "", //to the security for the production
            };
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration));
            services.Configure<AutoRiaOptions>(_configuration.GetSection(AutoRiaOptions.AutoRia));
          
            services.AddTelegramBotClient(_configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "FindCarBot.WEB", Version = "v1"});
            });
            services
                .AddScoped<ICommandService, CommandService>()
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