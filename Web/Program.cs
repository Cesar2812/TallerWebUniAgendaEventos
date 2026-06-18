using DotNetEnv;
using Web.Services.ServiceCategoria;

Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration["SERVER_API"] = Environment.GetEnvironmentVariable("SERVER_API");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IServiceCategoria, CategoriaServicio>();


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
