using ChatRoom.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// برا razor
//builder.Services.AddRazorPages();

// برا mvc
builder.Services.AddControllersWithViews();


//
builder.Services.AddSignalR();



//   با احراز هویت  و کوکی



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Account/Login/";
     //   options.SlidingExpiration=true;
      
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();




//   با احراز هویت  و کوکی
app.UseAuthentication();


//   با  دسترسی  
app.UseAuthorization();




app.UseRouting();

app.UseAuthorization();
// برا razor
//app.MapRazorPages();


// برا mvc
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chatHub");

app.Run();
