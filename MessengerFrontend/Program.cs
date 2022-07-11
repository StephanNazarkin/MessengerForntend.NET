using MessengerFrontend.Services;
using MessengerFrontend.Services.Interfaces;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IAccountServiceAPI, AccountServiceAPI>();
builder.Services.AddTransient<IChatServiceAPI, ChatServiceAPI>();
builder.Services.AddTransient<IMessageServiceAPI, MessageServiceAPI>();

builder.Services.AddHttpClient("Messenger", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:44309/");
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik1haW5Vc2VyIiwibmFtZWlkIjoiOWFhMzRhYzMtNzFiMS00NGQyLTllYjYtN2NkYjRiNTEyN2MwIiwibmJmIjoxNjU3NDYwODczLCJleHAiOjE2NTgwNjU2NzMsImlhdCI6MTY1NzQ2MDg3M30.Vb1_EjtYPETfJZnbNE3UUsonbkrYftqWyA1bPuWIbsA";
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
