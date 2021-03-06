﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using RCForm.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using RCForm.API.Entities;

namespace RCForm.API
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Startup.Configuration["connectionStrings:relCharterDBConnectionString"];
            services.AddDbContext<RCFormContext>(o => o.UseSqlServer(connectionString));

#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddScoped<IMailService, CloudMailService>();
#endif
            services.AddScoped<IRCFormRepository, RCFormRepository>();
            services.AddMvc()
                .AddMvcOptions(o=> o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));

            //services.AddMvc()
            //    .AddJsonOptions(o =>
            //    {
            //        if (o.SerializerSettings.ContractResolver != null)
            //        {
            //            var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;

            //            castedResolver.NamingStrategy = null;
            //        }
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, RCFormContext rcContext)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            rcContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.City, Models.CityWithoutPOIDTO>();
                cfg.CreateMap<Entities.City, Models.CityDTO>();
                cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDTO>();
                cfg.CreateMap<Models.PointOfInterestForCreationDTO, Entities.PointOfInterest>();
                cfg.CreateMap<Models.PointOfInterestForUpdateDTO, Entities.PointOfInterest>();
                cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDTO>();
            });

            app.UseMvc();

        }
    }
}
