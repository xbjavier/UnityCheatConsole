using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCheatConsole
{
    public enum CHEAT_CONSOLE_THEME_PICKER
    {
        BACKGROUND,
        PRIMARY,
        SECONDARY,
        COMMAND_INPUT,
        COMMAND_PLACEHOLDER,
        COMMAND_OUTPUT,
        COMMAND_ERROR,
        COMMAND_EXECUTED,
    }

    public class ColorThemePicker : MonoBehaviour
    {
        [SerializeField] Graphic graphic;
        [SerializeField] CHEAT_CONSOLE_THEME_PICKER type;

        public void SetColor(ThemeSO theme)
        {
            switch (type)
            {
                case CHEAT_CONSOLE_THEME_PICKER.BACKGROUND:
                    graphic.color = theme.Background;
                    break;
                case CHEAT_CONSOLE_THEME_PICKER.PRIMARY:
                    graphic.color = theme.Primary;
                    break;
                case CHEAT_CONSOLE_THEME_PICKER.SECONDARY:
                    graphic.color = theme.Secondary;
                    break;
                case CHEAT_CONSOLE_THEME_PICKER.COMMAND_INPUT:
                    graphic.color = theme.CommandInput;
                    break;
                case CHEAT_CONSOLE_THEME_PICKER.COMMAND_PLACEHOLDER:
                    graphic.color = theme.CommandPlaceHolder;
                    break;
                case CHEAT_CONSOLE_THEME_PICKER.COMMAND_OUTPUT:
                    graphic.color = theme.CommandOutput;
                    break;
                case CHEAT_CONSOLE_THEME_PICKER.COMMAND_ERROR:
                    graphic.color = theme.CommandError;
                    break;
                case CHEAT_CONSOLE_THEME_PICKER.COMMAND_EXECUTED:
                    graphic.color = theme.CommandExecuted;
                    break;
                default:
                    break;
            }
        }
    }
}
