# Jal.Bootstrapper

Just another library to bootstrap your libraries

## How to use?

Create your Bootstrapper class
```csharp
public class DoSomethingBootstrapper : IBootstrapper<bool>
{
    public void Run()
    {
        Result = true;
    }
    public bool Result { get; private set; }
}
```
Create an instance of your class and add it to the CompositeBootstrapper class
```csharp
var bootstrapper = new DoSomethingBootstrapper();

new CompositeBootstrapper().Add(bootstrapper)Run();
```	
Check the results of your Bootstrapper class looking the property Result
```csharp
var result = bootstrapper.Result;
```	
## Implementations

* CastleWindsor [![NuGet](https://img.shields.io/nuget/v/Jal.Bootstrapper.CastleWindsor.svg)](https://www.nuget.org/packages/Jal.Bootstrapper.CastleWindsor )
* AutoMapper [![NuGet](https://img.shields.io/nuget/v/Jal.Bootstrapper.AutoMapper.svg)](https://www.nuget.org/packages/Jal.Bootstrapper.AutoMapper )
* LightInject [![NuGet](https://img.shields.io/nuget/v/Jal.Bootstrapper.LightInject.svg)](https://www.nuget.org/packages/Jal.Bootstrapper.LightInject )
* Serilog.Sinks.Splunk [![NuGet](https://img.shields.io/nuget/v/Jal.Bootstrapper.Serilog.Sinks.Splunk.svg)](https://www.nuget.org/packages/Jal.Bootstrapper.Serilog.Sinks.Splunk )