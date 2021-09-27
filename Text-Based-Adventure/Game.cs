using System;
using System.Collections.Generic;
using System.Text;

namespace Text_Based_Adventure
{
   
    class Game
    {
        //Initializing variables
        private string _playerName;
        private bool _gameOver;
        private Scene _currentScene;
        private Entity _currentEnemy;
        private int _currentEnemyIndex;
        private Entity[] _enemies;
        private Item[] _knightItems;
        private Item[] _assassinItems;
        private Item[] _wizardItems;
        private Item[] _shopItems;
        Player _player;
        
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
            Utilties.WriteRead("Goodbye, player!");
        }

        void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case Scene.STARTMENU:
                    DisplayStartMenu();
                    break;
                case Scene.ENTRANCE:

                    break;
                case Scene.GETPLAYERNAME:
                    GetPlayerName();
                    break;
                case Scene.GETPLAYERCLASS:
                    GetPlayerClass();
                    break;
            }
        }

        void DisplayStartMenu()
        {
            Console.WriteLine("Hello player! Welcome to this text-based adventure game! During this game you will climb a tower while fighting enemies on the way to reach the final boss at the top!");
            int choice = GetInput("Would you like to start a new game, or load an existing one?", "Start new game", "Load existing game");

            if (choice == 0)
                _currentScene = Scene.GETPLAYERNAME;

            if (choice == 1)
                Load();
        }

        private void DisplayEntranceScene()
        {
            Console.WriteLine(
        }

        void GetPlayerName()
        {
            Console.Clear();
            Console.WriteLine("Please enter your name:");
            _playerName = Console.ReadLine();
            Console.Clear();

            int choice = GetInput($"Hmm... Are you sure {_playerName} is your name?", "Yes", "No");

            if (choice == 0)
                _currentScene = Scene.GETPLAYERCLASS;
        }

        private void GetPlayerClass()
        {
            int choice = GetInput($"Okay {_playerName}, select a class:", "Knight", "Wizard", "Assassin");

            if (choice == 0)
                _player = new Player(_playerName, 100, 20, 40, _knightItems, PlayerClass.KNIGHT);
            if (choice == 1)
                _player = new Player(_playerName, 100, 60, 0, _wizardItems, PlayerClass.WIZARD);
            if (choice == 2)
                _player = new Player(_playerName, 100, 40, 20, _assassinItems, PlayerClass.ASSASSIN);

        }

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
        }

        private void InitializeEnemies()
        {
            Entity slime = new Entity("Slime", 10, 10, 25);
            Entity skeleton = new Entity("Skeleton", 30, 20, 55);
            Entity darkKnight = new Entity("Dark Knight", 50, 50, 80);
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

        private void DisplayStats(Entity entity)
        {
            Console.WriteLine("Enemy Stats:\n");
            Console.WriteLine($"Name: {entity.Name}");
            Console.WriteLine($"Health: {entity.Health}");
            Console.WriteLine($"Attack Power: {entity.AttackPower}");
            Console.WriteLine($"Defense Power: {entity.DefensePower}");
        }

        private void DisplayStats(Player player)
        {
            Console.WriteLine("Your Stats:\n");
            Console.WriteLine($"Name: {player.Name}");
            Console.WriteLine($"Health: {player.Health}");
            Console.WriteLine($"Attack Power: {player.AttackPower}");
            Console.WriteLine($"Defense Power: {player.DefensePower}");
            Console.WriteLine($"Equipped Item: {player.CurrentItem.Name}");
        }
        private bool Load()
        {
            return true;
        }
    }
}
