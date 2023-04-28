using CamposDealerTest.Data;
using CamposDealerTest.Repository;
using CamposDealerTest.Repository.Interface;
using CamposDealerTest.Services;
using CamposDealerTest.Services.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CamposDealerTestContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CamposDealerTestContext") ?? throw new InvalidOperationException("Connection string 'CamposDealerTestContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IVendaService, VendaService>();
builder.Services.AddScoped<IVendaRepository, VendaRepository>();

builder.Services.AddHttpClient<IProdutoService, ProdutoService>("Produto", produto =>
{
    produto.BaseAddress = new Uri("https://camposdealer.dev/Sites/TesteAPI/");
});

builder.Services.AddHttpClient<IClienteService, ClienteService>("Cliente", produto =>
{
    produto.BaseAddress = new Uri("https://camposdealer.dev/Sites/TesteAPI/");
});

builder.Services.AddHttpClient<IVendaService, VendaService>("Vendas", produto =>
{
    produto.BaseAddress = new Uri("https://camposdealer.dev/Sites/TesteAPI/");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
