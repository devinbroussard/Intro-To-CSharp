using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Text_Based_Adventure
{
    class Shop
    {
        //Creating private variables to store the gold and inventory of the shop
        private int _gold;
        private Item[] _inventory;

        /// <summary>
        /// Constructor called whenever a new instance of the shop is created
        /// Takes in an array of items that is then set the shop's inventory
        /// </summary>
        /// <param name="inventory"></param>
        public Shop(Item[] inventory)
        {
            _inventory = inventory;
        }

        /// <summary>
        /// Function used to sell an item to the player
        /// takes in the player and the index of the item selected by the player
        /// </summary>
        /// <param name="player"></param>
        /// <param name="itemIndex"></param>
        /// <returns></returns>
        public bool Sell(Player player, int itemIndex)
        {
            //If the player has enough gold to purchase the item selected...
            if (player.Gold >= _inventory[itemIndex].Cost)
            {
                //..adds the gold collected by the shop into its gold variable...
                _gold += _inventory[itemIndex].Cost;
                //..calls the player's buy function and passes in the item selected
                player.Buy(_inventory[itemIndex]);
                // and returns true if successful
                return true;
            }
            //if the player doesn't have enough gold...
            else
                //returns false
                return false;
        }

        /// <summary>
        /// Creates a new string array filled with the shop item's names and costs
        /// </summary>
        /// <returns></returns>
        public string[] GetItemNames()
        {
            //Creates a new string array with the same length as the shop's inventory
            string[] itemNames = new string[_inventory.Length];

            //For each index in the itemNames array, set it equal to the name of the item in inventory, and display the cost
            for (int i = 0; i < _inventory.Length; i++)
                itemNames[i] = $"{_inventory[i].Name} - {_inventory[i].Cost}g";

            //returns the new string array
            return itemNames;
        }

        /// <summary>
        /// Gets the classes of the items in the inventory and stores them in a string array
        /// </summary>
        /// <returns></returns>
        public string[] GetItemClasses()
        {
            //creates a new array with the same length as the shop inventory
            string[] itemClasses = new string[_inventory.Length];

            //For each item in the inventory...
            for (int i = 0; i < _inventory.Length; i++)
                //..Set the itemClasses array to be the classtype of the item
                itemClasses[i] = $"{_inventory[i].ClassType}";


            //returns the stringarrayS
            return itemClasses;
        }
    }
}
