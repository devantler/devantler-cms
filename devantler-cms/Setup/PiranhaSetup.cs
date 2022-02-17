using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Piranha;
using Piranha.AspNetCore.Identity.SQLServer;
using Piranha.AttributeBuilder;
using Piranha.Data.EF.SQLServer;
using Piranha.Manager.Editor;

namespace devantler_cms.Setup;
public static class PiranhaSetup
{
    public static void SetupPiranha(this IServiceCollection services, IConfiguration config)
    {
        services.AddPiranha(options =>
        {
            options.UseCms();
            options.UseManager();

            options.UseFileStorage(naming: Piranha.Local.FileStorageNaming.UniqueFolderNames);
            options.UseImageSharp();
            options.UseTinyMCE();
            options.UseMemoryCache();

            var connectionString = config.GetConnectionString("piranha_db");
            options.UseEF<SQLServerDb>(db => db.UseSqlServer(connectionString));
            options.UseIdentityWithSeed<IdentitySQLServerDb>(db => db.UseSqlServer(connectionString));

            /**
             * Here you can configure the different permissions
             * that you want to use for securing content in the
             * application.
            options.UseSecurity(o =>
            {
                o.UsePermission("WebUser", "Web User");
            });
             */

            /**
             * Here you can specify the login url for the front end
             * application. This does not affect the login url of
             * the manager interface.
            options.LoginUrl = "login";
             */
        });
    }

    internal static void SetupPiranhaMiddleware(this IApplicationBuilder app)
    {
        app.UsePiranha(options =>
        {
            options.UseManager();
            options.UseTinyMCE();
            options.UseIdentity();
        });
    }

    internal static void ConfigureTinyMCE()
    {
        EditorConfig.FromFile("editorconfig.json");
    }

    public static void SetupPiranhaApi(IApi api)
    {
        App.Init(api);

        new ContentTypeBuilder(api)
            .AddAssembly(typeof(PiranhaSetup).Assembly)
            .Build()
            .DeleteOrphans();
    }
}