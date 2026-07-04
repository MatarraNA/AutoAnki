using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnki.Core.Engine
{
    public class AnkiStateMachine
    {
        private AnkiBaseState? _current;

        // vars
        public static readonly short UPDATE_TICK_RATE_MS = 100;
        private bool _running;
        public AnkiBaseState? CurrentBaseState => _current;

        public void Start()
        {
            if (_running) return;
            _running = true;

            Task.Run(async () =>
            {
                while (_running)
                {
                    _current?.Update();
                    await Task.Delay(UPDATE_TICK_RATE_MS);
                }
            });
        }

        public void Stop()
        {
            _running = false;
        }

        public void ChangeState(AnkiBaseState next)
        {
            _current?.Exit();
            _current = next;
            _current.Enter();
        }
    }
}
