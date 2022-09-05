using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityCheatConsole
{
    [CreateAssetMenu(fileName = "New Theme", menuName = "CheatConsole/Themes/New")]
    public class ThemeSO : ScriptableObject
    {
        public Color Background;
        public Color Primary;
        public Color Secondary;
        public Color CommandInput;
        public Color CommandPlaceHolder;
        public Color CommandOutput;
        public Color CommandError;
        public Color CommandExecuted;
    }
}
