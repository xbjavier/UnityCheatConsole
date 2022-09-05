using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text;
using TMPro;

namespace UnityCheatConsole
{
    public class CheatControllerUI : CheatControllerBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] TextMeshProUGUI previousCommands;
        [SerializeField] TMP_InputField inputCommand;
        [SerializeField] Button submitButton;

        [SerializeField] ThemeSO theme;

        StringBuilder listOfCommands = new StringBuilder();

        ColorThemePicker[] pickers;
        string errorColor;
        string inputColor;
        string commandSuccessColor;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (pickers == null)
                pickers = GetComponentsInChildren<ColorThemePicker>();

            for (int i = 0; i < pickers.Length; i++)
            {
                pickers[i].SetColor(theme);
            }

            inputColor = GetHexString(theme.CommandInput);
            errorColor = GetHexString(theme.CommandError);
            commandSuccessColor = GetHexString(theme.CommandExecuted);
            submitButton.onClick.AddListener(() => SubmitCommand());

        }

        
        public override void SubmitCommand()
        {

            string[] inputs = inputCommand.text.Split('#');

            string inputCmd = inputs[0] ?? null;
            string inputParams = inputs.Length > 1 ? inputs[1] : null;

            string[] commandSteps = inputCmd.Split('.');

            string command = commandSteps[0] ?? null;
            string instance = commandSteps.Length > 1 ? commandSteps[1] : null;

            string[] parameters = inputParams != null ? inputParams.Split(' ') : null;

            if (string.IsNullOrEmpty(command)) return;

            AppendInfo($"{command}");

            MethodInstance methodInstance = GetcommandInstances(command);
            if (methodInstance == null)
            {
                AppendError($"Command not found");
            }
            else
            {
                CheatController.RunCheat(command, parameters, instance);
                AppendSuccess("Completed!");
            }

            previousCommands.text = listOfCommands.ToString();
            inputCommand.text = string.Empty;
            inputCommand.Select();
        }

        protected void AppendError(string msg)
        {
            listOfCommands.AppendLine($"<color=#{errorColor}>{msg}<color=#{inputColor}>");
        }

        protected void AppendSuccess(string msg)
        {
            listOfCommands.AppendLine($"<color=#{commandSuccessColor}>{msg}<color=#{inputColor}>");
        }

        protected void AppendInfo(string msg)
        {
            listOfCommands.AppendLine($"<color=#{inputColor}>{msg}<color=#{inputColor}>");
        }

        protected string GetHexString(Color color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }

        [CheatCode("help", "Displays help for commands", "help")]
        protected override void CheatsHelp()
        {
            AppendInfo("############### CHEATS HELP ##################");
            foreach (var key in CheatController.Cheats.Keys)
            {
                listOfCommands.Append($"<color=#{commandSuccessColor}>{key} - {CheatController.Cheats[key].CheatCommand.CommandDescription} - {CheatController.Cheats[key].CheatCommand.CommandFormat}\n");
            }
            AppendInfo("##############################################");
            listOfCommands.Append($"<color=#{inputColor}>");
            previousCommands.text = listOfCommands.ToString();
        }

        protected override void Toggle()
        {
            base.Toggle();
            panel.SetActive(show);
        }
    }
}

