using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCheatConsole
{
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

        public CheatCommandBase(string id, string description, string format)
        {
            commandId = id;
            commandDescription = description;
            commandFormat = format;
        }
    }

    public class CheatCommand : CheatCommandBase
    {

        private System.Reflection.MethodInfo method;
        public CheatCommand(string id, string description, string format, MonoBehaviour monoBehaviour, System.Reflection.MethodInfo method) : base(id, description, format)
        {
            this.method = method;
        }

        public void Invoke()
        {
            method.Invoke(CommandMonoBehaviour, null);
        }
    }
}
