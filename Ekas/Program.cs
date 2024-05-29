using Radzen;
using Ekas.Monitoring.Components;
using Ekas.Monitoring.Services;
using Ekas.Common;
using Ekas.Monitoring;
using Ekas.Monitoring.Repository;
using Telegram.Bot;
using Ekas.Monitoring.Handlers;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
      .AddInteractiveServerComponents().AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024);

builder.Services.AddControllers();
builder.Services.AddRadzenComponents();
builder.Services.AddHttpClient();

builder.Services.AddTransient((sp) => new KafkaConsumer(AppData.BootstrapServers, AppData.GroupId));
builder.Services.AddTransient<UpdateHandler>();
builder.Services.AddTransient<TgNotificationService>();
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient("7173430096:AAHdyp64rkP8gSi4i5tOGMZ67nCP1zFQSxs"));
builder.Services.AddHostedService<KafkaHostedService>();
builder.Services.AddHostedService<TelegramHostedService>();
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<IncidentRepository>();
builder.Services.AddTransient<CommentRepository>();
builder.Services.AddTransient<ProblemService>();
builder.Services.AddTransient<IncidentService>();
builder.Services.AddTransient<CommentService>();
//singleton/transient/scoped

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
