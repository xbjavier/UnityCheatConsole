using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.ComponentModel;



public static class CheatController
{
    private static Dictionary<string, MethodInstance> cheats = new Dictionary<string, MethodInstance>();    

    public static Dictionary<string, MethodInstance> Cheats => cheats;

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
                //methods[i].GetBaseDefinition().GetParameters()[0].ParameterType

                CustomAttributeData data = methods[i].CustomAttributes.ElementAt(j);
                if (data.AttributeType != typeof(CheatCode)) continue;

                string id = data.ConstructorArguments.ElementAt(0).Value.ToString();



                CheatCommandBase command = new CheatCommandBase(
                    id,
                    data.ConstructorArguments.ElementAt(1).Value.ToString());

                if (!cheats.ContainsKey(id))
                {
                    cheats.Add(id, new MethodInstance(methods[i], command));
                }

                cheats[id].AddInstance(behaviour);
                
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
                BindingFlags.Instance |
                BindingFlags.FlattenHierarchy)
               .Where(m => m.GetCustomAttributes(typeof(CheatCode), false).Length > 0)
               .ToArray();
    }

    public static object[] ParamConverter(ParameterInfo[] required, string[] current)
    {
        object[] result = new object[required.Length];

        for (int i = 0; i < required.Length; i++)
        {
            TypeConverter typeConverter = TypeDescriptor.GetConverter(required[i].ParameterType);
            result[i] = typeConverter.ConvertFromString(current[i]);
        }
        return result;
    }

    public static void RunCheat(string cheat, string[] invokeParams, string instance)
    {
        if (string.IsNullOrEmpty(cheat)) return;

        MethodInfo methodInfo = cheats[cheat].MethodInfo;
        ParameterInfo[] parametersInfo = methodInfo.GetBaseDefinition().GetParameters();

        object[] parameters = ParamConverter(parametersInfo, invokeParams);


        if (string.IsNullOrEmpty(instance))
        {
            for (int i = 0; i < cheats[cheat].Instances.Count; i++)
            {
                cheats[cheat].MethodInfo.Invoke(cheats[cheat].Instances[i], parameters);
            }
        }
        else
        {
            for (int i = 0; i < cheats[cheat].Instances.Count; i++)
            {
                if (cheats[cheat].Instances[i].gameObject.name != instance)
                    continue;

                cheats[cheat].MethodInfo.Invoke(cheats[cheat].Instances[i], parameters);
                break;
            }
        }
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

public class MethodInstance
{
    private MethodInfo method;
    private CheatCommandBase cheatCommand;
    private List<MonoBehaviour> instances;

    public MethodInfo MethodInfo => method;
    public CheatCommandBase CheatCommand => cheatCommand;
    public List<MonoBehaviour> Instances => instances;

    public MethodInstance(MethodInfo method, CheatCommandBase cheatCommand)
    {
        this.method = method;
        this.instances = new List<MonoBehaviour>();
        this.cheatCommand = cheatCommand;
    }

    public void AddInstance(MonoBehaviour instance)
    {
        instances.Add(instance);
    }
}




