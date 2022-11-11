using Microsoft.EntityFrameworkCore;
using Persistencia.Context;


var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AplicationDbContext>(options =>
                       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
            ServiceLifetime.Transient);


builder.Services.AddCors(opt => {
opt.AddPolicy(name: myAllowSpecificOrigins,
    builder => {
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });

});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
