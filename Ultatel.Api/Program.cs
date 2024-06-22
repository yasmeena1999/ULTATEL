
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Security.Claims;
using System.Text;
using Ultatel.Api.Configurations;
using Ultatel.Api.Middlewares;
using Ultatel.Core.Entities;
using Ultatel.Core.Interfaces;
using Ultatel.Core.Profiles;
using Ultatel.Data.Data;
using Ultatel.Data.Repositories;
using Ultatel.Services.Services;

namespace Ultatel.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDatabase(builder.Configuration)
                            .AddCustomIdentity()
                            .AddJwtAuthentication(builder.Configuration)
                            .AddSwagger()
                            .AddApplicationServices();
            builder.Services.AddCors(
               options => options.AddPolicy(
                   "angularApp",
                   policy => policy.WithOrigins(builder.Configuration["AngularUrl"] ?? "http://localhost:4200/")
                .AllowAnyMethod()
                .SetIsOriginAllowed(policy => true)
                .AllowAnyHeader()
                .AllowCredentials()));

            builder.Services.AddControllers();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandlerMiddleware();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
