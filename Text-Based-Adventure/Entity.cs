﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Text_Based_Adventure
{
    class Entity
    {
        //Initializing private variables
        private string _name;
        private float _health;
        private float _attackPower;
        private float _defensePower;

        //Creating read-only properties
        public string Name
        {
            get { return _name; }
        }
         public float Health
        {
            get { return _health; }
        }
        public float AttackPower
        {
            get { return _attackPower; }
        }
        public float DefensePower
        {
            get { return _defensePower; }
        }

        public Entity(string name, float health, float attackPower, float defensePower)
        {
            _name = name;
            _health = health;
            _attackPower = attackPower;
            _defensePower = defensePower;
        }

        public float TakeDamage(float damageAmount)
        {
            float damageTaken = damageAmount * (100 - _defensePower) / 100;

            _health -= damageTaken;

            if (_health > 0) _health = 0;

            return damageTaken;
        }

        public float Attack(Entity defender)
        {
            return defender.TakeDamage(_attackPower);
        }
    }
}
