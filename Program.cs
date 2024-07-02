var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/api/hello", (HttpContext context) =>
{
    string visitor = context.Request.Query.ContainsKey("visitor_name") ?
     context.Request.Query["visitor_name"].ToString() : "Anonymous";
    var response = new {
        Client_ip = context.Connection.RemoteIpAddress?.ToString(), 
        Location = "Lagos",
        Greeting = $"Hello {visitor}! the temperature is 11 degree Celscius in Lagos",
        };
        return Results.Ok(response);
})
.WithName("Greetings")
.WithOpenApi();

app.Run();

