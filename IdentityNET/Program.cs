var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication", option =>
{
    option.Cookie.Name = "CookieAuthentication";
    option.LoginPath = "/Account/Login";
    option.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("AdminOnly", policy => policy.RequireClaim("Department", "Administrator"));

    option.AddPolicy("HRDept", policy => policy.RequireClaim("Department", "HR"));

    option.AddPolicy("HRManagerOnly", policy => policy.
         RequireClaim("Department", "HR")
        .RequireClaim("Department", "Manager")); 
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
