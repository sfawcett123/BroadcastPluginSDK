# Test Plugin



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
