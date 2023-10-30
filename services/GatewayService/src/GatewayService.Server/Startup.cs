using GatewayService.CircuitBreaker;
using GatewayService.CircuitBreaker.Extensions;
using GatewayService.RetryQueue.Extensions;
using GatewayService.Server.Configuration;
using GatewayService.Server.Middlewares;
using Microsoft.OpenApi.Models;

namespace GatewayService.Server;

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
        services.AddControllers().AddNewtonsoftJson();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "GatewayService.Server", Version = "v1" });
        });
        services.AddSwaggerGenNewtonsoftSupport();

        var routesConfiguration = Configuration
            .GetRequiredSection(nameof(ServicesRoutesConfiguration))
            .Get<ServicesRoutesConfiguration>()!;
        
        services
            .AddGrpcClient<CarsService.Api.CarsService.CarsServiceClient>(o =>
            {
                o.Address = new Uri(routesConfiguration.CarsServiceUri);
                
            })
            .AddInterceptor<CircuitBreakerInterceptor>();

        services
            .AddGrpcClient<PaymentService.Api.PaymentService.PaymentServiceClient>(o =>
            {
                o.Address = new Uri(routesConfiguration.PaymentServiceUri);
            })
            .AddInterceptor<CircuitBreakerInterceptor>();

        services
            .AddGrpcClient<RentalService.Api.RentalService.RentalServiceClient>(o =>
            {
                o.Address = new Uri(routesConfiguration.RentalServiceUri);
            })
            .AddInterceptor<CircuitBreakerInterceptor>();

        services.AddCircuitBreaker(Configuration);
        services.AddRetryQueue(Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PersonService.Server v1"));
        
        app.UseHttpsRedirection();
        
        app.UseRouting();

        app.UseAuthorization();

        app.UseMiddleware<GrpcExceptionsHandleMiddleware>();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}