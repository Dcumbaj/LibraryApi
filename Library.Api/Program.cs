using Library.Api.DbContexts;
using Library.Api.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting the web host");

    var builder = WebApplication.CreateBuilder(args);

    //Read the configuration
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

    builder.Logging.Services.AddLogging();
    //Adding Connection string and ef core
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUserContactRepository, UserContactRepository>();
    builder.Services.AddScoped<IBookRepository, BookRepository>();

    var app = builder.Build();

    //Log the requests
    app.UseSerilogRequestLogging(configure =>
    {
        configure.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000}ms";
    }); // We want to log all HTTP requests

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexepctedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

return 0;