using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastPluginSDK
{
    public interface IPluginRegistry
    {
        void Add(IPlugin plugin);
        IReadOnlyList<IPlugin> GetAll();
        public IEnumerable<BroadcastCacheBase>? Caches();
        public IEnumerable<IProvider>? Providers();
        public ICache? MasterCache();
        public record PluginInfo(string Name, string Version, string FilePath, string Description, string RepositoryUrl);
    }

    public class PluginRegistry : IPluginRegistry
    {
        public record PluginInfo(string Name, string Version, string FilePath, string Description, string RepositoryUrl);
     //   public IEnumerable<PluginInfo> GetPluginInfo() =>
     //       _plugins.Select(p => new PluginInfo(p.Name, p.Version, p.FilePath, p.Description, p.RepositoryUrl));

        private readonly List<IPlugin> _plugins = new();
        public void Add(IPlugin plugin) => _plugins.Add(plugin);
        public IReadOnlyList<IPlugin> GetAll() => _plugins;
        public ICache? MasterCache()
        {
            var c = Caches()?.FirstOrDefault(c => c.Master);
            if (c != null)
            {
                return c;
            }

            Debug.WriteLine("No master cache found.");
            return null;
        }
        public IEnumerable<BroadcastCacheBase>? Caches()
        {
            foreach (var plugin in _plugins)
            {
                if (plugin is ICache c)
                {
                    yield return (BroadcastCacheBase)plugin;
                }
            }
        }
        public IEnumerable<IProvider>? Providers()
        {
            foreach (var plugin in _plugins)
                if (plugin is IProvider c)
                    yield return c;
        }
    }


}
