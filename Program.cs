using System;
using System.IO;
using Atena.Models;
using Atena.Services.Implementations;
using Atena.Services.Interfaces;
using DataBaseContext;
using Gtk;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PdfSharp.Fonts;

namespace Atena
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            string cn = "";
            var IsCaadi = config.GetSection("Env:IsCaadi").Get<bool>();
            
            if(IsCaadi)
                cn = config.GetConnectionString("Caadi");
            else
                cn = config.GetConnectionString("test");

           
            

            var host = Host
                .CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IAtenaReport, AtenaReport>();    
                    services.AddScoped<IFontResolver, AtlasFontResolverUbuntu>();

                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<SecondWindow>();
                    services.AddSingleton<AtenaGlobalConfigs>();
                    services.AddDbContext<CaadiDbContext>();
                    // services.AddDbContext<CaadiDbContext>((options) =>
                    // {
                    //     options.UseMySql(cn, ServerVersion.AutoDetect(cn));
                    // });
                })
                .Build();

           

            Application.Init();

            var SecondWindow = host.Services.GetRequiredService<SecondWindow>();
            var MainWindow = host.Services.GetRequiredService<MainWindow>();

            SecondWindow.ShowAll();
            SecondWindow.DeleteEvent += (sender, e) =>
            {
                if((bool)e.RetVal == true)
                {
                    SecondWindow.Hide();
                    MainWindow.ShowAll();
                }
            };

            // MainWindow.ShowAll();
            
            Application.Run();
        }
    }
}
