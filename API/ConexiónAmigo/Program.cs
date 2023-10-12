using ConexiónAmigo;
using ConexiónAmigo.ChatHub;
using ConexiónAmigo.Model.Config;
using ConexiónAmigo.Model.JWTSetting;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddHttpClient("Api" , httpclient => {

    httpclient.BaseAddress = new Uri("http://localhost:46427/");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DataConfig>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<JwtSetter>(builder.Configuration.GetSection("JWT"));
Register.RegisterService(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
        builder.WithOrigins("http://localhost:61324", "http://localhost:4200")  // Allow requests from this specific origin
            .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials()
);

app.UseAuthorization();

app.MapControllers();
app.UseRouting();

var provider = new FileExtensionContentTypeProvider();
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider // this is not set by default
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MainHub>("/chatServer");
    endpoints.MapControllers();
});

app.Run();
