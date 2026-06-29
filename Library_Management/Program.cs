using Library_Management.DI;

// Configure the WebApplicationBuilder and services
var builder = WebApplication.CreateBuilder(args);
builder = Startup.BuildApplication(builder);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("LibraryManagmentPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
