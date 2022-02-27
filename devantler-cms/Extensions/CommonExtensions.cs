namespace devantler.cms.Extensions;

public static class CommonExtensions
{
    public static void UseDeveloperExceptionPageSimplified(this WebApplication app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
            app.UseDeveloperExceptionPage();
    }
}
