using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Text_Based_Adventure
{
    class Shop
    {
        private int _gold;
        private Item[] _inventory;

        public Shop(Item[] inventory)
        {
            _inventory = inventory;
        }

        public bool Sell(Player player, int i)
        {
            if (player.Gold >= _inventory[i].Cost)
            {
                _gold += _inventory[i].Cost;
                player.Buy(_inventory[i]);
                return true;
            }
            else
                return false;
        }

        public string[] GetItemNames()
        {
            string[] itemNames = new string[_inventory.Length];

            for (int i = 0; i < _inventory.Length; i++)
                itemNames[i] = $"{_inventory[i].Name} - {_inventory[i].Cost}g";

            return itemNames;
        }
    }
}
