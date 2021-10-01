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
                if (_currentItem.EquipType == ItemType.ATTACK)
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
                if (_currentItem.EquipType == ItemType.DEFENSE)
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
        /// Constructor that is called whenever a new instance of the player is called
        /// Can take in parameters that gives the player it's stats
        /// Also inherrits the base entity's constructor
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
        /// This constructor is used when a new instance of the player is created while loading 
        /// </summary>
        /// <param name="inventory"></param>
        public Player(Item[] inventory)
        {
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
            //If the player's selected index is outside of the inventory, return false
            if (itemIndex >= _inventory.Length || itemIndex < 0)
                return false;

            //If the player's selected item is one of the healing type
            if (_inventory[itemIndex].EquipType == ItemType.HEALING)
            {
                //Create a new item variable to store the current item
                Item currentItem = _currentItem;

                //Calls the base entity's heal function and passes in the healing item's statboost 
                base.Heal(_inventory[itemIndex].StatBoost);

                //Creates a new inventory that is one element shorter than the current inventory so that the healing item can be removed
                Item[] newInventory = new Item[_inventory.Length - 1];

                //For each index in the new inventory up until the selected item index, set it equal to the old inventory's index
                for (int i = 0; i < itemIndex; i++)
                {
                    newInventory[i] = _inventory[i];
                }

                //For each index in the new inventory after the selected index before, set it equal to the old inventory's index plus one
                //This is used to skip the used item so that it is not in the new inventory
                for (int i = itemIndex; i < _inventory.Length - 1; i++)
                {
                    newInventory[i] = _inventory[i + 1];
                }

                //Sets the old inventory to the new one
                _inventory = newInventory;
                //Sets the current item to be the one saved at the start of the function
                _currentItem = currentItem;

                //returns true unless the equip was unsuccessful
                return true;
            }

            //Sets the currentItemIndex to be the itemIndex selected by the player
            _currentItemIndex = itemIndex;

            //Sets the current item to be that of the currentItemIndex
            _currentItem = _inventory[_currentItemIndex];

            //returns true unless the equip was unsuccessful
            return true;
        }

        /// <summary>
        /// Set the current item to be nothing
        /// </summary>
        /// <returns>False if there is no item equipped</returns>
        public bool TryRemoveCurrentItem()
        {
            //If the player already has nothign equipped, return false
            if (_currentItem.Name == "Nothing")
                return false;

            //Sets the currentItemIndex to -1, so that if the player saves and loads, an item won't be re-equipped
            _currentItemIndex = -1;

            //Sets the current item to a blank item with the name "Nothing"
            _currentItem = new Item();
            _currentItem.Name = "Nothing";

            return true;
        }

        /// <summary>
        /// Gets a list of the player items' name
        /// </summary>
        /// <returns>A string array</returns>
        public string[] GetItemNames()
        {
            //Creates a new string array with the same length as the player's inventory
            string[] itemNames = new string[_inventory.Length];

            //Sets each index in the itemNames array to be equal to the Name of the same index in the inventory array
            for (int i = 0; i < _inventory.Length; i++)
            {
                itemNames[i] = _inventory[i].Name;
            }

            //returns the string array
            return itemNames;
        }

        /// <summary>
        /// Saves the player stats
        /// </summary>
        /// <param name="writer"></param>
        public override void Save(StreamWriter writer)
        {
            //Saves the player's stats
            writer.WriteLine(_job);
            //Calls the base entity's save function
            base.Save(writer);
            writer.WriteLine(_currentItemIndex);
            writer.WriteLine(_gold);
            
            //Saves the player's inventory length
            writer.WriteLine(_inventory.Length);

            //Saves the item stats of each item in the player inventory
            for (int i = 0; i < _inventory.Length; i++)
            {
                writer.WriteLine(_inventory[i].Name);
                writer.WriteLine(_inventory[i].StatBoost);
                writer.WriteLine(_inventory[i].EquipType);
                writer.WriteLine(_inventory[i].Cost);
            }
        }

        /// <summary>
        /// Loads the player stats
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>If the load was successful or not</returns>
        public override bool Load(StreamReader reader)
        {
            //Checks to see if the entity's base load function was successful
            if (!base.Load(reader))
                return false;
            //Checks to see if the loaded text can be successfully converted to the original variables type, and returns false if not
            if (!int.TryParse(reader.ReadLine(), out _currentItemIndex))
                return false;
            if (!int.TryParse(reader.ReadLine(), out _gold))
                return false;
            if (!int.TryParse(reader.ReadLine(), out int inventoryLength))
                return false;

            //Creates a new inventory with the inventory length loaded
            _inventory = new Item[inventoryLength];

            //For each item in the in the inventory, loads the stats of each item
            for (int i = 0; i < _inventory.Length; i++)
            {
                _inventory[i].Name = reader.ReadLine();
                if (!float.TryParse(reader.ReadLine(), out _inventory[i].StatBoost))
                    return false;
                if (!Enum.TryParse<ItemType>(reader.ReadLine(), out _inventory[i].EquipType))
                    return false;
                if (!int.TryParse(reader.ReadLine(), out _inventory[i].Cost))
                    return false;
            }

            //Tries to equip the item of the current index
            TryEquipItem(_currentItemIndex);

            //returns true if the load was successful
            return true;
        }

        /// <summary>
        /// Function used to buy an item from the shop
        /// </summary>
        /// <param name="item"></param>
        public void Buy(Item item)
        {
            //Subtracts the gold by the amount of the item purchased
            _gold -= item.Cost;

            //Creates a new inventory that is one length larger than the one before
            Item[] newInventory = new Item[_inventory.Length + 1];

            //Sets the new inventory's index as equal to the old inventory
            for (int i = 0; i < _inventory.Length; i++)
                newInventory[i] = _inventory[i];

            //Sets the last index of the new inventory equal to the newly bought item
            newInventory[_inventory.Length] = item;

            //Sets the player's inventory equal to the new inventory created
            _inventory = newInventory;
        }
        
        //Function used to get the reward money from the defeated enemy after battle
        public int GetRewardMoney(Entity entity)
        {
            //Adds the reward moeny to the player's inventory
            _gold += entity.RewardMoney;

            //Returns the reward money so that it can be displayed to the player
            return entity.RewardMoney;
        }
    }
}
