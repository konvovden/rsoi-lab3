using Microsoft.EntityFrameworkCore;
using RentalService.Core.Services;
using RentalService.Database.Context;
using RentalService.Server.GrpcServices;
using RentalService.Server.Interceptors;

namespace RentalService.Server;

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
        
        services.AddDbContext<RentalServiceContext>(opt => 
                    opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IRentalService, RentalService.Services.RentalService>();
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
            endpoints.MapGrpcService<RentalApiService>();
        });
    }
}