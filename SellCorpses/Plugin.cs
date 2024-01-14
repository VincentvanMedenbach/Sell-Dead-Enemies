using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using SellCorpses.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using ViniMod.Patches;
using System.IO;
using System.Reflection;
using LethalLib.Modules;

namespace SellCorpses
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class SellCorpsesBase : BaseUnityPlugin
    {
        private const string modGUID = "SellCorpsesMod";
        private const string modName = "Sell corpses mod";
        private const string modVersion = "1.0.0";
        private const string assetPackageName = "SellCorpses";
        //public static ConfigSettings configSettings = new ConfigSettings();
        private readonly Harmony harmony = new Harmony(modGUID);

        public static SellCorpsesBase Instance;
        public static ManualLogSource mls;
        public static Item deadHoardingBug;
        //[RuntimeInitializeOnLoadMethod]
        //internal static void InitializeRPCS_Landmine()
        //{
        //    NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("ViniMod-ClientExplodeRpc", HoardingBugPatch.ExplodeYipeeClientRpc);
        //    NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("ViniMod-ServerExplodeRpc", HoardingBugPatch.ExplodeYipeeServerRpc);
        //}
        void Awake() //Entrypoint!
        {
            
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogDebug("ViniMod has awoken!");

            //configSettings.LoadConfigs();
            mls.LogDebug("Config has awoken!");

            mls = Logger;

            string assetDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), assetPackageName);
            mls.LogDebug(File.Exists(assetDir));
            AssetBundle bundle = AssetBundle.LoadFromFile(assetDir);

             deadHoardingBug = bundle.LoadAsset<Item>("Assets/ViniStuffs/DeadHoarderBugItem.asset");
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(deadHoardingBug.spawnPrefab);
            deadHoardingBug.creditsWorth = 100;
            Utilities.FixMixerGroups(deadHoardingBug.spawnPrefab);
            Items.RegisterScrap(deadHoardingBug, 0, Levels.LevelTypes.None);

            harmony.PatchAll(typeof(SellCorpsesBase));
            harmony.PatchAll(typeof(RoundManagerPatch));
            ////harmony.PatchAll(typeof(FlowerManPatch)); TODO ADD LATER
            harmony.PatchAll(typeof(HoardingBugPatch));
            //harmony.PatchAll(typeof(CoilHeadPatch));



        }


    }
}
