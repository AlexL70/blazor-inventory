using IMS.Plugins.InMemory;
using IMS.UseCases.Inventories;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.Products;
using IMS.UseCases.Products.Interfaces;
using IMS.UseCases.PluginInterfaces;
using IMS.WebApp.Components;
using IMS.UseCases.Activities.Interfaces;
using IMS.UseCases.Activities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

RegisterServices(builder.Services);

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

app.Run();

static void RegisterServices(IServiceCollection services)
{
    // Inventory Management Services
    services.AddSingleton<IInventoryRepository, InventoryRepository>();
    services.AddTransient<IViewInventoriesByNameUserCase, ViewInventoriesByNameUserCase>();
    services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
    services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();
    services.AddTransient<IGetInventoryByIdUseCase, GetInventoryByIdUseCase>();
    services.AddTransient<IDeleteInventoryUseCase, DeleteInventoryUseCase>();
    // Product Management Services
    services.AddSingleton<IProductRepository, ProductRepository>();
    services.AddTransient<IViewProductsByNameUseCase, ViewProductsByNameUseCase>();
    services.AddTransient<IAddProductUseCase, AddProductUseCase>();
    services.AddTransient<IEditProductUseCase, EditProductUseCase>();
    services.AddTransient<IGetProductByIdUseCase, GetProductByIdUseCase>();
    services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
    // Activities Services
    services.AddSingleton<IInventoryTransactionRepository, InventoryTransactionRepository>();
    services.AddSingleton<IProductTransactionRepository, ProductTransactionRepository>();
    services.AddTransient<IPurchaseInventoryUseCase, PurchaseInventoryUseCase>();
    services.AddTransient<IProduceProductUseCase, ProduceProductUseCase>();
}