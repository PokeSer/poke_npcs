using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace poke_npcs_cl
{
    class ClearCaches : BaseScript
    {
        public ClearCaches()
        {
            EventHandlers["onResourceStop"] += new Action<string>(OnResourceStop);
        }

        private void OnResourceStop(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            Debug.WriteLine($"{resourceName} cleared blips and NPC's.");

            foreach (int npc in Poke_npcs_cl_Init._PedsCreate)
            {
                int _ped = npc;
                DeletePed(ref _ped);
            }
        }
    }
}
