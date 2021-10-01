using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Text_Based_Adventure
{
    class Player : Entity
    {
        //Definining the player's inventory
        private Item[] _inventory;
        //Defining the player's current item
        private Item _currentItem;
        //Defining the current item index, that will be used to fetch items from the _inventory
        private int _currentItemIndex;
        //Defining the player's class
        private PlayerClass _job;
        //Defining the player's gold count
        private int _gold;
        

        //Creating public Job property that can be read and set
        public PlayerClass Job {
            get { return _job; }
            set { _job = value; }
        }

        //Creating readonly AttackPower and DefensePower properties that override the inherited entity's
        public override float AttackPower
        {
            get
            {
                //If the player has an item that affects their attack stat...
                if (_currentItem.equipType == ItemType.ATTACK)
                    //returns their base attackpower plus the statboost from their item
                    return base.AttackPower + CurrentItem.StatBoost;

                //otherwise, return their base attackpower
                return base.AttackPower;
            }
        }
        public override float DefensePower
        {
            get
            {
                //If the player has an item that affects their defense stat...
                if (_currentItem.equipType == ItemType.DEFENSE)
                    //returns their base attackpower plus the statboost from their item
                    return base.DefensePower + CurrentItem.StatBoost;

                //otherwise, return their base attackpower
                return base.DefensePower;
            }
        }

        //Creating a readonly property that returns the current item
        public Item CurrentItem
        {
            get { return _currentItem; }
        }
        //Creating a readonly property that returns the player's gold count
        public int Gold
        {
            get { return _gold; }
        }

        /// <summary>
        /// Construcotr called to give the player stats, an inventory, and a class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>
        /// <param name="attackPower"></param>
        /// <param name="defensePower"></param>
        /// <param name="inventory"></param>
        /// <param name="job"></param>
        public Player(string name, float health, float attackPower, float defensePower, Item[] inventory, PlayerClass job) : base(name, health, attackPower, defensePower)
        {
            _inventory = inventory;
            _currentItem.Name = "Nothing";
            _job = job;
            _gold = 50;
            _currentItemIndex = -1;
        }
        /// <summary>
        /// Constructor called to create an instance of the player when loading so that other stats can be loaded
        /// </summary>
        /// <param name="inventory"></param>
        public Player(Item[] inventory)
        {
            //Sets the current item to be nothing, and the inventory to be the one given in the parameters list 
            _currentItem.Name = "Nothing";
            _inventory = inventory;
        }

        /// <summary>
        /// Tries to equip item at the given index of the _items array
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <returns>False if the index is outside the bounds of the array</returns>
        public bool TryEquipItem(int itemIndex)
        {
            //If the given index is outside of the array...
            if (itemIndex >= _inventory.Length || itemIndex < 0)
                //..return false
                return false;


            //If the item at the given index is that of the healing type...
            else if (_inventory[itemIndex].equipType == ItemType.HEALING)
            {
                //Creating a variable to store the current item
                Item currentItem = _currentItem;

                //Use the base entity's heal function and pass in the healing item selected's statboost
                base.Heal(_inventory[itemIndex].StatBoost);

                //Creates a new inventory that is one element smaller than the previous so that the healing item can be removed
                Item[] newInventory = new Item[_inventory.Length - 1];

                //For each index up until the itemIndex that was used, set the new inventory equal to the old one
                for (int i = 0; i < itemIndex; i++)
                {
                    newInventory[i] = _inventory[i];
                }

                //For every index after the itemIndex, set the newInventory to be equal to the previous one, skipping the used item
                for (int i = itemIndex; i < _inventory.Length - 1; i++)
                {
                    newInventory[i] = _inventory[i + 1];
                }

                //Set the player inventory equal to the new inventory
                _inventory = newInventory;
                //Set the current item back to the one stored in the beginning
                _currentItem = currentItem;

                //returns true because the equip was successful
                return true;
            }

            //Set the currentItemIndex to be the one given by the player
            _currentItemIndex = itemIndex;

            //Sets the current item to be the one selected by the player
            _currentItem = _inventory[_currentItemIndex];

            //returns true of the equip was succesful
            return true;
        }

        /// <summary>
        /// Set the current item to be nothing
        /// </summary>
        /// <returns>False if there is no item equipped</returns>
        public bool TryRemoveCurrentItem()
        {
            //If the current item's name is nothing, return false
            if (_currentItem.Name == "Nothing")
                return false;

            //sets the current item index to be -1
            _currentItemIndex = -1;

            //Creates a new blank item that will because the current item
            _currentItem = new Item();
            _currentItem.Name = "Nothing";

            //return true unless the player had no item equipped
            return true;
        }

        /// <returns>Get the name of all items in the player inventory</returns>
        public string[] GetItemNames()
        {
            //Creates a new string array equal to the inventory length
            string[] itemNames = new string[_inventory.Length];

            //For each item in the inventory..
            for (int i = 0; i < _inventory.Length; i++)
            {
                //Set the itemNames array to match the name of the item in the inventory array
                itemNames[i] = _inventory[i].Name;
            }

            //return the new string array of names
            return itemNames;
        }

        /// <summary>
        /// Function called to save the player's stats 
        /// </summary>
        /// <param name="writer"></param>
        public override void Save(StreamWriter writer)
        {
            //Writes down the player's job 
            writer.WriteLine(_job);
            //writes down the stats from the base entity's save file
            base.Save(writer);
            //write down the player's current item index
            writer.WriteLine(_currentItemIndex);
            //writes down the player's gold
            writer.WriteLine(_gold);
            //writes down the player's inventory length
            writer.WriteLine(_inventory.Length);

            //writes down the stats of the items in the player's inventory
            for (int i = 0; i < _inventory.Length; i++)
            {
                writer.WriteLine(_inventory[i].Name);
                writer.WriteLine(_inventory[i].StatBoost);
                writer.WriteLine(_inventory[i].equipType);
                writer.WriteLine(_inventory[i].Cost);
            }
        }

        /// <summary>
        /// Function called the load the player's stats
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>true or false depending on if the load was succesful</returns>
        public override bool Load(StreamReader reader)
        {
            //If the base load of the player worked...
            if (!base.Load(reader))
                return false;
            //IF the current i
            if (!int.TryParse(reader.ReadLine(), out _currentItemIndex))
                return false;


            if (!int.TryParse(reader.ReadLine(), out _gold))
                return false;
            if (!int.TryParse(reader.ReadLine(), out int inventoryLength))
                return false;

            _inventory = new Item[inventoryLength];

            for (int i = 0; i < _inventory.Length; i++)
            {
                _inventory[i].Name = reader.ReadLine();
                if (!float.TryParse(reader.ReadLine(), out _inventory[i].StatBoost))
                    return false;
                if (!Enum.TryParse<ItemType>(reader.ReadLine(), out _inventory[i].equipType))
                    return false;
                if (!int.TryParse(reader.ReadLine(), out _inventory[i].Cost))
                    return false;
            }

            TryEquipItem(_currentItemIndex);

            return true;
        }

        public void Buy(Item item)
        {
            _gold -= item.Cost;

            Item[] newInventory = new Item[_inventory.Length + 1];

            for (int i = 0; i < _inventory.Length; i++)
                newInventory[i] = _inventory[i];

            newInventory[_inventory.Length] = item;

            _inventory = newInventory;
        }

        public int GetRewardMoney(Entity entity)
        {
            _gold += entity.RewardMoney;

            return entity.RewardMoney;
        }
    }
}
