var builder = DistributedApplication.CreateBuilder(args);

// Add a service that provides an API.
var apiService = builder.AddProject<Projects.BoilerPlate_ApiService>("apiservice");

// Add a worker that processes messages from a queue.
//builder.AddProject<Projects.BoilerPlate_Worker>("worker");

// Add a web frontend that uses the API service.
//builder.AddProject<Projects.BoilerPlate_Web>("webfrontend")
//    .WithExternalHttpEndpoints()
//    .WithReference(apiService)
//    .WaitFor(apiService);


builder.Build().Run();
