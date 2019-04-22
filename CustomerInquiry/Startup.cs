using CustomerInquiry.DataAccess;
using CustomerInquiry.DataAccess.DbContext;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Domain.Entities;
using CustomerInquiry.Provider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace CustomerInquiry
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var conn = Configuration["connectionStrings:sqlConnection"];
            services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(conn, b => b.MigrationsAssembly(typeof(Startup).Assembly.FullName)));

            services.AddScoped(typeof(ICustomerProvider), typeof(CustomerProvider));
            services.AddScoped(typeof(ITransactionProvider), typeof(TransactionProvider));

            services.AddScoped(typeof(IGenericEfRepository<>), typeof(GenericEfRepository<>));

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Transaction, TransactionBaseDto>();
                config.CreateMap<TransactionBaseDto, Transaction>();
                config.CreateMap<Transaction, TransactionDto>();
                config.CreateMap<TransactionDto, Transaction>();
                config.CreateMap<Customer, CustomerBaseDto>();
                config.CreateMap<CustomerBaseDto, Customer>();
                config.CreateMap<Customer, CustomerDto>();
                config.CreateMap<CustomerDto, Customer>();
            });

            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Customer Inquiry API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Inquiry API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();

        }
    }
}
