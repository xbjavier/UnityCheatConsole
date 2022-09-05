using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace UnityCheatConsole
{

    public class CheatControllerBehaviour : MonoBehaviour
    {
        public KeyCode Action_Cheats;
        public KeyCode Action_EnterCheat;

        protected bool show;

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

        protected virtual void Update()
        {
            ReadInput();
        }
        protected virtual void ReadInput()
        {
            if (Input.GetKeyDown(Action_Cheats))
            {
                Toggle();
            }
            else if (Input.GetKeyDown(Action_EnterCheat))
            {
                SubmitCommand();
            }
        }

        [System.Diagnostics.Conditional("ENABLE_CHEATS")]
        public virtual void SubmitCommand()
        {

        }

        protected MethodInstance GetcommandInstances(string command)
        {
            if (string.IsNullOrEmpty(command) ||
                !CheatController.Cheats.ContainsKey(command))
                return null;

            return CheatController.Cheats[command];
        }

        [CheatCode("help", "Displays help for commands", "help")]
        protected virtual void CheatsHelp()
        {
        }

        [System.Diagnostics.Conditional("ENABLE_CHEATS")]
        protected virtual void Toggle()
        {
            show = !show;
        }
    }
}