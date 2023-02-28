using kitap_yazar.BLL.DBContext;
using kitap_yazar.BLL.Services.Kitap;
using kitap_yazar.BLL.Services.Yazar;
using kitap_yazar.BLL.Services.Helpers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
}); 

builder.Services.AddMvc();

builder.Services.AddScoped<IKitapService, KitapService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped(typeof(IPaginationService<>), typeof(PaginationService<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkNpgsql().AddKitapYazarDbContext();

var app = builder.Build();

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
