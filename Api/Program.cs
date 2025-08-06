using ExternalServices;
using Persistence;
using Application;


var builder = WebApplication.CreateBuilder(args);

// Inject dependencies
var persistenceConfiguration = builder.Configuration.GetSection("Persistence");
var externalSerivcesConfiguration = builder.Configuration.GetSection("ExternalSerivces");
var applicationConfiguration = builder.Configuration.GetSection("Application");
builder.Services.AddPersistence(persistenceConfiguration);
builder.Services.AddExternalServices(externalSerivcesConfiguration);
builder.Services.AddAplication(applicationConfiguration);

//
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Create DB if not exists
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SqlServerDbContext>();
    db.Database.EnsureCreated();
}

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
