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

            if (_menu == null)
            {
                _menu = new Menu("Stash Picker", "StashPicker", true);
                _menu.AddItem(new MenuItem("hotkeyPick", "Hotkey to pick").SetValue(new KeyBind('I', KeyBindType.Press)));
                _menu.AddItem(new MenuItem("alwaysPick", "Always pick").SetValue(false));
                _menu.AddToMainMenu();
            }
            Game.OnIngameUpdate += Game_OnIngameUpdate;
        }

        private static void Events_OnClose(object sender, EventArgs e)
        {
            Game.OnIngameUpdate -= Game_OnIngameUpdate;
        }

        private static void PickfromStash()
        {
            var stash = _me.Inventory.Stash.ToList();

            if (!stash.Any())
                return;

            // move recipes to free backpack slots
            var backpackSlots = _me.Inventory.FreeBackpackSlots.ToList();
            if (backpackSlots.Any())
            {
                var recipes = stash.Where(x => x.IsRecipe).ToList();
                foreach (var item in recipes)
                {
                    if (!backpackSlots.Any())
                    {
                        break;
                    }
                    var slot = backpackSlots.First();

                    backpackSlots.RemoveAt(0);
                    stash.Remove(item);

                    item.MoveItem(slot);
                }
            }

            // move rest of items to free slots
            var freeSlots = _me.Inventory.FreeInventorySlots.ToList();
            if (freeSlots.Any())
            {
                foreach (var item in stash)
                {
                    if (!freeSlots.Any())
                    {
                        break;
                    }
                    var slot = freeSlots.First();

                    freeSlots.RemoveAt(0);

                    item.MoveItem(slot);
                }
            }
        }

        private static void Game_OnIngameUpdate(EventArgs args)
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
