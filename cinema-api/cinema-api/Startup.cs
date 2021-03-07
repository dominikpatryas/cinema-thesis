using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using cinema_api.Data;
using cinema_api.Data.Interfaces;
using cinema_api.Data.Repositories;
using cinema_api.Helpers;
using cinema_api.Helpers.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace cinema_api
{
    public class Startup
    {
        readonly string myOrigins = "http://localhost:4201";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(x => x.UseSqlServer(connection));
            services.AddAuthorization();
            services.AddMvc();
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IMoviesRepository, MoviesRepository>();
            services.AddScoped<IHallsRepository, HallsRepository>();
            services.AddScoped<IShowsRepository, ShowsRepository>();
            services.AddScoped<ITicketsRepository, TicketsRepository>();
            services.AddScoped<IAuthorizer, Authorizer>();
            services.AddScoped<IReservationsRepository, ReservationsRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                                            .GetBytes(Configuration.GetSection("Token").Value)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };

                    });
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
                options => options.WithOrigins("http://localhost:4201").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
            );
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
