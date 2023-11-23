using AppFrigoNonna.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace AppFrigoNonna
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // CODICE POI DA RIMUOVERE COME DA ISTRUZIONI SLIDE PER AUTHENTICATION
            // var connectionString = builder.Configuration.GetConnectionString("FridgeProdContextConnection") ?? throw new InvalidOperationException("Connection string 'FridgeProdContextConnection' not found.");

            // PER AUTH
            // PRECEDENTEMENTE DA LEVARE, per test funzionamento, invece, la modifico
            builder.Services.AddDbContext<FridgeProdContext>();
            // contenuto delle parentesi da LEVARE di FridgeProdContext 'options =>options.UseSqlServer(connectionString)' lasciare solo le () parentesi


            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<FridgeProdContext>();




            // Add services to the container.
            builder.Services.AddControllersWithViews();


            // Codice aggiunto per evitare che la chiamata Api, vada il loop
            // Codice di configurazione per il serializzatore Json
            // in modo che ignori completamente le dipendenze cicliche di eventuali relazioni N:N o 1:N
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //DEPENDECY INJECTION
            builder.Services.AddScoped<FridgeProdContext, FridgeProdContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            // DA AGGIUNGERE PER AUTH
            app.UseAuthentication();
            app.MapRazorPages();



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=FridgeProd}/{action=Index}/{id?}");

            app.MapRazorPages();
            // ^^^^^^^^ AGGIUNGERE per il routing di bottoni e link Razor

            app.Run();
        }
    }
}