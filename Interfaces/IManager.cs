using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BroadcastPluginSDK.Interfaces
{
    public interface IManager
    {
        public event EventHandler<bool> TriggerRestart;
    }
}
