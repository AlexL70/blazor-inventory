using IMS.Plugins.InMemory;
using IMS.Plugins.EFCoreSqlServer;
using IMS.UseCases.Inventories;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.Products;
using IMS.UseCases.Products.Interfaces;
using IMS.UseCases.PluginInterfaces;
using IMS.WebApp.Components;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.Activities;
using IMS.UseCases.Reports.Interfaces;
using IMS.UseCases.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using IMS.WebApp.Components.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configure DB context
builder.Services.AddDbContextFactory<IMSContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("InventoryManagement");
    options.UseSqlServer(connectionString);
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

RegisterRepositories(builder);
RegisterServices(builder.Services);

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IdentityRedirectManager>();

builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    .AddEntityFrameworkStores<IMSContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints(); ;

app.Run();

static void RegisterRepositories(WebApplicationBuilder builder)
{
    if (builder.Environment.IsEnvironment("Testing"))
    {
        StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
        Console.WriteLine("Using In-Memory Repositories for Testing");
        builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>();
        builder.Services.AddSingleton<IProductRepository, ProductRepository>();
        builder.Services.AddSingleton<IInventoryTransactionRepository, InventoryTransactionRepository>();
        builder.Services.AddSingleton<IProductTransactionRepository, ProductTransactionRepository>();
    }
    else
    {
        Console.WriteLine("Using EF Core SQL Server Repositories");
        builder.Services.AddTransient<IInventoryRepository, InventoryEFCoreRepository>();
        builder.Services.AddTransient<IProductRepository, ProductEFCoreRepository>();
        builder.Services.AddTransient<IInventoryTransactionRepository, InventoryTransactionEFCoreRepository>();
        builder.Services.AddTransient<IProductTransactionRepository, ProductTransactionEFCoreRepository>();
    }
}

static void RegisterServices(IServiceCollection services)
{
    // Inventory Management Services
    services.AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>();
    services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
    services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();
    services.AddTransient<IGetInventoryByIdUseCase, GetInventoryByIdUseCase>();
    services.AddTransient<IDeleteInventoryUseCase, DeleteInventoryUseCase>();
    // Product Management Services
    services.AddTransient<IViewProductsByNameUseCase, ViewProductsByNameUseCase>();
    services.AddTransient<IAddProductUseCase, AddProductUseCase>();
    services.AddTransient<IEditProductUseCase, EditProductUseCase>();
    services.AddTransient<IGetProductByIdUseCase, GetProductByIdUseCase>();
    services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
    // Activities Services
    services.AddTransient<IPurchaseInventoryUseCase, PurchaseInventoryUseCase>();
    services.AddTransient<IProduceProductUseCase, ProduceProductUseCase>();
    services.AddTransient<ISellProductUseCase, SellProductUseCase>();
    // Reports Services
    services.AddTransient<ISearchInventoryTransactionsUseCase, SearchInventoryTransactionsUseCase>();
    services.AddTransient<ISearchProductTransactionsUseCase, SearchProductTransactionsUseCase>();
}