using Invoice.Plugins.Repository.Csv.Invoices;
using Invoice.Plugins.Repository.InMemory.Invoices;
using Invoice.UseCases.Invoices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Invoice.Api", Version = "v1" });
            });

            services.Configure<InvoiceCsvOptions>(Configuration.GetSection(InvoiceCsvOptions.CsvDataSource));

            //services.AddSingleton<IInvoiceRepository, InvoiceInMemoryRepository>();
            services.AddSingleton<IInvoiceRepository, InvoiceCsvRepository>();

            services.AddTransient<IGetInvoicesUseCase, GetInvoicesUseCase>();
            services.AddTransient<IGetInvoiceByNumberUseCase, GetInvoiceByNumberUseCase>();
            services.AddTransient<IAddInvoiceUseCase, AddInvoiceUseCase>();
            services.AddTransient<IEditInvoiceUseCase, EditInvoiceUseCase>();

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
                options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice.Api v1"));
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
