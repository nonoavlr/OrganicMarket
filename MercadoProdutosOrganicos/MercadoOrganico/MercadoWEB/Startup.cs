using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using MercadoInfrastructure;
using MercadoPersistency;
using MercadoApplication;
using MercadoCore;

namespace MercadoWeb
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
            services.AddDbContext<MercadoDbContext>(options =>
            {
                options.UseSqlite("Filename=Mercado.db", opt =>
                {
                    opt.MigrationsAssembly(typeof(MercadoDbContext).Assembly.FullName
                    );
                });
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MercadoDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddScoped<IEntityCrudHandler<Comprador>>(
                serviceProvider => new CompradorHandler(
                    serviceProvider.GetService<MercadoDbContext>()
                 )
            );

            services.AddScoped<IEntityCrudHandler<Contato>>(
                serviceProvider => new ContatoHandler(
                    serviceProvider.GetService<MercadoDbContext>()
                 )
            );

            services.AddScoped<IEntityCrudHandler<Endereco>>(
                serviceProvider => new EnderecoHandler(
                    serviceProvider.GetService<MercadoDbContext>()
                 )
            );

            services.AddScoped<IEntityCrudHandler<ItemPedido>>(
                serviceProvider => new ItemPedidoHandler(
                    serviceProvider.GetService<MercadoDbContext>()
                 )
            );

            services.AddScoped<IEntityCrudHandler<Pedido>>(
                serviceProvider => new PedidoHandler(
                    serviceProvider.GetService<MercadoDbContext>()
                 )
            );

            services.AddScoped<IEntityCrudHandler<Produto>>(
                serviceProvider => new ProdutoHandler(
                    serviceProvider.GetService<MercadoDbContext>()
                 )
            );

            services.AddScoped<IEntityCrudHandler<Produtor>>(
                serviceProvider => new ProdutorHandler(
                    serviceProvider.GetService<MercadoDbContext>()
                 )
            );

            services.AddScoped<IEntityCrudHandler<TipoProduto>>(
                serviceProvider => new TipoProdutoHandler(
                    serviceProvider.GetService<MercadoDbContext>()
                 )
            );

            services.AddScoped<IEntityCrudHandler<TipoQuantidade>>(
                serviceProvider => new TipoQuantidadeHandler(
                    serviceProvider.GetService<MercadoDbContext>()
                 )
            );

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
