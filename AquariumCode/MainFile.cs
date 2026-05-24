using System.Reflection;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace Aquarium.AquariumCode;

[ModInitializer(nameof(Initialize) )]

public partial class MainFile : Node
{
   
    public const string ModId = "Aquarium"; //Used for resource filepath
    public const string ResPath = $"res://{ModId}";

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } =
        new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize()
    {
        Harmony harmony = new(ModId);
        Godot.Bridge.ScriptManagerBridge.LookupScriptsInAssembly(Assembly.GetExecutingAssembly()); 
        harmony.PatchAll();
    }
    
}
