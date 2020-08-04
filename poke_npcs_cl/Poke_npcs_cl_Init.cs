using CitizenFX.Core;
using CitizenFX.Core.Native;
using poke_npcs_cl.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poke_npcs_cl
{
    public class Poke_npcs_cl_Init : BaseScript
    {
        public static List<int> _PedsCreate = new List<int>();
        public static async Task InitNpcs()
        {
            await Delay(10000);
            foreach (var v in GetConfig.Config["Peds"])
            {
                uint model = (uint)API.GetHashKey(v["Hash"].ToString());
                float x = float.Parse(v["x"].ToString());
                float y = float.Parse(v["y"].ToString());
                float z = float.Parse(v["z"].ToString());
                float h = float.Parse(v["h"].ToString());

                await LoadModel(model);

                int _Peds = API.CreatePed(model, x, y, z, h, false, true, true, true);
                Function.Call((Hash)0x283978A15512B2FE, _Peds, true);
                API.SetEntityNoCollisionEntity(API.PlayerPedId(), _Peds, false);
                API.SetEntityCanBeDamaged(_Peds, false);
                API.SetEntityInvincible(_Peds, true);
                API.SetBlockingOfNonTemporaryEvents(_Peds, true);
                await Delay(1000);
                API.FreezeEntityPosition(_Peds, true);
                _PedsCreate.Add(_Peds);

                await Delay(200);
            }
        }

        public static async Task<bool> LoadModel(uint hash)
        {
            if (Function.Call<bool>(Hash.IS_MODEL_VALID, hash))
            {
                Function.Call(Hash.REQUEST_MODEL, hash);
                while (!Function.Call<bool>(Hash.HAS_MODEL_LOADED, hash))
                {
                    Debug.WriteLine($"Waiting for model {hash} load!");
                    await Delay(100);
                }
                return true;
            }
            else
            {
                Debug.WriteLine($"Model {hash} is not valid!");
                return false;
            }
        }
    }
}
