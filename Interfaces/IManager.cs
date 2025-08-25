using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadcastPluginSDK.Interfaces
{
    public interface IManager
    {
        public event EventHandler<bool> TriggerRestart;
    }
}
