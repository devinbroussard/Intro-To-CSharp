using System;
using System.Collections.Generic;
using System.Text;

namespace Text_Based_Adventure
{
   
    class Game
    {
        //Initializing variables
        private bool _gameOver;
        private Scene _currentScene;
        private Entity _currentEnemy;
        private int _currentEnemyIndex;
        private Entity[] _enemies;
        private Item[] _knightItems;
        private Item[] _assassinItems;
        private Item[] _wizardItems;
        private Item[] _shopItems;
        
        /// <summary>
        /// Main run function that is called by the program
        /// </summary>
        public void Run()
        {
            Start();
            //Loops until game is over
            while (!_gameOver)
            {
                Update();
            }
            End();
        }
        
        /// <summary>
        /// Function called when the program is opened
        /// </summary>
        private void Start()
        {
            _gameOver = false;
            _currentScene = Scene.STARTMENU;
        }

        //Function that is continiously called until the game is over
        private void Update() 
        {
            DisplayCurrentScene();
        }

        //Function that is called at the end of the program;
        private void End() 
        {
            Utilties.WriteRead("Goodbye, player!");
        }

        void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case Scene.STARTMENU:
                    DisplayStartMenu();
                    break;
                case Scene.GETPLAYERNAME:
                    GetPlayerName();
                    break;
            }
        }

        private void StartMenu()
        {
            Console.WriteLine("Hello player!");
            Console.ReadKey(true);
        }

        private void InitializeItems()
        {
            //Shop items
            Item healthPotion = new Item { Name = "Health Potion", StatBoost = 50, Type = ItemType.HEALING };
            Item longSword = new Item { Name = "Long Sword", StatBoost = 35, Type = ItemType.ATTACK };
            Item ironShield = new Item { Name = "Iron Shield", StatBoost = 60, Type = ItemType.DEFENSE };
            Item longDagger = new Item { Name = "Long Dagger", StatBoost = 60, Type = ItemType.ATTACK };
            Item shadowCloak = new Item { Name = "Shadow Cloak", StatBoost = 25, Type = ItemType.DEFENSE };
            Item enchantedWand = new Item { Name = "Enchanted Wand", StatBoost = 75, Type = ItemType.ATTACK };

            //Items for the knight class
            Item sword = new Item { Name = "Sword", StatBoost = 20, Type = ItemType.ATTACK };
            Item shield = new Item { Name = "Shield", StatBoost = 40, Type = ItemType.DEFENSE };

            //Items for the assassin class
            Item dagger = new Item { Name = "Dagger", StatBoost = 40, Type = ItemType.ATTACK };
            Item cloak = new Item { Name = "Cloak", StatBoost = 15, Type = ItemType.DEFENSE };

            //Items for the wizard class
            Item wand = new Item { Name = "Wand", StatBoost = 60, Type = ItemType.ATTACK };

            //Creating class-specific item arrays
            _knightItems = new Item[] { sword, shield };
            _assassinItems = new Item[] { dagger, cloak };
            _wizardItems = new Item[] { wand, healthPotion };
            _shopItems = new Item[] { healthPotion, longSword, ironShield, longDagger, shadowCloak, enchantedWand };
        }

        /// <summary>
        /// Reusable function for getting player input
        /// </summary>
        /// <param name="description"></param>
        /// <param name="options"></param>
        /// <returns>int that represents the players choice</returns>
        private int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputReceived = -1;

            while (inputReceived == -1)
            {
                Console.WriteLine(description);
                
                for (int i = 0; i < options.Length; i++)
                    Console.WriteLine($"{i + 1}. {options[i]}");

                Console.Write("> ");
                input = Console.ReadLine();
                
                //if the player typed an int...
                if (int.TryParse(input, out inputReceived))
                {
                    //...decrement the input and check if it's within the bounds of the array
                    inputReceived--;
                    if (inputReceived < 0 || inputReceived >- options.Length)
                    {
                        //Set input received to be the default value
                        inputReceived = -1;
                        //Display error message
                        Utilties.WriteRead("Invalid input.");
                    }
                }
                else
                {
                    inputReceived = -1;
                    Utilties.WriteRead("Invalid input.");
                }
                Console.Clear();
            }
            return inputReceived;
        }

        void DisplayStartMenu()
        {
            Console.WriteLine("Hello player! Welcome to this text-based adventure game!");
            int choice = GetInput("Would you like to start a new game, or load an existing one?", "Start new game", "Load existing game");

            if (choice == 1)
                _currentScene = Scene.GETPLAYERNAME;
        }

        void GetPlayerName()
        {

        }
    }
}
