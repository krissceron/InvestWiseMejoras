using InvestWiseProyecto.Data;
using InvestWiseProyecto.DataConnection;

using InvestWiseProyecto.Model;
using InvestWiseProyecto.Repository;
using InvestWiseProyecto.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUsuarioRepository, UsuarioConection>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<IRolRepository, RolConnection>();
builder.Services.AddScoped<RolService>();
builder.Services.AddScoped<IPropuestaRepository, PropuestaConnection>();
builder.Services.AddScoped<PropuestaService>();






// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NuevaPolitica");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
