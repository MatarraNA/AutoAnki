using AutoAnki.Core.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnki.Core.Engine.EngineStates
{
    public class CheckAnkiState : AnkiBaseState
    {
        public CheckAnkiState(AnkiStateMachine machine, MainForm ankiForm) : base(machine, ankiForm) { }

        public override void Enter()
        {
            base.Enter();

            AnkiForm.StatusText = "Attempting to connect to AnkiConnect...";
        }

        public override void Update()
        {
            base.Update();

            // poll anki API to see if its connected
            if (AnkiAPI.GetVersion() > 0)
            {
                Machine.ChangeState(new RecordingAnkiState(Machine, AnkiForm));
                return;
            }
        }
    }
}
