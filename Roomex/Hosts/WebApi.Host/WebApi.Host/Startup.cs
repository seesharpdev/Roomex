namespace WebApi.Host
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Versioning;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.OpenApi.Models;
	using Roomex.Core.Interfaces.Services;
	using Roomex.Core.Services;
	using System;
	using System.IO;
	using System.Reflection;

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
			services.AddScoped<IGeoLocationService, GeoLocationService>();
			services.AddControllers();
			services.AddApiVersioning(config =>
				{
					config.AssumeDefaultVersionWhenUnspecified = true;
					config.DefaultApiVersion = new ApiVersion(1, 0);
					config.ReportApiVersions = true;
					config.ApiVersionReader = new HeaderApiVersionReader("api-version");
				});

			services.AddSwaggerGen(c =>
				{
					c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi.Host", Version = "v1" });
					var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
					var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
					c.IncludeXmlComments(xmlPath);
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi.Host v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
