using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poke_npcs_cl.Config
{
    public class GetConfig : BaseScript
    {
        public static JObject Config = new JObject();
        public static bool configLoaded = false;

        public GetConfig()
        {
            EventHandlers[$"{API.GetCurrentResourceName()}:SendConfig"] += new Action<string>(LoadDefaultConfig);
            TriggerServerEvent($"{API.GetCurrentResourceName()}:getConfig");
        }

        private void LoadDefaultConfig(string dc)
        {
            Config = JObject.Parse(dc);
            configLoaded = true;
            Poke_npcs_cl_Init.InitNpcs();
        }
    }
}
