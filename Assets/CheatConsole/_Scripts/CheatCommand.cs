using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCommandBase
{
    private string commandId;
    private string commandDescription;
    private string commandFormat;
    private MonoBehaviour commandMonobehaviour;

    public string CommandId => commandId;
    public string CommandDescription => commandDescription;
    public string CommandFormat => commandFormat;

    public MonoBehaviour CommandMonoBehaviour => commandMonobehaviour;

    public string GameObjectName => CommandMonoBehaviour.gameObject.name;

    public CheatCommandBase(string id, string description)
    {
        commandId = id;
        commandDescription = description;
    }
}

public class CheatCommand : CheatCommandBase
{
    
    private System.Reflection.MethodInfo method;
    public CheatCommand(string id, string description, MonoBehaviour monoBehaviour, System.Reflection.MethodInfo method) : base(id, description)
    {
        this.method = method;
    }

    public void Invoke()
    {
        method.Invoke(CommandMonoBehaviour, null);
    }
}

//public class CheatCommand<T> : CheatCommandBase
//{
//    private System.Action<T> command;
//    public CheatCommand(string id, string description, string format, System.Action<T> command) : base(id, description, format)
//    {
//        this.command = command;
//    }

//    public void Invoke(T value)
//    {
//        command.Invoke(value);
//    }
//}