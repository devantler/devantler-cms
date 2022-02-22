using devantler_cms.Setup;
using Piranha;

namespace devantler_cms;

public class Startup
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _environment;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _config = configuration;
        _environment = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddWebOptimizer(pipeline =>
            pipeline.AddScssBundle("/css/style.css", "scss/style.scss"));
        services.AddPiranhaSimplified(_config, _environment);
    }

    public void Configure(IApplicationBuilder app, IApi api)
    {
        if (_environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseWebOptimizer();
        PiranhaSetup.Init(api);
        PiranhaSetup.ConfigureTinyMCE();
        app.UsePiranhaSimplified();
    }
}
