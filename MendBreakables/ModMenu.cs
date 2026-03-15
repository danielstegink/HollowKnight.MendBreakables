using Satchel.BetterMenus;
using System;

namespace MendBreakables
{
    public static class ModMenu
    {
        private static Menu menu;
        private static MenuScreen menuScreen;

        public static MenuScreen CreateMenuScreen(MenuScreen modListMenu)
        {
            // Declare the root menu
            menu = new Menu("Mend Breakables Options", new Element[] { });

            // Populate root menu
            menu.AddElement(new HorizontalOption("Tram Gramaphones",
                    "",
                    MenuValues(),
                    value => MendBreakables.globalSettings.tramGramaphones = Convert.ToBoolean(value),
                    () => Convert.ToInt32(MendBreakables.globalSettings.tramGramaphones)));
            menu.AddElement(new HorizontalOption("Gardens Gramaphone",
                    "",
                    MenuValues(),
                    value => MendBreakables.globalSettings.gardensGramaphone = Convert.ToBoolean(value),
                    () => Convert.ToInt32(MendBreakables.globalSettings.gardensGramaphone)));
            menu.AddElement(new HorizontalOption("Collector Jars",
                    "",
                    MenuValues(),
                    value => MendBreakables.globalSettings.collectorJars = Convert.ToBoolean(value),
                    () => Convert.ToInt32(MendBreakables.globalSettings.collectorJars)));

            // Insert the menu into the overall menu
            menuScreen = menu.GetMenuScreen(modListMenu);

            return menuScreen;
        }

        private static string[] MenuValues()
        {
            return new string[] { "OFF", "ON" };
        }
    }
}