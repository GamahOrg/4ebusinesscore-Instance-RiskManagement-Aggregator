using _4EPlatform.PermissionsGroupingChecker;
using _4EPlatform.PremessionRegister;
using _4EPlatform_StrategicTelecom_Aggregator.GrpClientsConfig;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Logging
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog(logger);

// Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 7003, o => o.Protocols = HttpProtocols.Http2);
    options.Listen(IPAddress.Any, 5106, o => o.Protocols = HttpProtocols.Http1);
});

// Services
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc(options => { options.EnableDetailedErrors = true; });
builder.Services.AddGrpcReflection();
builder.Services.AddServices();
builder.Services.AddCorsConfig();
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddGrpcConfig(builder.Configuration);
builder.Services.AddSwaggerConfig();
builder.Services.AddLocalizationOptions();
builder.Services.AddVersioning();
builder.Services.AddRateLimitings(builder.Configuration);

// gRPC Clients
builder.Services.AddServiceOrgMemberGrpcClients(builder.Configuration["GrpcServiceSettings:orgUrl"]);
builder.Services.AddRiskAggGrpcClients(builder.Configuration["GrpcServiceSettings:RiskManagement"]);
builder.Services.AddControlPanelAggGrpcClients(builder.Configuration["GrpcServiceSettings:ControlPanelAggUrl"]);
builder.Services.AddUserManagementGrpcClients(builder.Configuration["GrpcServiceSettings:UserManagmentUrl"]);
builder.Services.AddUserManagementGrpcClient(builder.Configuration["GrpcServiceSettings:UserManagmentUrl"]);
builder.Services.AddServiceOperationsGrpcClient(builder.Configuration["GrpcServiceSettings:ServiceOperations"]);
builder.Services.AddTicketingSystemGrpcClients(builder.Configuration["GrpcServiceSettings:TicketingSystemUrl"]);
builder.Services.AddHostedService<AddPermissionsMiddleware>();
builder.Services.AddScoped<PermissionsGroupingCheckerMiddleware, PermissionsGroupingCheckerMiddleware>();
var app = builder.Build();

// Middleware
var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);
app.UseGrpcWeb();
app.UseCors("AllowCors");
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.UseMiddleware<PermissionsGroupingCheckerMiddleware>();
// Mapping
app.MapControllers();
app.MapGrpcReflectionService().EnableGrpcWeb().RequireCors("AllowCors"); // For Postman / gRPC testing
app.AddgRPCServices();          // Your custom gRPC service mapping

app.Run();
