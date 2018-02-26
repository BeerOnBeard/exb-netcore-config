# Example of .NET Core Configuration Loading in ASP.NET and Console Apps

[Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/2.0.0) and [Microsoft.Extensions.Configuration.Json](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json/2.0.0) provide an easy way to load JSON configuration files into .NET Core applications. ASP.NET Core apps have these packages included by default and have some helpful paradigms that can be reused in Console apps. The projects in this repository showcase how to use environment variables to load configuration overrides in both types of applications.

The examples include the ability to build and run in both a VSCode environment and a Docker environment. See the section `Running the Examples` for more information.

## ASP.NET Core Apps

ASP.NET Core apps use the environment variable `ASPNETCORE_ENVIRONMENT` to optionally load configuration files using the structure `appsettings.{ASPNETCORE_ENVIRONMENT}.json` as overrides to the values stored in `appsettings.json`. If the file exists, the system will load the file at startup.

## .NET Core Console Apps

Console apps in .NET Core do not reference the [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/2.0.0) and [Microsoft.Extensions.Configuration.Json](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json/2.0.0) packages by default. However, they can easily be added to projects to allow these console apps to use these helpful frameworks to load JSON configuration files and use the same optional overrides as ASP.NET Core apps using an environment variable with a little bit of extra code.

Settings files are not automatically shipped with Console apps when published, like they are in ASP.NET Core apps. In order to publish settings files to the output directory, add the following to the project's `.csproj` file. It will copy any file that starts with `appsettings`, such as `appsettings.json` and `appsettings.Development.json`, to the publish directory.

```xml
<Project Sdk="Microsoft.NET.Sdk">
...
  <ItemGroup>
    <Content Include="appsettings*" CopyToPublishDirectory="PreserveNewest" />
  </ItemGroup>
...
</Project>
```

Add references to the packages [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/2.0.0) and [Microsoft.Extensions.Configuration.Json](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json/2.0.0) and add the configuration files `appsettings.json` and `appsettings.Development.json` to the project directory.

**NOTE:** Case is important in file names and **must** match the case of the environment variable value.

The next code snippet will first set the base directory to the current working directory of the app where the settings files reside. Then, it will load in the `appsettings.json` file. Then, it will load in the settings file that is named using the environment variable, if it exists. The `optional` parameter lets the system know that the file might not exists and that it's not a big deal. `.Build()` is called to generate the final `IConfiguration` instance that can be used in the application.

```csharp
  var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("BEERONBEARD_ENVIRONMENT")}.json", optional: true)
    .Build();
```

In order to reduce potential collisions, a unique environment variable should be used to signify what configuration override to use. In this example, `BEERONBEARD_ENVIRONMENT` is used. The value of `Development` will be set by the examples in this project.

## Running the Examples

The examples include support for running in both VSCode and Docker. Follow the instructions below for running in either environment.

### Running with VSCode

First, open the Debug panel on the right side of VSCode. At the top, select the application to run; `API Example App` or `Console Example App`. Click the start button or press `F5` to start debugging. The environment variables are set to `Development` in the `launch.json` configuration file in the `.vscode` folder.

The output of the Console app will be in the command line window where the app is running.

To see the configuration values from the API app, go to http://localhost:5000/configs in a browser. Modern browsers like Firefox will be able to render the JSON result. Otherwise, use CURL.

```bash
curl -i -H 'Accept: application/json' http://localhost:5000/configs
```

### Running with Docker

Each app includes a Dockerfile that already has the environment variable set to `Development` by default. To run the app, open a console and run the following. The commands assume the console is in the context of the root directory of this project.

To build and run the API project:

```bash
cd api
docker build -t api-example .
docker run -p 8080:80 api-example
```

To see the configuration values from the API application, go to http://localhost:8080/configs in a browser. Modern browsers like Firefox will be able to render the JSON result. Otherwise, use CURL.

```bash
curl -i -H 'Accept: application/json' http://localhost:8080/configs
```

To build and run the Console project:

```bash
cd console
docker build -t console-example .
docker run console-example
```

When run, the configuration values will be output to the command line.
