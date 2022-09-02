using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCommandBase
{
    private string commandId;
    private string commandDescription;
    private string commandFormat;

    public string CommandId => commandId;
    public string CommandDescription => commandDescription;
    public string CommandFormat => commandFormat;

    public CheatCommandBase(string id, string description, string format)
    {
        commandId = id;
        commandDescription = description;
        commandFormat = format;
    }
}

public class CheatCommand : CheatCommandBase
{
    private MonoBehaviour behaviour;
    private System.Reflection.MethodInfo method;
    public CheatCommand(string id, string description, string format, MonoBehaviour behaviour, System.Reflection.MethodInfo method) : base(id, description, format)
    {
        this.behaviour = behaviour;
        this.method = method;
    }

    public void Invoke()
    {
        method.Invoke(behaviour, null);
    }
}

public class CheatCommand<T> : CheatCommandBase
{
    private System.Action<T> command;
    public CheatCommand(string id, string description, string format, System.Action<T> command) : base(id, description, format)
    {
        this.command = command;
    }

    public void Invoke(T value)
    {
        command.Invoke(value);
    }
}