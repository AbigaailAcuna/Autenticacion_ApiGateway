using LibrosApiAutenticacion.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();

builder.Services.AddDbContext<LibroContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// autenticación para API Key
app.Use(async (context, next) =>
{
    const string HeaderName = "Authorization";
    const string apiKey = "Api123456"; 

    if (context.Request.Headers.TryGetValue(HeaderName, out var extractedApiKey))
    {
        if (extractedApiKey == apiKey)
        {
           // context.Response.ContentType = "text/plain";
        //    context.Response.StatusCode = StatusCodes.Status200OK;
          //  await context.Response.WriteAsync("Autenticado");
            await next();
            return;
        }
        else
        {
            // La clave API es incorrecta
            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("No autenticado, La clave API es incorrecta, verifique sus datos");
            return;
        }
    }

    // No se proporcionó ninguna clave API
    context.Response.ContentType = "text/plain";
    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    await context.Response.WriteAsync("No autenticado, La clave API no fue proporcionada, intenta nuevamente");
});

app.UseRouting();
app.UseAuthorization();


await app.UseOcelot();


var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Ocelot is running on port {Port}", builder.Configuration["GlobalConfiguration:BaseUrl"]?.Split(':').Last());

app.Run();
