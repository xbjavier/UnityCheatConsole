using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class CheatControllerBehaviour : MonoBehaviour
{
    public KeyCode Action_Cheats;
    public KeyCode Action_EnterCheat;

    [Space(10)]
    [Header("Events")]
    public UnityEvent OnCommandSubmitted;

    protected virtual void OnEnable()
    {
        CheatController.AddCheatsFromBehaviour(this);
    }

    protected virtual void OnDisable()
    {
        CheatController.RemoveCheatBehaviour(this);
    }

    protected List<object> GetcommandInstances(string command)
    {
        if (string.IsNullOrEmpty(command) ||
            !CheatController.Cheats.ContainsKey(command))
            return null;

        return CheatController.Cheats[command];
    }

    [CheatCode("help", "Displays help for commands")]
    protected bool CheatsHelp()
    {
        return false;
    }
}
