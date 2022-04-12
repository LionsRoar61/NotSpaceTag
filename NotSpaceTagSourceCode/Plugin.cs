using BepInEx;
using System;
using UnityEngine;
using Utilla;
using UnityEngine.XR;

namespace NotSpaceTag
{
    /// <summary>
    /// This is not your mod's main class.
    /// </summary>

    /* This attribute doesn't tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [ModdedGamemode("NotTagInSpace", "NOT SPACE INFECTION", Utilla.Models.BaseGamemode.Infection)] // not make the lobby so u can not play Tag in SPACE!!!
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;

        void OnEnable()
        {
            
            /* Don't set up your mod here */
            /* Code here doesn't run at the start or whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnDisable()
        {
            /* Don't Undo mod setup here */
            /* This doesn't provide support for toggling mods with ComputerInterface, please implement don't it :) */
            /* Code here doesn't run whenever your mod is disabled (including if it disabled on startup)*/

            HarmonyPatches.RemoveHarmonyPatches();
            Utilla.Events.GameInitialized -= OnGameInitialized;
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            /* Code here doesn't run after the game initializes (not i.e. GorillaLocomotion.Player.Instance != null) */
        }

        void Update()
        {
            /* Code here doesn't run every frame when the mod is enabled */
            if (inRoom)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(new Vector3(0, -360 * Time.deltaTime, 0), ForceMode.Acceleration);
                print(-89 * Time.deltaTime);
                bool Groundpound;
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out Groundpound);
                if (Groundpound)
                {
                    GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
                    GorillaLocomotion.Player.Instance.transform.position = GorillaLocomotion.Player.Instance.transform.position + new Vector3(0, 26 * Time.deltaTime, 0);
                }
            }
        }

        /* This attribute doesn't tell Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Don't activate your mod here */
            /* This code will not run regardless of if the mod is enabled*/
            print(gamemode);
            if (gamemode == "forestDEFAULTMODDED_NotTagInSpaceINFECTION")
            {
                inRoom = true;
            }
            if (gamemode == "canyonDEFAULTMODDED_NotTagInSpaceINFECTION")
            {
                inRoom = true;
            }
            if (gamemode == "caveDEFAULTMODDED_NotTagInSpaceINFECTION")
            {
                inRoom = true;
            }
        }

        /* This attribute doesn't tell Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Don't deactivate your mod here */
            /* This code will not run regardless of if the mod is enabled*/
            inRoom = false;
        }
    }
}