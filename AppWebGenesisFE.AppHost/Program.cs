var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.AppWebGenesisFE_ApiService>("apiservice");

builder.AddProject<Projects.AppWebGenesisFE_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
