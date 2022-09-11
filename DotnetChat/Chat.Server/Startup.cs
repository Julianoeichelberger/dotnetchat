﻿using Chat.Server.Adapters;
using Chat.Server.Adapters.Database;
using Chat.Server.Adapters.Database.Services;
using Chat.Server.Ports.Database.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Chat.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MessageContext>(options => options.UseInMemoryDatabase("Messages"));
            services.AddScoped<IMessageService, MessageService>();
            services.AddControllersWithViews();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            app.UseStaticFiles();
            app.UseBlazorFrameworkFiles();  

            app.UseRouting(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<HubAdapter>(Shared.ChatClient._hubresource);

                endpoints.MapFallbackToFile("index.html");   
            });
        }

    }
}
