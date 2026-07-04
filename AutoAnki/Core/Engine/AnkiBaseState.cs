using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnki.Core.Engine
{
    public class AnkiBaseState
    {
        protected readonly AnkiStateMachine Machine;
        protected readonly MainForm AnkiForm;

        /// <summary>
        /// How long this state has been running for, in MS
        /// </summary>
        protected long StateUptimeMS;

        protected AnkiBaseState(AnkiStateMachine machine, MainForm ankiForm)
        {
            Machine = machine;
            AnkiForm = ankiForm;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { StateUptimeMS += AnkiStateMachine.UPDATE_TICK_RATE_MS; }
    }
}
