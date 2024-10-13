using EasyBillingReports2.BusinessLogic;
using EasyBillingReports2.Data;
using EasyBillingReports2.Data.Interfaces;
using EasyBillingReports2.Web.Components;
using Radzen;

namespace EasyBillingReports2.Web
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services
                .AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddRadzenComponents();

            builder.Services.AddSingleton<Project>();

            builder.Services.AddScoped<SettingsICalLocRepo>();
            builder.Services.AddScoped<SettingsGitHub>();
            builder.Services.AddScoped<ISettings>(serviceProvider =>
            {
                var project = serviceProvider.GetService<Project>();
                return project.Selected switch
                {
                    ProjectLst.Arexdata => serviceProvider.GetRequiredService<SettingsGitHub>(),
                    ProjectLst.IQVisio => serviceProvider.GetRequiredService<SettingsICalLocRepo>(),
                    _ => throw new NotImplementedException()
                };
            });

            builder.Services.AddScoped<PeriodLoaderICalLocRepo>();
            builder.Services.AddScoped<PeriodLoaderGitHub>();
            builder.Services.AddScoped<IPeriodLoader>(serviceProvider =>
            {
                var project = serviceProvider.GetService<Project>();
                return project.Selected switch
                {
                    ProjectLst.Arexdata => serviceProvider.GetRequiredService<PeriodLoaderGitHub>(),
                    ProjectLst.IQVisio => serviceProvider.GetRequiredService<PeriodLoaderICalLocRepo>(),
                    _ => throw new NotImplementedException()
                };
            });

            builder.Services.AddScoped<Billing>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}



