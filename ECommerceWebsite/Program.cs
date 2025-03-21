using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Persistence;
using Persistence.Repositories;
using Services;
using Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

string connectionString = builder.Configuration.GetConnectionString("DbConnection");
string dbName = builder.Configuration.GetConnectionString("DbName");
var settings = MongoClientSettings.FromConnectionString(connectionString);
settings.ServerApi = new ServerApi(ServerApiVersion.V1);
var client = new MongoClient(settings);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
	.AddMongoDbStores<ApplicationUser, ApplicationRole, ObjectId>(connectionString, dbName);
builder.Services.AddDbContext<RepositoryDbContext>(o => o.UseMongoDB(client, dbName));


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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
