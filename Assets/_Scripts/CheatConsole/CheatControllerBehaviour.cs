using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[CheatCode("test", "testd", "testf")]
public class CheatControllerBehaviour : MonoBehaviour
{
    bool showConsole;
    bool showHelp;
    string input;
    Vector2 scroll;

    public KeyCode Action_Cheats;
    public KeyCode Action_EnterCheat;

    public List<object> commandList;

    #region events
    UnityEvent<bool> OnToogle;
    #endregion events


    #region Commands
    public static CheatCommand HELP;
    #endregion Commands


    private void OnEnable()
    {
        CheatController.AddCheatsFromBehaviour(this);
    }

    private void OnDisable()
    {
        CheatController.RemoveCheatBehaviour(this);
    }
    private void Awake()
    {
        //Debug.Log("Test");
       // DebugController.SetupCheats(null);
    }

    [System.Diagnostics.Conditional("ENABLE_CHEATS")]
    private void Update()
    {
        if (Input.GetKeyDown(Action_Cheats))
        {
            ToggleDebug();
        }else if (Input.GetKeyDown(Action_EnterCheat))
        {
            HandleInput();
        }
    }
    public void ToggleDebug()
    {
        showConsole = !showConsole;
        if (showConsole)
        {
        }
        else
        {
            input = "";
        }

        OnToogle?.Invoke(showConsole);
    }

    [System.Diagnostics.Conditional("ENABLE_CHEATS")]
    private void OnGUI()
    {
        if (!showConsole) return;

        float y = 0.0f;

        GUIStyle textStyle = new GUIStyle();
        textStyle.normal.textColor = Color.green;
        textStyle.fontSize = 18;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * CheatController.Cheats.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            foreach (var (command, index) in CheatController.Cheats.Select((command, index) => (command, index)))
            {
                CheatCommandBase @base = (command.Value as CheatCommandBase);
                string label = $"{@base.CommandFormat} - {@base.CommandDescription}";
                Rect labelRect = new Rect(5, 20 * index, viewport.width - 100, 20);
                GUI.Label(labelRect, label, textStyle);
            }

          
            GUI.EndScrollView();
            y += 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);


        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 90f), input, textStyle);

    }

    [System.Diagnostics.Conditional("ENABLE_CHEATS")]
    private void HandleInput()
    {
        if (string.IsNullOrEmpty(input)) return;

        string[] commandProperties = input.Split(' ');

        string command = commandProperties[0];
        string arg = string.Empty;

        if(commandProperties.Length > 1) 
            arg = commandProperties[1];

        CheatController.RunCheat(command, arg);

    }

    List<Object> objs = new List<Object>();

    [CheatCode(name: "!help", description: "Show help for all cheats", commandFormat: "!help")]
    private void ShowHelp()
    {
        showHelp = true;      
    }

    void HideHelp()
    {
        showHelp = false;
    }
}
