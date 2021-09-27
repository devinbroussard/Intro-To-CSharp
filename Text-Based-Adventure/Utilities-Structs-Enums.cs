using System;
using System.Collections.Generic;
using System.Text;

namespace Text_Based_Adventure
{
    static class Utilties
    {
    public static void WriteRead(string text)
    {
        Console.WriteLine(text);
        Console.ReadKey(true);
    }
    }

    //Enum created to manage item types
    public enum ItemType { DEFENSE, ATTACK, HEALING, NONE }
    public enum PlayerClass { KNIGHT, WIZARD, ASSASSIN }

    //Item struct used for items
    public struct Item
    {
        public string Name;
        public float StatBoost;
        public ItemType equipType;
        public int Cost;
        public playerClass classType;
    }

    //Enum created to manage scenes
    public enum Scene { STARTMENU, GETPLAYERNAME}

}
