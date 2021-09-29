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

        //Creating read-only properties
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

        //Creating virtual properties that can be overwritten byclasses that inherit
        public virtual float AttackPower
        {
            get { return _attackPower; }
        }
        public virtual float DefensePower
        {
            get { return _defensePower; }
        }

        /// <summary>
        /// Base constructor that assigns default values
        /// </summary>
        public Entity()
        {
            _name = "Default";
            _health = 0;
            _attackPower = 0;
            _defensePower = 0;
        }
        /// <summary>
        /// Constructor that allows the setting of entity stats through parameters
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>
        /// <param name="attackPower"></param>
        /// <param name="defensePower"></param>
        public Entity(string name, float health, float attackPower, float defensePower)
        {
            _name = name;
            _health = health;
            _attackPower = attackPower;
            _defensePower = defensePower;
        }
        public Entity(string name, float attackPower, float defensePower, int rewardMoney)
        {
            _name = name;
            _health = 100;
            _attackPower = attackPower;
            _defensePower = defensePower;
            _rewardMoney = rewardMoney;
        }

        /// <summary>
        /// Function that calculates damage and subtracts it from the private health variable
        /// </summary>
        /// <param name="damageAmount"></param>
        /// <returns>Damage taken</returns>
        public float TakeDamage(float damageAmount)
        {
            float damageTaken = damageAmount * (100 - DefensePower) / 100;

            _health -= damageTaken;

            if (_health < 0) _health = 0;

            return damageTaken;
        }

        public float Heal(float healAmount)
        {
            _health += healAmount;

            if (_health > 100)
                _health = 100;

            return healAmount;
        }

        /// <summary>
        /// Function that allows an entity to attack another entity
        /// </summary>
        /// <param name="defender"></param>
        /// <returns>The damage calculated by the enemy's TakeDamage function</returns>
        public float Attack(Entity defender)
        {
            return defender.TakeDamage(AttackPower);
        }

        /// <summary>
        /// Function that saves the entity's stats
        /// </summary>
        /// <param name="writer"></param>
        public virtual void Save(StreamWriter writer)
        {
            writer.WriteLine(_name);
            writer.WriteLine(_health);
            writer.WriteLine(_attackPower);
            writer.WriteLine(_defensePower);
        }

        /// <summary>
        /// Function that loads the enemy's stats
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public virtual bool Load(StreamReader reader)
        {
            _name = reader.ReadLine();

            if (!float.TryParse(reader.ReadLine(), out _health))
                return false;
            if (!float.TryParse(reader.ReadLine(), out _attackPower))
                return false;
            if (!float.TryParse(reader.ReadLine(), out _defensePower))
                return false;

            return true;
        }
    }
}
