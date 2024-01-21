using HarmonyLib;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace SellCorpses.Patches
{
    internal class SandSpiderPatch
    {
        [HarmonyPatch((typeof(SandSpiderAI)), "KillEnemy")]
        [HarmonyPostfix]
        public static void spawnDeadBody(SandSpiderAI __instance)
        {

            //TODO do stuff
            //SellCorpsesBase.mls.LogDebug("spawnDeadBody called!");
           
            
            GameObject gameObject = UnityEngine.Object.Instantiate(SellCorpsesBase.deadSpider.spawnPrefab, __instance.transform.position, __instance.transform.rotation);
            GrabbableObject component = gameObject.GetComponent<GrabbableObject>();
            component.transform.rotation = Quaternion.Euler(component.itemProperties.restingRotation);
            component.fallTime = 0f;
            component.scrapValue = 160;
            NetworkObject component2 = gameObject.GetComponent<NetworkObject>();
            component2.Spawn();
            Debug.Log("Despawn network object in kill enemy called!");

            RoundManager.Instance.DespawnEnemyOnServer(__instance.GetComponent<NetworkObject>());



        }

    }
}
