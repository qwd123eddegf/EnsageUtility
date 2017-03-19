using System;
using System.Linq;
using Ensage;
using Ensage.Common;
using Ensage.Common.Menu;
using Ensage.Common.Threading;

namespace uStash
{
    internal class Program
    {
        private static Hero _me;
        private static Menu _menu;

        static void Main(string[] args)
        {
            Events.OnLoad += Events_OnLoad;
            Events.OnClose += Events_OnClose;
        }

        private static void Events_OnLoad(object sender, EventArgs e)
        {
            _me = ObjectManager.LocalHero;

            _menu = new Menu("Stash Picker", "StashPicker", true);
            _menu.AddItem(new MenuItem("hotkeyPick", "Hotkey to pick").SetValue(new KeyBind('I', KeyBindType.Press)));
            _menu.AddItem(new MenuItem("alwaysPick", "Always pick").SetValue(false));

            _menu.AddToMainMenu();

            GameDispatcher.OnIngameUpdate += GameDispatcher_OnIngameUpdate;
        }

        private static void Events_OnClose(object sender, EventArgs e)
        {
            GameDispatcher.OnIngameUpdate -= GameDispatcher_OnIngameUpdate;
        }

        private static void PickfromStash()
        {
            var stash = _me.Inventory.Stash;

            if (!stash.Any())
                return;

            foreach (var item in stash)
            {
                item.MoveItem(_me.Inventory.FreeSlots.FirstOrDefault());
            }
        }

        private static void GameDispatcher_OnIngameUpdate(EventArgs args)
        {

            if (Game.IsPaused || !_me.IsAlive)
            {
                return;
            }

            if (_me.AvailableShops != ShopFlags.None &&
                (_menu.Item("hotkeyPick").GetValue<KeyBind>().Active || _menu.Item("alwaysPick").GetValue<bool>()))
            {
                PickfromStash();
            }
        }
    }
}
