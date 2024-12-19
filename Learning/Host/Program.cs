var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.API>("api");

var nextjs = builder.AddNpmApp("nextjs", "../next-js-project")
	.WithReference(api)
	.WaitFor(api)
	.WithHttpEndpoint(3000, 3001, isProxied: true)
	.WithExternalHttpEndpoints();

builder.Build().Run();