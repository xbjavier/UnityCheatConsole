using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Text;
using TMPro;

public class CheatControllerUI : CheatControllerBehaviour
{
    [SerializeField] TextMeshProUGUI previousCommands;
    [SerializeField] TMP_InputField inputCommand;

    [SerializeField] ThemeSO theme;

    StringBuilder listOfCommands = new StringBuilder();

    ColorThemePicker[] pickers;
    string errorColor;
    string inputColor;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (pickers == null)
            pickers = GetComponentsInChildren<ColorThemePicker>();

        for(int i = 0; i < pickers.Length; i++)
        {
            pickers[i].SetColor(theme);
        }

        inputColor = GetHexString(theme.CommandInput);
        errorColor = GetHexString(theme.CommandError);

    }

    //TODO: Display instances for command, if command includes . assume instance is second param.
    public void SubmitCommand()
    {
        if (string.IsNullOrEmpty(inputCommand.text)) return;

        List<object> commandInstances = GetcommandInstances(inputCommand.text);

        if (commandInstances == null || commandInstances.Count == 0)
        {
            listOfCommands.Append($"<color=#{errorColor}>Command not found\n<color=#{inputColor}>");
        }
        else
        {
            listOfCommands.Append($"{inputCommand.text}\n");
        }

        
        previousCommands.text = listOfCommands.ToString();
        inputCommand.text = string.Empty;
        OnCommandSubmitted?.Invoke();
    }

    protected string GetHexString(Color color)
    {
        return ColorUtility.ToHtmlStringRGBA(color);
    }
    
}
