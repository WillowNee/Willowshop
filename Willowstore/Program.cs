using Willowstore.BL.Auth;
using Willowstore.BL.Catalog;
using Willowstore.BL.General;
using Willowstore.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped <IAuth, Auth>();
builder.Services.AddSingleton<IEncrypt, Encrypt>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IDbSession, DbSession>();
builder.Services.AddScoped<IWebCookie, WebCookie>();
builder.Services.AddSingleton<IAuthor, Author>();
builder.Services.AddSingleton<IProduct, Product>();


builder.Services.AddSingleton<IAuthDAL, AuthDAL>();
builder.Services.AddSingleton<IDbSessionDAL, DbSessionDAL>();
builder.Services.AddSingleton<IUserTokenDAL, UserTokenDAL>();
builder.Services.AddSingleton<IProductDAL, ProductDAL>();
builder.Services.AddSingleton<IAuthorDAL, AuthorDAL>();
builder.Services.AddSingleton<IProductSearchDAL, ProductSearchDAL>();

builder.Services.AddMvc();


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

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

DbHelper.ConnString = app.Configuration["ConnectionStrings:Default"] ?? "";

app.Run();
