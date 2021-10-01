using System;
using System.Collections.Generic;
using System.Text;

namespace Text_Based_Adventure
{ 
    /// <summary>
    /// Enum created to group items by types
    /// </summary>
    public enum ItemType { DEFENSE, ATTACK, HEALING, NONE }

    /// <summary>
    /// Enum created to label things for a certain class
    /// </summary>
    public enum PlayerClass { KNIGHT, WIZARD, ASSASSIN }

    /// <summary>
    /// Type used for items obtainable by players
    /// Contains different item-specific attributes
    /// </summary>
    public struct Item
    {
        public string Name;
        //Float created to store how much it will affect the equipType stat
        public float StatBoost;
        //ItemType variable created to store which stat the item will affect
        public ItemType EquipType;
        //Cost variable created to store how much the item will cost in the hsop
        public int Cost;
        //PlayerClass variable created to store which class the item will be available to
        public PlayerClass ClassType;
    }

    /// <summary>
    /// Enum created to label the different game scenes
    /// </summary>
    public enum Scene { STARTMENU, GETPLAYERNAME, GETPLAYERCLASS, BATTLE, ENTRANCE, SHOP, BETWEENBATTLES, RESTARTMENU }

}
