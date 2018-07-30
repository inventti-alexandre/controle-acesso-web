using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleAcesso.Configurations;
using ControleAcesso.DAO;
using ControleAcesso.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ControleAcesso
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

            //Inicia o serviço de conexão com o banco de dados
            services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Habilitar serviços
            services.AddTransient<UsersDAO>();
            services.AddTransient<EventosDAO>();
            services.AddTransient<CursosDAO>();
            services.AddTransient<CursoPresencaDAO>();
            services.AddTransient<PessoaDAO>();
            services.AddTransient<TipoPresencaDAO>();
            services.AddTransient<StatusDAO>();

            //Iniciar configurações de autenticação com JWT 
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //Autenticação usando JWT para controlar o acesso à API
            .AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = false;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            })
            //Atenticação para usuários do site
            .AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Home/NaoAutorizado/";
                options.ReturnUrlParameter = "";
            })

            .AddCookie("admin", options =>
             {
                 options.LoginPath = "/Login";
                 options.AccessDeniedPath = "/Home/NaoAutorizado/";
                 options.ReturnUrlParameter = "";
             });

            services.AddCors();

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            //Deixar as urls minusculas
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:8080")
                       .WithOrigins("http://192.168.0.115:8100")
                       .AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                );

            app.UseMvc(routes =>
            {
                routes.MapRoute(
               name: "areas",
               template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
             );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=home}/{action=index}/{id?}");


            });
        }
    }
}
