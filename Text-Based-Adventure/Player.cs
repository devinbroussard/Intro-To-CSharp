using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Text_Based_Adventure
{
    class Player : Entity
    {
        //Initializing private variables
        private Item[] _inventory;
        private Item _currentItem;
        private int _currentItemIndex;
        private PlayerClass _job;
        private int _gold;
        

        //Creating public properties
        public PlayerClass Job {
            get { return _job; }
            set { _job = value; }
        }
        //Attack and defense powers change depending on which item you have equipped
        public override float AttackPower
        {
            get
            {
                if (_currentItem.equipType == ItemType.ATTACK)
                    return base.AttackPower + CurrentItem.StatBoost;

                return base.AttackPower;
            }
        }
        public override float DefensePower
        {
            get
            {
                if (_currentItem.equipType == ItemType.DEFENSE)
                    return base.DefensePower + CurrentItem.StatBoost;

                return base.DefensePower;
            }
        }
        public Item CurrentItem
        {
            get { return _currentItem; }
        }
        public int Gold
        {
            get { return _gold; }
        }

        //Constructors called when player is instantiated
        public Player(string name, float health, float attackPower, float defensePower, Item[] inventory, PlayerClass job, int gold) : base(name, health, attackPower, defensePower)
        {
            _inventory = inventory;
            _currentItem.Name = "Nothing";
            _job = job;
            _gold = gold;
        }
        public Player(Item[] inventory)
        {
            _currentItem.Name = "Nothing";
            _inventory = inventory;
        }

        public Player()
        {
            _inventory = new Item[0];
            _currentItem.Name = "Nothing";
        }

        /// <summary>
        /// Tries to equip item at the given index of the _items array
        /// </summary>
        /// <param name="index"></param>
        /// <returns>False if the index is outside the bounds of the array</returns>
        public bool TryEquipItem(int index)
        {
            if (index >= _inventory.Length || index < 0)
                return false;

            if (_inventory[index].equipType == ItemType.HEALING)
            {
                base.Heal(_inventory[index].StatBoost);

                Item[] newInventory = new Item[_inventory.Length - 1];

                for (int i = 0; i < index; i++)
                {
                        newInventory[i] = _inventory[i];
                }

                for (int i = index + 1; i < _inventory.Length - 1; i++)
                {
                    newInventory[i] = _inventory[i + 1];
                }

                _inventory = newInventory;
                return true;
            }

            _currentItemIndex = index;

            _currentItem = _inventory[_currentItemIndex];

            return true;
        }

        /// <summary>
        /// Set the current item to be nothing
        /// </summary>
        /// <returns>False if there is no item equipped</returns>
        public bool TryRemoveCurrentItem()
        {
            if (_currentItem.Name == "Nothing")
                return false;

            _currentItemIndex = -1;

            _currentItem = new Item();
            _currentItem.Name = "Nothing";

            return true;
        }

        /// <returns>Get the name of all items in the player inventory</returns>
        public string[] GetItemNames()
        {
            string[] itemNames = new string[_inventory.Length];

            for (int i = 0; i < _inventory.Length; i++)
            {
                itemNames[i] = _inventory[i].Name;
            }

            return itemNames;
        }

        public override void Save(StreamWriter writer)
        {
            writer.WriteLine(_job);
            base.Save(writer);
            writer.WriteLine(_currentItemIndex);

            writer.WriteLine(_gold);
            writer.WriteLine(_inventory.Length);


            for (int i = 0; i < _inventory.Length; i++)
            {
                writer.WriteLine(_inventory[i].Name);
                writer.WriteLine(_inventory[i].StatBoost);
                writer.WriteLine(_inventory[i].equipType);
                writer.WriteLine(_inventory[i].Cost);
            }
        }

        public override bool Load(StreamReader reader)
        {
            int inventoryLength = 0;

            if (!base.Load(reader))
                return false;
            if (!int.TryParse(reader.ReadLine(), out _currentItemIndex))
                return false;
            if (!int.TryParse(reader.ReadLine(), out _gold))
                return false;
            if (!int.TryParse(reader.ReadLine(), out inventoryLength))
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
