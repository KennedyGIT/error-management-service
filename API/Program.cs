using API.Extensions;
using API.Middleware;
using infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseMiddleware<JWTMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");


app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<ErrorDataContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

app.Run();
