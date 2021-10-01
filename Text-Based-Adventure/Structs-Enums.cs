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
        public float StatBoost;
        public ItemType EquipType;
        public int Cost;
        public PlayerClass ClassType;
    }

    /// <summary>
    /// Enum created to label the different game scenes
    /// </summary>
    public enum Scene { STARTMENU, GETPLAYERNAME, GETPLAYERCLASS, BATTLE, ENTRANCE, SHOP, BETWEENBATTLES, RESTARTMENU }

}
