using Microsoft.EntityFrameworkCore;
using PaymentService.Core.Services;
using PaymentService.Database.Context;
using PaymentService.Server.GrpcServices;
using PaymentService.Server.Interceptors;

namespace PaymentService.Server;

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
        services.AddControllers();
        
        services.AddGrpc(options => options.Interceptors.Add<ExceptionsHandlingInterceptor>());
        
        services.AddDbContext<PaymentServiceContext>(opt => 
            opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPaymentService, Services.PaymentService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGrpcService<PaymentApiService>();
        });
    }
}