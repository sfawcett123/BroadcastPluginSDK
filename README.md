# Test Plugin

![GitHub Release](https://img.shields.io/github/v/release/SteveFawcett/BroadcastPluginSDK?label=GitHub)
[![🚀 Publish NuGet Package](https://github.com/SteveFawcett/BroadcastPluginSDK/actions/workflows/publish.yml/badge.svg?branch=master)](https://github.com/SteveFawcett/BroadcastPluginSDK/actions/workflows/publish.yml)

## Creating a Plugin
Plugins are created by implementing the `IBroadcast` interface. This interface is defined in the `PluginBase` project, which is a dependency of this project.
To create a plugin, you need to create a class that implements the `IBroadcast` interface. Here is an example of how to do this:

- Create a new Windows Forms Control project. ensure it targets the same framework as the `PluginBase` project (e.g., .NET 8.0).
- Add a reference to the `PluginBase` project in your new project.
- Change the project file to include the `PluginBase` project as a reference, ensuring that it does not include runtime assets. This is necessary to avoid conflicts with the `IBroadcast` interface.
- Set the base output directory of your plugin project to the `Plugins` directory of the main project. This is where the main application will look for plugins.
- Inhert from `PluginBase.PluginControl` in your user control class.

```csharp
using Microsoft.Extensions.Configuration;
using PluginBase;

namespace MSFSPlugin
{
    public partial class UserControl1 : PluginBase.PluginControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

    }
}
```

### Plugin Base Project Reference
To overcome an error with ```typeof(IBroadcast).IsAssignableFrom(type)``` 
returning false the following entry needs to be made to your projects file.
```xml
  <ItemGroup>
    <ProjectReference Include="..\PluginBase\PluginBase.csproj">
        <Private>false</Private>
	    <ExcludeAssets>runtime</ExcludeAssets>
    </ProjectReference>
  </ItemGroup>
```

### Publishing the Plugin
The plugin is built as a ZIP file with all the components required to run the plugin.

To do this you need to edit the project file of your plugin project and add the following properties:
```xml

    <PropertyGroup>
        ....
        .... Existing properties
        ....
        <PackageDir Condition="'$(PackageDir)' == ''">$([System.IO.Path]::Combine($(OutputPath),'package'))/</PackageDir>
	    <PackagePath> c:\plugins\API.zip</PackagePath>
    </PropertyGroup>

	<Target Name="Package" DependsOnTargets="Publish">
		<MakeDir Directories="$(PackageDir)" />
		<ZipDirectory Overwrite="true" SourceDirectory="$(MSBuildProjectDirectory)/$(PublishDir)" DestinationFile="$(PackagePath)" />
	</Target>
	<Target Name="PackageClean" AfterTargets="Clean">
		<Delete Files="$(PackagePath)" />
	</Target>
  ```
  Now on your command line you can run the following command to build and package your plugin:
  ```bash
  dotnet publish /t:Package
  ```
  You can also add the configuration for example:
  ```bash 
    dotnet publish /t:Package -c Release
  ```

## Development

When changing the plugin SDK you may want to deliver the local (debug) version from a project dependancy and the (Release) version frp, the nuget package.

```
	<ItemGroup Condition="'$(Configuration)' == 'Debug'">
		<ProjectReference Include="..\BroadcastPluginSDK\BroadcastPluginSDK.csproj" />
	</ItemGroup>

	<ItemGroup Condition="'$(Configuration)' == 'Release'">
		<PackageReference Include="BroadcastPluginSDK" Version="1.0.4" />
	</ItemGroup>
 ```

 Then it is possible to build manually or via the GUI:

 ```
 dotnet build --configuration:Debug --target:Package -p:OutputDirectory=c:\Plugins\ -p:Version=0.0.3
 dotnet build --configuration:Release --target:Package -p:OutputDirectory=c:\Plugins\ -p:Version=0.0.3
 ```
