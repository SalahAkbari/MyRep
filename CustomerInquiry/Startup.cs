using CustomerInquiry.DataAccess;
using CustomerInquiry.DataAccess.DbContext;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddScoped(typeof(IGenericEFRepository<>), typeof(GenericEFRepository<>));

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Transaction, TransactionDTO>();
                config.CreateMap<TransactionDTO, Transaction>();
                config.CreateMap<Customer, CustomerDTO>();
                config.CreateMap<CustomerDTO, Customer>();
            });

            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Customer Inquiry API", Version = "v1" });
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
