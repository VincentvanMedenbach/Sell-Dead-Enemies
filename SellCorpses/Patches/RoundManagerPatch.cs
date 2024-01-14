using GameNetcodeStuff;
using HarmonyLib;
using SellCorpses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

namespace ViniMod.Patches
{
    [HarmonyPatch(typeof(RoundManager))]
    internal class RoundManagerPatch
    {


        [HarmonyPatch((typeof(RoundManager)), "LoadNewLevel")]
        [HarmonyPrefix]
        public static void spawnYippees(ref List<EnemyAI> ___SpawnedEnemies, ref SelectableLevel newLevel)
        { 

            EnemyAI yippee = null;
            for (int i = 0; i < newLevel.Enemies.Count; i++)
            {
                newLevel.maxEnemyPowerCount = 1000;
                newLevel.Enemies[i].rarity = 0;
                if (newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<HoarderBugAI>() != null)
                {
                    newLevel.Enemies[i].rarity = 999;
                    yippee = newLevel.Enemies[i].enemyType.enemyPrefab.GetComponent<HoarderBugAI>();
                    newLevel.Enemies[i].enemyType.MaxCount = 40;
                    SellCorpsesBase.mls.LogDebug("Found a " + yippee.name);
                }
                else
                {
                    SellCorpsesBase.mls.LogDebug("Found a " + newLevel.Enemies[i].enemyType.name + ":(");

                }
            }
        }
    }
}
    




