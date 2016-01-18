packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Bootstrapper\Jal.Bootstrapper.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Bootstrapper.Nuget

packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Bootstrapper.AssemblyFinder\Jal.Bootstrapper.AssemblyFinder.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Bootstrapper.Nuget

packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Bootstrapper.AutoMapper\Jal.Bootstrapper.AutoMapper.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Bootstrapper.Nuget

packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Bootstrapper.CastleWindsor\Jal.Bootstrapper.CastleWindsor.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Bootstrapper.Nuget

packages\NuGet.CommandLine.2.8.6\tools\nuget pack Jal.Bootstrapper.Serilog.Sinks.Splunk\Jal.Bootstrapper.Serilog.Sinks.Splunk.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.Bootstrapper.Nuget

pause;