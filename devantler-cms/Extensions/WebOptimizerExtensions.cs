namespace devantler.cms.Extensions;
public static class WebOptimizerExtensions
{
    public static void AddWebOptimizerSimplified(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddWebOptimizer(
            environment,
            new CssBundlingSettings { Minify = environment.IsProduction() },
            new CodeBundlingSettings { Minify = environment.IsProduction() },
            pipeline => pipeline.AddScssBundle("/css/style.css", "scss/style.scss")
        );
    }
}