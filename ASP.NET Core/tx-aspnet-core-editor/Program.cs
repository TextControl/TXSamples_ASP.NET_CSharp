var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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


// serve static linked files (JavaScript and CSS for the editor)
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
       System.IO.Path.Combine(System.IO.Path.GetDirectoryName(
           System.Reflection.Assembly.GetEntryAssembly().Location),
           "TXTextControl.Web")),
    RequestPath = "/TXTextControl.Web"
});

// enable Web Sockets
app.UseWebSockets();

// attach the Text Control WebSocketHandler middleware
app.UseMiddleware<TXTextControl.Web.WebSocketMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
