using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XepaCommerce.src.repositorios;
using XepaCommerce.src.repositorios.implementacoes;
using XepaCommerce.src.servicos;
using XepaCommerce.src.servicos.implementacoes;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using XepaCommerce.src.data;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Microsoft.AspNetCore.Authentication.JwtBearer;

=======
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

>>>>>>> 9e99cbade8f543a2797ce567c7e4b4803e00a0e9
namespace XepaCommerce
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configuracao DB
            services.AddDbContext<XepaCommerceContexto>(opt => opt.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            #endregion

            //Repositorio
            services.AddScoped<IUsuario, UsuarioRepositorio>();
            services.AddScoped<IPedido, PedidoRepositorio>();
            services.AddScoped<IProduto, ProdutoRepositorio>();

            //Controladores
            services.AddCors();
            services.AddControllers();

             // Configuração de Serviços
            services.AddScoped<IAutenticacao, AutenticacaoServicos>();

            // Configuração do Token Autenticação JWTBearer
            var chave = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(b =>
            {
                b.RequireHttpsMetadata = false;
                b.SaveToken = true;
                b.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chave),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Configuração Swagger
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "XepaCommerce", Version = "v1" });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT authorization header utiliza: Bearer + JWT Token",
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, XepaCommerceContexto contexto)
        {
            if (env.IsDevelopment())
            {
                contexto.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "XepaCommerce v1"));
            }

            //ambiente de Produ��o Rotas
            app.UseRouting();
            app.UseCors(c => c
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
            );

             // Autenticação e Autorização
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
