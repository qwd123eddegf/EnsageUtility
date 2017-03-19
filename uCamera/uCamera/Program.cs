using System;
using Ensage;
using Ensage.Common;
using Ensage.Common.Menu;

namespace uCamera
{
    class Program
    {
        private static Menu _menu;

        static void Main(string[] args)
        {
            Events.OnLoad += Events_OnLoad;
        }

        private static void Events_OnLoad(object sender, EventArgs e)
        {
            _menu = new Menu("Utility Camera", "uCam", true);

            var overwatch = _menu.AddSubMenu(new Menu("Overwatch", "overwatch"));
            overwatch.AddItem(new MenuItem("enable", "Enable Overwatch").SetValue(false));
            overwatch.AddItem(new MenuItem("enabletop", "Top Runespot").SetValue(false));
            overwatch.AddItem(new MenuItem("enablebot", " Bot Runespot").SetValue(false));
            overwatch.AddItem(new MenuItem("enabletbase", "Home Base").SetValue(false));
            overwatch.AddItem(new MenuItem("enableebase", "Enemy Base").SetValue(false));

            _menu.AddItem(new MenuItem("lockCamera", "Lock Camera").SetValue(new KeyBind('Z', KeyBindType.Toggle))).ValueChanged += Lock_Changed;

            _menu.AddToMainMenu();
        }

        private static void Lock_Changed(object sender, OnValueChangeEventArgs e)
        {
            Game.ExecuteCommand($"dota_camera_lock {Convert.ToInt32(e.GetNewValue<KeyBind>().Active)}");
        }
    }
}
