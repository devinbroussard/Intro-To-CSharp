﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Text_Based_Adventure
{

    class Game
    {
        //Defining variables
        private string _playerName;
        private bool _gameOver;
        private Scene _currentScene;
        private Entity _currentEnemy;
        private int _currentEnemyIndex;
        private Entity[] _enemies;
        private Shop _shop;

        //Defining arrays containing entity/shop items
        private Item[] _knightItems;
        private Item[] _assassinItems;
        private Item[] _wizardItems;
        private Item[] _shopItems;

        //Defining player entity
        Player _player;

        /// <summary>
        /// Main run function that is called by the program
        /// </summary>
        public void Run()
        {
            //Start function called at the start of the game to initialize anything needed
            Start();
            //Loops until game is over
            while (!_gameOver)
            {
                //Updates the current scene 
                Update();
            }
            //Called at the end of the function 
            End();
        }

        /// <summary>
        /// Function called when the program is opened
        /// </summary>
        private void Start()
        {
            _gameOver = false;
            _currentScene = Scene.STARTMENU;

            InitializeEnemies();
            InitializeItems();
        }

        //Function that is continiously called until the game is over
        private void Update()
        {
            DisplayCurrentScene();
        }

        //Function that is called at the end of the program;
        private void End()
        {
            Console.WriteLine("Goodbye, player!");

            Console.ReadKey(true);
        }

        /// <summary>
        /// Called to display a current scene
        /// </summary>
        void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case Scene.STARTMENU:
                    DisplayStartMenu();
                    break;
                case Scene.ENTRANCE:
                    DisplayEntranceScene();
                    break;
                case Scene.GETPLAYERNAME:
                    GetPlayerName();
                    break;
                case Scene.GETPLAYERCLASS:
                    GetPlayerClass();
                    break;
                case Scene.BATTLE:
                    Battle();
                    CheckBattleResults();
                    break;
                case Scene.BETWEENBATTLES:

                    break;
                case Scene.SHOP:
                    DisplayShopMenu();
                    break;

            }
        }

        /// <summary>
        /// Used as the welcome screen
        /// </summary>
        void DisplayStartMenu()
        {
            Console.WriteLine("Hello player! In this game you will fight enemies to reach the top of a tower!");
            int choice = GetInput("Would you like to start a new game, or load an existing save file?", "Start new game", "Load existing game");

            if (choice == 0)
                _currentScene = Scene.GETPLAYERNAME;

            if (choice == 1)
                Load();
        }

        private void DisplayBetweenBattlesScene()
        {
            
        }

        private void DisplayEntranceScene()
        {
            Console.WriteLine("You approach the tower...");
            Console.WriteLine("You notice there is a shop near the front entrance");
            Console.ReadKey(true);
            Console.Clear();

            _currentScene = Scene.BETWEENBATTLES;
        }

        /// <summary>
        /// Used to ask the player for their name
        /// </summary>
        void GetPlayerName()
        {
            Console.Clear();
            Console.WriteLine("Please enter your name:");
            Console.Write("> ");
            _playerName = Console.ReadLine();

            int choice = GetInput($"\nAre you sure {_playerName} is your name?", "Yes", "No");

            if (choice == 0)
                _currentScene = Scene.GETPLAYERCLASS;
        }

        /// <summary>
        /// Asks the player to select a class and assigns their stats accordingly
        /// </summary>
        private void GetPlayerClass()
        {
            int choice = GetInput($"Okay {_playerName}, select a class:", "Knight", "Wizard", "Assassin");

            if (choice == 0)
                _player = new Player(_playerName, 100, 20, 40, _knightItems, PlayerClass.KNIGHT);
            else if (choice == 1)
                _player = new Player(_playerName, 100, 60, 0, _wizardItems, PlayerClass.WIZARD);
            else if (choice == 2)
                _player = new Player(_playerName, 100, 40, 20, _assassinItems, PlayerClass.ASSASSIN);

            _currentScene = Scene.BATTLE;
        }

        private void DisplayBetweenBattle()
        {
            //Not finished
        }

        /// <summary>
        /// Initializes all the used items in the game
        /// </summary>
        private void InitializeItems()
        {
            //Shop items
            Item healthPotion = new Item { Name = "Health Potion", StatBoost = 50, equipType = ItemType.HEALING, Cost = 50 };
            Item ironShield = new Item { Name = "Iron Shield", StatBoost = 60, equipType = ItemType.DEFENSE, Cost = 200 };
            Item longDagger = new Item { Name = "Long Dagger", StatBoost = 60, equipType = ItemType.ATTACK, Cost = 200 };
            Item enchantedWand = new Item { Name = "Enchanted Wand", StatBoost = 75, equipType = ItemType.ATTACK, Cost = 200 };

            //Items for the knight class
            Item woodenShield = new Item { Name = "Wooden Shield", StatBoost = 40, equipType = ItemType.DEFENSE, Cost = 50 };

            //Items for the assassin class
            Item shortDagger = new Item { Name = "Short Dagger", StatBoost = 40, equipType = ItemType.ATTACK, Cost = 50 };

            //Items for the wizard class
            Item basicWand = new Item { Name = "Basic Wand", StatBoost = 60, equipType = ItemType.ATTACK, Cost = 50 };

            //Creating class-specific item arrays
            _knightItems = new Item[] { woodenShield };
            _assassinItems = new Item[] { shortDagger };
            _wizardItems = new Item[] { basicWand };
            _shopItems = new Item[] { healthPotion, ironShield, longDagger, enchantedWand };

            _shop = new Shop(_shopItems);
        }

        /// <summary>
        /// Initializes all of the enemies fought in the game
        /// </summary>
        private void InitializeEnemies()
        {
            Entity slime = new Entity("Slime", 10, 10, 25);
            Entity skeleton = new Entity("Skeleton", 30, 20, 55);
            Entity darkKnight = new Entity("Dark Knight", 50, 50, 80);

            _enemies = new Entity[] { slime, skeleton, darkKnight };
            _currentEnemyIndex = 0;
            _currentEnemy = _enemies[_currentEnemyIndex];
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

            Console.WriteLine($"{description}\n");

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            Console.Write("> ");

            //Get input from player
            input = Console.ReadLine();

            //If the player typed an int...
            if (int.TryParse(input, out inputReceived))
            {
                //...decrement the input and check if it's within the bounds of the array
                inputReceived--;

                if (inputReceived < 0 || inputReceived >= options.Length)
                {
                    //Set the input received to be the default value
                    inputReceived = -1;
                    //Display an error message
                    Console.WriteLine("\nInvalid input.");
                    Console.ReadKey(true);
                }
            }
            else
            {
                inputReceived = -1;
                Console.WriteLine("\nInvalid input.");
                Console.ReadKey(true);
            }

            Console.Clear();

            return inputReceived;
        }

        /// <summary>
        /// Displays the stats of an enemy
        /// </summary>
        /// <param name="entity"></param>
        private void DisplayStats(Entity entity)
        {
            Console.WriteLine("\nEnemy Stats:");
            Console.WriteLine($"    Name: {entity.Name}");
            Console.WriteLine($"    Health: {entity.Health}");
            Console.WriteLine($"    Attack Power: {entity.AttackPower}");
            Console.WriteLine($"    Defense Power: {entity.DefensePower}\n");
        }

        /// <summary>
        /// Displays the stats of the player
        /// </summary>
        /// <param name="player"></param>
        private void DisplayStats(Player player)
        {
            Console.WriteLine("Your Stats:");
            Console.WriteLine($"    Name: {player.Name}");
            Console.WriteLine($"    Class: {player.Job}");
            Console.WriteLine($"    Health: {player.Health}");
            Console.WriteLine($"    Attack Power: {player.AttackPower}");
            Console.WriteLine($"    Defense Power: {player.DefensePower}");
            Console.WriteLine($"    Equipped Item: {player.CurrentItem.Name}\n");
        }

        /// <summary>
        /// Function used to call Save functions from multiple enemies
        /// </summary>
        private void Save()
        {
            //creates a new streamwriter
            StreamWriter writer = new StreamWriter("SaveData.txt");

            //saves the current enemy index
            writer.WriteLine(_currentEnemyIndex);

            //calls both of the 
            _player.Save(writer);
            _currentEnemy.Save(writer);

            //closes the writer
            writer.Close();
        }

        /// <summary>
        /// Function used to call load functions from multiple enemies
        /// </summary>
        /// <returns>True if the load is successful</returns>
        private bool Load()
        {
            //crating a variable we can easily return
            bool loadSuccessful = true;
            PlayerClass playerJob;

            StreamReader reader = new StreamReader("SaveData.txt");

            if (!int.TryParse(reader.ReadLine(), out _currentEnemyIndex))
                loadSuccessful = false;
            if (!Enum.TryParse<PlayerClass>(reader.ReadLine(), out playerJob))
                loadSuccessful = false;

            _player.Job = playerJob;

            if (_player.Job == PlayerClass.KNIGHT)
                _player = new Player(_knightItems);
            else if (_player.Job == PlayerClass.WIZARD)
                _player = new Player(_wizardItems);
            else if (_player.Job == PlayerClass.ASSASSIN)
                _player = new Player(_assassinItems);

            if (!_player.Load(reader))
                loadSuccessful = false;
            if (!_currentEnemy.Load(reader))
                loadSuccessful = false;


            return loadSuccessful;
        }

        /// <summary>
        /// Function used to start a battle between the player and the current enemy
        /// </summary>
        private void Battle()
        {
            DisplayStats(_player);
            DisplayStats(_currentEnemy);

            int choice = GetInput($"A {_currentEnemy.Name} stands in front of you! What will you do:", "Attack", "Equip Item", "Remove current Item", "Save Game");

            Console.Clear();

            if (choice == 0)
            {
                float damage = _player.Attack(_currentEnemy);
                Console.WriteLine($"You dealt {damage} damage!");

                damage = _currentEnemy.Attack(_player);
                Console.WriteLine($"The {_currentEnemy.Name} dealt {damage} damage!");

            }

            else if (choice == 1)
            {
                DisplayEquipItemMenu();
            }

            else if (choice == 2)
            {
                if (!_player.TryRemoveCurrentItem())
                    Console.WriteLine("You don't have anything equipped.");
                else
                    Console.WriteLine("You placed the item in your bag.");
            }

            else if (choice == 3)
            {
                Save();
                Console.WriteLine("Game saved successfully!");
            }
            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Checks the outcome of the battle and assigns a new enemy or ends the game
        /// </summary>
        private void CheckBattleResults()
        {
            if (_player.Health == 0)
            {
                Console.WriteLine("You died!");
                Console.WriteLine("Game over!");
                Console.ReadKey(true);
                Console.Clear();

                //Put restart menu here
            }

            else if (_currentEnemy.Health == 0)
            {
                Console.WriteLine($"You slayed the {_currentEnemy.Name}!");
                Console.ReadKey(true);
                Console.Clear();
                _currentEnemyIndex++;

                //Not finished
            }
        }
        

        private bool TryFinalBoss()
        {
            bool fightBoss = _currentEnemyIndex >= _enemies.Length;

            if (fightBoss)
            {
                Console.WriteLine("You have reached the highest floor. The final boss awaits on the roof");
                int choice = GetInput("What would you like to do?", "Fight more enemies outside (You can come back later)", "Go to the shop", "Fight the boss");

                //plug different choices here

            }

            return fightBoss;
        }
        /// <summary>
        /// Allows the player to select an item to equip out of the player inventory
        /// </summary>
        private void DisplayEquipItemMenu()
        {
            int choice = GetInput("Select an item to equip.", _player.GetItemNames());

            if (!_player.TryEquipItem(choice))
                Console.WriteLine("You couldn't find that item in your bag!");

            Console.WriteLine($"You equipped {_player.CurrentItem.Name}");
        }

        private string[] GetShopMenuOptions()
        {
            string[] shopItems = _shop.GetItemNames();
            string[] menuOptions = new string[shopItems.Length + 2];

            for (int i = 0; i < shopItems.Length; i++)
            {
                menuOptions[i] = shopItems[i];
            }

            menuOptions[shopItems.Length] = "Save Game";
            menuOptions[shopItems.Length + 1] = "Quit Game";

            return menuOptions;
        }

        private void DisplayShopMenu()
        {
            string[] playerItemNames = _player.GetItemNames();

            Console.WriteLine($"Your gold: {_player.Gold}\n");
            Console.WriteLine("Your inventory:");
            for (int i = 0; i < playerItemNames.Length; i++)
            {
                Console.WriteLine(playerItemNames[i]);
            }
            Console.WriteLine();


            int inputReceived = GetInput("What would you like to purchase?", GetShopMenuOptions());

            if (inputReceived >= 0 && inputReceived < GetShopMenuOptions().Length - 2)
            {
                if (_shop.Sell(_player, inputReceived))
                {
                    Console.Clear();
                    Console.WriteLine($"You purchased the {_shop.GetItemNames()[inputReceived]}!");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You don't have enough gold for that.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }

            if (inputReceived == GetShopMenuOptions().Length - 2)
            {
                Console.Clear();
                Save();

                Console.WriteLine("Game saved successfully!");
                Console.ReadKey(true);
                Console.Clear();

            }
            if (inputReceived == GetShopMenuOptions().Length - 1)
            {
                _gameOver = true;
            }

        }
    }
}
