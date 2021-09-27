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

    //Item struct used for items
    public struct Item
    {
        public string Name;
        public float StatBoost;
        public ItemType Type;
        public int Cost;
    }

    //Enum created to manage scenes
    public enum Scene { STARTMENU, GETPLAYERNAME}

}
