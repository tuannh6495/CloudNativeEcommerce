var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var db = builder.AddSqlServer("sqlserver").AddDatabase("ecommerce");
var messageQueue = builder.AddRabbitMQ("messaging");

var apiService = builder.AddProject<Projects.CloudNativeEcommerce_ApiService>("apiservice")
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.CloudNativeEcommerce_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);


builder.Build().Run();
