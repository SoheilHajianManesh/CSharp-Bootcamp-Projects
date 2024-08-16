using System.Reflection;
using System.Runtime.Loader;
using FinalProject.Common;
using Plugin.Common;

namespace FinalProject.Service;

public class PluginLoadContext : AssemblyLoadContext
{
    AssemblyDependencyResolver _resolver;

    public PluginLoadContext(string pluginPath, bool isCollectible)
        : base(name: Path.GetFileName(pluginPath), isCollectible: isCollectible)
    {
        _resolver = new AssemblyDependencyResolver(pluginPath);
    }

    protected override Assembly? Load(AssemblyName assemblyName)
    {
        if (assemblyName.Name == typeof(ILoyaltyClubPoint).Assembly.GetName().Name) 
            return null;
        if (assemblyName.Name == typeof(IDbContext).Assembly.GetName().Name)
            return null;
        string target = _resolver.ResolveAssemblyToPath(assemblyName);
        if (target != null)
            return LoadFromAssemblyPath(target);
        return null;
    }
    
    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        if (libraryPath != null)
            return LoadUnmanagedDllFromPath(libraryPath);
        return IntPtr.Zero;
    }
}