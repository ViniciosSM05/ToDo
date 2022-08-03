using DDDApi.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
serviceScope.StartDatabase();

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
