using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Text_Based_Adventure
{
    class Entity
    {
        //Initializing private variables
        private string _name;
        private float _health;
        private float _attackPower;
        private float _defensePower;
        private int _rewardMoney;

        //Creating read-only public properties
        public string Name
        {
            get { return _name; }
        }
         public float Health
        {
            get { return _health; }
        }

        public int RewardMoney
        {
            get { return _rewardMoney; }
        }

        //Creating public read-only virtual properties that can be overwritten by classes that inherit this class
        public virtual float AttackPower
        {
            get { return _attackPower; }
        }
        public virtual float DefensePower
        {
            get { return _defensePower; }
        }

        /// <summary>
        /// Overloaded constructor that assigns default values if nothing is passed in 
        /// </summary>
        public Entity()
        {
            //Gives the entity default stats
            _name = "Default";
            _health = 0;
            _attackPower = 0;
            _defensePower = 0;
        }

        /// <summary>
        /// Overloaded constructor that allows the user to set the stats of the entity
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attackPower"></param>
        /// <param name="defensePower"></param>
        /// <param name="rewardMoney"></param>
        public Entity(string name, float attackPower, float defensePower, int rewardMoney)
        {
            //Sets the entity's stats to those of the passed in ones
            _name = name;
            _health = 100;
            _attackPower = attackPower;
            _defensePower = defensePower;
            _rewardMoney = rewardMoney;
        }

        /// <summary>
        /// Overloaded constructor that is called inside of inherrited classes
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>
        /// <param name="attackPower"></param>
        /// <param name="defensePower"></param>
        public Entity(string name, float health, float attackPower, float defensePower)
        {
            //Sets the stats to be those given in the parameters list
            _name = name;
            _health = health;
            _attackPower = attackPower;
            _defensePower = defensePower;
        }

        /// <summary>
        /// Function that calculates damage and subtracts it from the private health variable
        /// </summary>
        /// <param name="damageAmount"></param>
        /// <returns>Damage taken</returns>
        public float TakeDamage(float damageAmount)
        {
            //Calculates the damage taking the enemies defense into account
            float damageTaken = damageAmount * (100 - DefensePower) / 100;

            //subtracts the entity's health by the damage taken
            _health -= damageTaken;

            //If the entity's health is below 0, set their health to 0
            if (_health < 0) _health = 0;

            //return their damage taken so that it can be displayed
            return damageTaken;
        }

        /// <summary>
        /// Function that heals the entity by the amount passed in
        /// </summary>
        /// <param name="healAmount"></param>
        /// <returns></returns>
        public float Heal(float healAmount)
        {
            //Adds the heal amount to their health
            _health += healAmount;

            //If their health is over 100...
            if (_health > 100)
                //Sets their health to 100
                _health = 100;

            //returns the heal amount
            return healAmount;
        }

        /// <summary>
        /// Function that allows an entity to attack another entity
        /// </summary>
        /// <param name="defender"></param>
        /// <returns>The damage calculated by the enemy's TakeDamage function</returns>
        public float Attack(Entity defender)
        {
            //Calls the defender's takeDamage function and passes in the attacker's attackpower
            return defender.TakeDamage(AttackPower);
        }

        /// <summary>
        /// Function that saves the entity's stats
        /// </summary>
        /// <param name="writer"></param>
        public virtual void Save(StreamWriter writer)
        {
            //Writes down these stats into the "SaveData.txt" file
            writer.WriteLine(_name);
            writer.WriteLine(_health);
            writer.WriteLine(_attackPower);
            writer.WriteLine(_defensePower);
        }

        /// <summary>
        /// Function that loads the enemy's stats
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>true unless one of the loads are successful</returns>
        public virtual bool Load(StreamReader reader)
        {
            //Loads the entity's stats from the SaveData.txt file
            _name = reader.ReadLine();

            //Checks to see if these can be converted to floats, and if not, returns false
            if (!float.TryParse(reader.ReadLine(), out _health))
                return false;
            if (!float.TryParse(reader.ReadLine(), out _attackPower))
                return false;
            if (!float.TryParse(reader.ReadLine(), out _defensePower))
                return false;

            //returns true by default
            return true;
        }
    }
}
