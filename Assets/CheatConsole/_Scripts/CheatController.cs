using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;



public static class CheatController
{
    private static Dictionary<string, List<object>> cheats = new Dictionary<string, List<object>>();

    public static Dictionary<string, List<object>> Cheats => cheats;

    //TODO: Fix command format to display behaviour id.
    [System.Diagnostics.Conditional("ENABLE_CHEATS")]
    public static void AddCheatsFromBehaviour(MonoBehaviour behaviour)
    {
        var methods = GetMethods(behaviour);

        if (methods.Length == 0) return;

        for (int i = 0; i < methods.Length; i++)
        {
            var attributes = methods[i].GetCustomAttributes(typeof(CheatCode), false);
            for (int j = 0; j < attributes.Length; j++)
            {
                CustomAttributeData data = methods[i].CustomAttributes.ElementAt(j);
                if (data.AttributeType != typeof(CheatCode)) continue;

                string id = data.ConstructorArguments.ElementAt(0).Value.ToString();



                CheatCommand command = new CheatCommand(
                    id,
                    data.ConstructorArguments.ElementAt(1).Value.ToString(),
                    behaviour,
                    methods[i]);

                if (!cheats.ContainsKey(id))
                {
                    cheats.Add(id, new List<object>() { command });
                }
                else
                {
                    cheats[id].Add(command);
                }

            }
        }

    }

    [System.Diagnostics.Conditional("ENABLE_CHEATS")]

    public static void RemoveCheatBehaviour(MonoBehaviour behaviour)
    {
        var methods = GetMethods(behaviour);

        if (methods.Length == 0) return;

        for (int i = 0; i < methods.Length; i++)
        {
            var attributes = methods[i].GetCustomAttributes(typeof(CheatCode), false);
            for (int j = 0; j < attributes.Length; j++)
            {
                CustomAttributeData data = methods[i].CustomAttributes.ElementAt(j);
                if (data.AttributeType != typeof(CheatCode)) continue;

                string id = GenerateCheatId(behaviour, data);

                if (cheats.ContainsKey(id))
                {
                    cheats.Remove(id);
                }
            }
        }
    }

    [System.Diagnostics.Conditional("ENABLE_CHEATS")]
    public static void RunCheat(string cheatName, string arg = null)
    {

    }

    //[System.Diagnostics.Conditional("ENABLE_CHEATS")]
    //public static void RunCheat(string cheatName, string arg = null)
    //{
    //    if (!cheats.ContainsKey(cheatName)) return;

    //    if (string.IsNullOrEmpty(arg))
    //    {
    //        (cheats[cheatName] as CheatCommand).Invoke();
    //    }
    //    else
    //    {
    //        float valFloat = 0.0f;
    //        bool castToFloat = float.TryParse(arg, out valFloat);

    //        if (castToFloat)
    //        {
    //            (cheats[cheatName] as CheatCommand<float>).Invoke(valFloat);
    //            return;
    //        }

    //        int valInt = 0;
    //        bool castToInt = int.TryParse(arg, out valInt);
    //        if (castToInt)
    //        {
    //            (cheats[cheatName] as CheatCommand<int>).Invoke(valInt);
    //            return;
    //        }
    //    }
    //}

    //[System.Diagnostics.Conditional("ENABLE_CHEATS")]
    //public static void ShowObjectsThatCanRunCheat(string cheatName, out string[] gameObjects)
    //{
    //    gameObjects = cheats[cheatName].Select(c => (c as CheatCommandBase).GameObjectName).ToArray();
    //}


    private static string GenerateCheatId(MonoBehaviour behaviour, CustomAttributeData data)
    {
        return $"{data.ConstructorArguments.ElementAt(0).Value}.{behaviour.gameObject.GetInstanceID()}";
    }

    private static MethodInfo[] GetMethods(MonoBehaviour behaviour)
    {
        return behaviour.GetType().GetMethods(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static |
                BindingFlags.Default |
                BindingFlags.Instance)
               .Where(m => m.GetCustomAttributes(typeof(CheatCode), false).Length > 0)
               .ToArray();
    }
}

public class CheatCode : System.Attribute
{
    public CheatCode(string name, string description)
    {
    }

    public CheatCode(string name, string description, int arg)
    {
    }

    public CheatCode(string name, string description, float arg)
    {
    }

}

[System.Serializable]
public class Cheat
{
    private string id;
    private object command;

    public string Id => id;
    public object Command => command;
    public Cheat(string id, object command)
    {
        this.id = id;
        this.command = command;
    }
}



