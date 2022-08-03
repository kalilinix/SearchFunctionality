using SearchFunction.DataLayer;

var builder = WebApplication.CreateBuilder(args);

//Following lines included to resolve blank JQGrid rows issue
builder.Services.AddMvc()
.AddJsonOptions(x => {
    x.JsonSerializerOptions.PropertyNamingPolicy = null;
    x.JsonSerializerOptions.DictionaryKeyPolicy = null;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ICloud, Cloud>();
string connString = "Data Source =DESKTOP-B3BJGQV; Initial Catalog = Employee; Integrated Security = true";
//builder.Services.AddScoped<IDataRepository, DataService>(s => new DataService(connString));
builder.Services.AddSingleton<IDataRepository, DataService>(s => new DataService(connString));

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
