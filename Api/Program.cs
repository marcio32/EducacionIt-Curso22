using Api.Middlewares;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var MyAllowSpecificOrigin = "";
			var builder = WebApplication.CreateBuilder(args);
			ApplicationDbContext.ConnectionString = builder.Configuration.GetConnectionString("WebEducacionIt");
			// Add services to the container.
			builder.Services.AddCors(options =>
			{
				options.AddPolicy(name: MyAllowSpecificOrigin,
					options =>
					{
						options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
					});
			});
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition(
					"Bearer",
					new OpenApiSecurityScheme
					{
						Description = "Autorizacion",
						Name = "Autorizacion",
						BearerFormat = "JWT",
						In = ParameterLocation.Header,
						Type = SecuritySchemeType.Http,
						Scheme = "bearer"
					});

				options.AddSecurityRequirement(
					new OpenApiSecurityRequirement
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
							new string[]{}
						}
					});
			});


			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Firma"]))
				};
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseCors(MyAllowSpecificOrigin);

			app.UseMiddleware<ExceptionMiddleware>();

			app.MapControllers();

			app.Run();
		}
	}
}