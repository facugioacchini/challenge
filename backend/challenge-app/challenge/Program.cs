var builder = WebApplication.CreateBuilder(args);

var Policy = "MyPolicy";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Policy,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Policy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
