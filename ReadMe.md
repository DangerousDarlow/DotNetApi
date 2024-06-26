# Build Docker image

Open a terminal in the project root directory. Build a docker image and tag it as the latest version.

```
docker build -t dotnetapi:latest . 
```

# Run Docker container

Run the docker container and dependencies.

```
docker-compose up
```

# Run integration tests

Integration tests can be run against any instance of the `DotNetApi`. The default URL is defined in `DotNetApi.IntegrationTests.IntegrationTest` and has value `http://localhost:8080`. This is the default URL for the container started by `docker-compose up`. The default URL can be changed by modifying the class or by specifying test parameter `TargetUrl`.

The default URL for locally executing `DotNetApi` is `http://localhost:5000`.

Run tests passing the TargetUrl parameter

```
cd DotNetApi.IntegrationTests
dotnet test --no-build --% -- TestRunParameters.Parameter(name=\"TargetUrl\", value=\"http://localhost:5000\")
```