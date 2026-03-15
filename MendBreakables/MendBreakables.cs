using Modding;
using System.Collections.Generic;
using UnityEngine;

namespace MendBreakables
{
    public class MendBreakables : Mod, ICustomMenuMod, IGlobalSettings<GlobalSettings>
    {
        internal static MendBreakables Instance;

        public override string GetVersion() => "1.0.0.0";

        #region Global Settings
        internal static GlobalSettings globalSettings = new GlobalSettings();

        public void OnLoadGlobal(GlobalSettings s)
        {
            globalSettings = s;
        }

        public GlobalSettings OnSaveGlobal()
        {
            return globalSettings;
        }
        #endregion

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initializing");

            Instance = this;
            On.SceneData.FindMyState_PersistentBoolData += MendBreakable;

            Log("Initialized");
        }

        /// <summary>
        /// If an object is part of a group that needs to be mended, set its peristent bool to deactivated.
        /// Otherwise, the game will treat it as broken.
        /// </summary>
        /// <param name="orig"></param>
        /// <param name="self"></param>
        /// <param name="persistentBoolData"></param>
        /// <returns></returns>
        private PersistentBoolData MendBreakable(On.SceneData.orig_FindMyState_PersistentBoolData orig, 
                                                    SceneData self, PersistentBoolData persistentBoolData)
        {
            PersistentBoolData defaultData = orig(self, persistentBoolData);

            // Gramaphones in the trams
            if (globalSettings.tramGramaphones)
            {
                if (defaultData.sceneName.Contains("Room_Tram") &&
                    defaultData.id.Contains("gramaphone"))
                {
                    defaultData.activated = false;
                }
            }
            
            // That one gramaphone in Queen's Gardens
            if (globalSettings.gardensGramaphone)
            {
                if (defaultData.sceneName.Contains("Fungus3_50") &&
                    defaultData.id.Contains("gramaphone"))
                {
                    defaultData.activated = false;
                }
            }
            
            // The glass jars in the Tower of Love
            if (globalSettings.collectorJars)
            {
                if (defaultData.sceneName.Contains("Ruins2_11") &&
                    defaultData.id.Contains("Break Jar"))
                {
                    defaultData.activated = false;
                }
            }

            return defaultData;
        }

        #region Menu
        public bool ToggleButtonInsideMenu => false;

        public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? toggleDelegates)
        {
            return ModMenu.CreateMenuScreen(modListMenu);
        }
        #endregion
    }
}