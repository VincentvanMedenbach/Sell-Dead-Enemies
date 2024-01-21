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
        private const string modVersion = "1.2";
        private const string assetPackageName = "SellCorpses";
        //public static ConfigSettings configSettings = new ConfigSettings();
        private readonly Harmony harmony = new Harmony(modGUID);

        public static SellCorpsesBase Instance;
        public static ManualLogSource mls;
        public static Item deadHoardingBug;
        public static Item deadSpider;
        public static Item deadCentipede;
        public static Item deadEyelessDog;
        public static Item deadThumper;
        public static Item deadNutcracker;
        public static Item deadBaboonHawk;


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
            deadBaboonHawk = bundle.LoadAsset<Item>("Assets/ViniStuffs/DeadBaboonHawkItem.asset");
            deadCentipede = bundle.LoadAsset<Item>("Assets/ViniStuffs/DeadCentipedeItem.asset");
            deadThumper = bundle.LoadAsset<Item>("Assets/ViniStuffs/DeadCrawlerItem.asset");
            deadEyelessDog = bundle.LoadAsset<Item>("Assets/ViniStuffs/DeadEyelessDogItem.asset");
            deadNutcracker = bundle.LoadAsset<Item>("Assets/ViniStuffs/DeadNutcrackerItem.asset");
            deadSpider = bundle.LoadAsset<Item>("Assets/ViniStuffs/DeadSandSpiderItem.asset");

            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(deadHoardingBug.spawnPrefab);
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(deadBaboonHawk.spawnPrefab);
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(deadCentipede.spawnPrefab);
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(deadThumper.spawnPrefab);
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(deadEyelessDog.spawnPrefab);
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(deadNutcracker.spawnPrefab);
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(deadSpider.spawnPrefab);


            Utilities.FixMixerGroups(deadHoardingBug.spawnPrefab);
            Utilities.FixMixerGroups(deadBaboonHawk.spawnPrefab);
            Utilities.FixMixerGroups(deadCentipede.spawnPrefab);
            Utilities.FixMixerGroups(deadThumper.spawnPrefab);
            Utilities.FixMixerGroups(deadEyelessDog.spawnPrefab);
            Utilities.FixMixerGroups(deadSpider.spawnPrefab);
            Utilities.FixMixerGroups(deadNutcracker.spawnPrefab);

            Items.RegisterScrap(deadHoardingBug, 0, Levels.LevelTypes.None);
            Items.RegisterScrap(deadBaboonHawk, 0, Levels.LevelTypes.None);
            Items.RegisterScrap(deadCentipede, 0, Levels.LevelTypes.None);
            Items.RegisterScrap(deadThumper, 0, Levels.LevelTypes.None);
            Items.RegisterScrap(deadEyelessDog, 0, Levels.LevelTypes.None);
            Items.RegisterScrap(deadNutcracker, 0, Levels.LevelTypes.None);
            Items.RegisterScrap(deadSpider, 0, Levels.LevelTypes.None);


            harmony.PatchAll(typeof(SellCorpsesBase));
            //harmony.PatchAll(typeof(RoundManagerPatch));

            harmony.PatchAll(typeof(HoardingBugPatch));
            harmony.PatchAll(typeof(BaboonHawkPatch));
            harmony.PatchAll(typeof(CentipedePatch));
            harmony.PatchAll(typeof(EyelessDogPatch));
            harmony.PatchAll(typeof(NutcrackerPatch));
            harmony.PatchAll(typeof(SandSpiderPatch));
            harmony.PatchAll(typeof(ThumperPatch));



        }


    }
}
