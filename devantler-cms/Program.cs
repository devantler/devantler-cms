using devantler_cms.Setup;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
builder.Services.AddWebOptimizerSimplified(environment);
builder.Services.AddPiranhaSimplified(builder.Configuration, environment);

var app = builder.Build();
if (environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UsePiranhaSimplified();
app.UseWebOptimizer();
app.Run();