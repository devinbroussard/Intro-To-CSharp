using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Text_Based_Adventure
{

    class Game
    {
        //Holds the players name until it is initialized
        private string _playerName;
        //Holds if the game is over or not
        private bool _gameOver;
        //Holds the current scene
        private Scene _currentScene;
        //Holds the current enemy;
        private Entity _currentEnemy;
        //Holds the index of the current enemy in the _enemies array
        private int _currentEnemyIndex;
        //Holds all of the enemies
        private Entity[] _enemies;
        //Defining the shop
        private Shop _shop;
        //Holds the current floor, used for the _floorText array
        private int _currentFloorTextIndex;
        //Holds the text that tells players what floor they are entering
        private string[] _floorText = { "First", "Second", "Third", "Fourth", "Fifth and Final" };

        //Defining arrays containing entity/shop items
        private Item[] _knightItems;
        private Item[] _assassinItems;
        private Item[] _wizardItems;
        private Item[] _shopItems;


        //Defining player
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
            //Initializing variables, enemies, and items
            _gameOver = false;
            _currentFloorTextIndex = 0;
            _currentScene = Scene.STARTMENU;
            InitializeEnemies();
            InitializeItems();
        }

        /// <summary>
        /// Function that is continiously called until the game is over
        /// </summary>
        private void Update()
        {
            //Displays the current scene
            DisplayCurrentScene();
        }

        /// <summary>
        /// Function that is called at the end of the program
        /// </summary>
        private void End()
        {
            //A goodbye message for the player
            Console.WriteLine("Goodbye, player!");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Called to display a current scene, can be updated with the _currentScene variable
        /// </summary>
        void DisplayCurrentScene()
        {
            //Switch statement that changes the display based on the current scene variable
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
                    DisplayBattle();
                    CheckBattleResults();
                    break;
                case Scene.BETWEENBATTLES:
                    DisplayBetweenBattles();
                    break;
                case Scene.SHOP:
                    DisplayShopMenu();
                    break;
                case Scene.RESTARTMENU:
                    DisplayRestartMenu();
                    break;

            }
        }

        /// <summary>
        /// Used as the welcome screen
        /// </summary>
        void DisplayStartMenu()
        {
            //Asks and gets the player's input on if they want to start a new game, or load
            Console.WriteLine("Hello player! In this game you will fight enemies to reach the top of a tower!");
            int choice = GetInput("Would you like to start a new game, or load an existing save file?", "Start new game", "Load existing game");

            //Transitions to the next scene if they want to start a new game
            if (choice == 0)
                _currentScene = Scene.GETPLAYERNAME;

            //Loads the game if they want to load
            if (choice == 1)
                Load();
        }

        /// <summary>
        /// Asks the player what they would like to do between battles
        /// </summary>
        private void DisplayBetweenBattles()
        {
            //Gets the player input on what they want to do
            int choice = GetInput("What would you like to do?", "Go to the next floor", "Go to the shop", "Save Game", "Quit Game");

            //If they want to go to the next floor..
            if (choice == 0)
            {
                //Tell the player what floor they are going to...
                Console.WriteLine($"You enter the {_floorText[_currentFloorTextIndex]} floor.\n");
                //...Change the current floor 
                _currentFloorTextIndex++;

                //...Tell the player what enemy they are fighting...
                Console.WriteLine($"You run in to a {_currentEnemy.Name}!");
                Console.ReadKey(true);
                Console.Clear();

                //Change the game to the battle scene
                _currentScene = Scene.BATTLE;
            }

            //If they want to go to the shop...
            if (choice == 1)
            {
                //...Tell the player they are going to the shop
                Console.WriteLine("You decide to head to the shop");
                Console.ReadKey(true);
                Console.Clear();
                //...and change the current scene to the shop scene
                _currentScene = Scene.SHOP;
            }

            //If they want to save the game...
            else if (choice == 2)
            {
                //Call the save function...
                Save();

                //...And tell the player they changed the game
                Console.WriteLine("Game saved successfully!");
                Console.ReadKey(true);
                Console.Clear();

            }

            //If they want to quit the game...
            else if (choice == 3)
            {
                //Change the gameover variable to true, ending the while loop in the update function
                _gameOver = true;
            }
        }

        /// <summary>
        /// Transitions from the welcome menu into the game
        /// </summary>
        private void DisplayEntranceScene()
        {
            //Tells the player that there is a shop near the front entrance, and that they are in front of the tower
            Console.WriteLine("You approach the tower and notice there is a shop near the front entrance...");
            Console.ReadKey(true);
            Console.Clear();

            //Sets the current scene to the between battles scene, where the player can select what they want to do next
            _currentScene = Scene.BETWEENBATTLES;
        }

        /// <summary>
        /// Used to ask the player for their name
        /// </summary>
        void GetPlayerName()
        {
            //Asks and gets the player's name
            Console.Clear();
            Console.WriteLine("Please enter your name:");
            Console.Write("> ");
            _playerName = Console.ReadLine();

            //Asks the player to confirm their name
            int choice = GetInput($"\nAre you sure {_playerName} is your name?", "Yes", "No");
            
            //If their choice is yes, then progresses to the next scene
            if (choice == 0)
                _currentScene = Scene.GETPLAYERCLASS;

            //If the choice is no, the while loop will cause the scene to restart, asking for their name again
        }

        /// <summary>
        /// Asks the player to select a class and assigns their stats accordingly
        /// </summary>
        private void GetPlayerClass()
        {
            //Asks and gets the player input on which class they want to play
            int choice = GetInput($"Okay {_playerName}, select a class:", "Knight", "Wizard", "Assassin");

            //Creates a new instance of the player with stats according that of which the player selects
            if (choice == 0)
                _player = new Player(_playerName, 100, 25, 50,  _knightItems, PlayerClass.KNIGHT);
            else if (choice == 1)
                _player = new Player(_playerName, 100, 80, 0, _wizardItems, PlayerClass.WIZARD);
            else if (choice == 2)
                _player = new Player(_playerName, 100, 60, 25, _assassinItems, PlayerClass.ASSASSIN);

            _currentScene = Scene.ENTRANCE;
        }

        /// <summary>
        /// Initializes all the used items in the game
        /// </summary>
        private void InitializeItems()
        {
            //Initializing shop items that can be sold to the player
            Item healthPotion = new Item { Name = "Health Potion", StatBoost = 50, EquipType = ItemType.HEALING, Cost = 50 };
            Item ironShield = new Item { Name = "Iron Shield", StatBoost = 40, EquipType = ItemType.DEFENSE, Cost = 200, ClassType = PlayerClass.KNIGHT };
            Item longDagger = new Item { Name = "Long Dagger", StatBoost = 60, EquipType = ItemType.ATTACK, Cost = 200, ClassType = PlayerClass.ASSASSIN };
            Item enchantedWand = new Item { Name = "Enchanted Wand", StatBoost = 75, EquipType = ItemType.ATTACK, Cost = 200, ClassType = PlayerClass.WIZARD };

            //Initializing start item for the knight, assassin, and wizard class, respectively
            Item woodenShield = new Item { Name = "Wooden Shield", StatBoost = 30, EquipType = ItemType.DEFENSE, Cost = 50 };
            Item shortDagger = new Item { Name = "Short Dagger", StatBoost = 40, EquipType = ItemType.ATTACK, Cost = 50 };
            Item basicWand = new Item { Name = "Basic Wand", StatBoost = 60, EquipType = ItemType.ATTACK, Cost = 50 };

            //Defining the class-specific item arrays that allow for the items to be accessed anywhere in the game class
            _knightItems = new Item[] { woodenShield };
            _assassinItems = new Item[] { shortDagger };
            _wizardItems = new Item[] { basicWand };
            _shopItems = new Item[] {ironShield, longDagger, enchantedWand, healthPotion };

            //Creates a new instance of the shop with the shop items initialized before
            _shop = new Shop(_shopItems);
        }

        /// <summary>
        /// Initializes all of the enemies fought in the game
        /// </summary>
        private void InitializeEnemies()
        {
            //Initializing enemies that will be fought later
            Entity slime = new Entity("Slime", 10, 10, 50);
            Entity skeleton = new Entity("Skeleton", 30, 20, 75);
            Entity cursedMannequin = new Entity("Cursed Mannequin", 20, 10, 150);
            Entity darkKnight = new Entity("Dark Knight", 60, 20, 150);
            Entity finalBoss = new Entity("Final Boss", 95, 30, 1000);

            //Sets the current enemy index for the _enemies array to 0
            _currentEnemyIndex = 0;
            //Stores the enemies in the _enemies arary so that they can be accessed anywhere in the game class
            _enemies = new Entity[] { slime, skeleton, cursedMannequin, darkKnight, finalBoss };
            //sets the current enemy to be that of the currentEnemyIndex in the enemies array
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
            //Defining variables used in function
            string input;
            int inputReceived;

            //Prints the question asked to the player
            Console.WriteLine($"{description}\n");

            //Loops through the options available to the player and prints them
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
            //If the player didn't type an int
            else
            {
                //tells the player that they gave invalid input and sets inputReceived to -1 so that the scene loops again
                inputReceived = -1;
                Console.WriteLine("\nInvalid input.");
                Console.ReadKey(true);
            }

            Console.Clear();

            return inputReceived;
        }

        /// <summary>
        /// Overloaded fucntion that displays the stats of an enemy
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
        /// Overloaded function that displays the stats of the player
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
            writer.WriteLine(_currentScene);
            writer.WriteLine(_currentFloorTextIndex);

            //calls the save function from both the player and current enemy
            _player.Save(writer);
            _currentEnemy.Save(writer);

            //closes the writer
            writer.Close();
        }

        /// <summary>
        /// Function used to load the game from the SaveData.txt file
        /// </summary>
        /// <returns>True if the load is successful</returns>
        private bool Load()
        {
            //crating a variable we can easily return true or false
            bool loadSuccessful = true;
            //defining a string to store the player class
            PlayerClass playerJob;

            //creating a new StreamReader
            StreamReader reader = new StreamReader("SaveData.txt");

            //Reads the current line in the SaveData.txt file, and tries to convert it to the required type.
            //If false, changes the loadSuccessful variable to false, and if true, loads the variables data from the savefile
            if (!int.TryParse(reader.ReadLine(), out _currentEnemyIndex))
                loadSuccessful = false;
            if (!Enum.TryParse<Scene>(reader.ReadLine(), out _currentScene))
                return false;
            if (!int.TryParse(reader.ReadLine(), out _currentFloorTextIndex))
                loadSuccessful = false;

            //Does the same thing as the previous lines but outputs to the playerJob variable
            if (!Enum.TryParse<PlayerClass>(reader.ReadLine(), out playerJob))
                return false;

            //Creates a new instance of the player and sets the player's job to be the data loaded into the playerJob variable
            if (playerJob == PlayerClass.KNIGHT)
            {
                _player = new Player(_knightItems);
                _player.Job = PlayerClass.KNIGHT;
            }
            else if (playerJob == PlayerClass.WIZARD)
            {
                _player = new Player(_wizardItems);
                _player.Job = PlayerClass.WIZARD;
            }
            else if (playerJob == PlayerClass.ASSASSIN)
            {
                _player = new Player(_assassinItems);
                _player.Job = PlayerClass.ASSASSIN;
            }

            //Calls the load function of the player and current enemy, and changes loadSuccessful to false if they return false
            if (!_player.Load(reader))
                loadSuccessful = false;
            if (!_currentEnemy.Load(reader))
                loadSuccessful = false;

            //Closes the reader
            reader.Close();

            //returns the loadSuccessful variable, returns true unless one of the loads failed
            return loadSuccessful;
        }

        /// <summary>
        /// Function used to start a battle between the player and the current enemy
        /// </summary>
        private void DisplayBattle()
        {
            //Displays the stats of both the player, and the current enemy
            DisplayStats(_player);
            DisplayStats(_currentEnemy);

            //Gets the players input on what they want to do, and returns that input in the form of an int
            int choice = GetInput($"A {_currentEnemy.Name} stands in front of you! What will you do:", "Attack", "Equip Item", "Remove Current Item", "Save Game", "Quit Game");

            Console.Clear();

            //If the player chose to attack...
            if (choice == 0)
            {
                //..Calls the attack function from the player and passes in the enemy
                float damage = _player.Attack(_currentEnemy);
                //...Displays the damage dealt by the player
                Console.WriteLine($"You dealt {damage} damage!");

                //...Calls the attack function from the enemy and passes in the player
                damage = _currentEnemy.Attack(_player);
                //..Displays the damage done by the enemy
                Console.WriteLine($"The {_currentEnemy.Name} dealt {damage} damage!");
                Console.ReadKey(true);
                Console.Clear();

            }

            //If the player chose to equip item...
            else if (choice == 1)
            {
                //..Displays the equip item menu
                DisplayEquipItemMenu();
            }

            //If t he player chose to remove their current item...
            else if (choice == 2)
            {
                //Calls the player's tryremovecurrentitem function
                if (!_player.TryRemoveCurrentItem())
                    //Tells the player they have nothing equipped if the function fails
                    Console.WriteLine("You don't have anything equipped.");
                //If the function worked...
                else
                    //Tells the player that the item was stored in their inventory
                    Console.WriteLine("You placed the item in your bag.");
                Console.ReadKey(true);
                Console.Clear();
            }

            //If the player chose to save the game...
            else if (choice == 3)
            {
                //...Calls the save function
                Save();
                //...Tells the player that the save was successful
                Console.WriteLine("Game saved successfully!");
                Console.ReadKey(true);
                Console.Clear();
            }

            //if the player chose to quit the game...
            else if (choice == 4)
            {
                //...Changes the gameover variable to be true
                _gameOver = true;
                return;
            }


        }

        /// <summary>
        /// Checks the outcome of the battle and assigns a new enemy or ends the game
        /// </summary>
        private void CheckBattleResults()
        {
            //If the player is dead...
            if (_player.Health == 0)
            {
                //Tells the player that they have died...
                Console.WriteLine("You died!");
                Console.ReadKey(true);
                Console.Clear();

                //Changes the current scene to be that of the restart menu
                _currentScene = Scene.RESTARTMENU;
            }

            //If the player isn't dead and the current enemy is dead...
            else if (_currentEnemy.Health == 0)
            {
                //Tells the player that they have slain the enemy, and collects/tells the player how much reward money they collected
                Console.WriteLine($"You slayed the {_currentEnemy.Name} and collected {_player.GetRewardMoney(_currentEnemy)} gold!\n");
                Console.WriteLine($"Current Gold: {_player.Gold}");
                Console.ReadKey(true);
                Console.Clear();
                //Adds to the current enemy idnex
                _currentEnemyIndex++;

                //Checks to see if the player has reached the end of the game, and returns if so
                if (TryEndGame())
                    return;

                //Sets the current enemy to be that of the currentenemyidnex
                _currentEnemy = _enemies[_currentEnemyIndex];

                //changes the current scene to be the between battles one
                _currentScene = Scene.BETWEENBATTLES;
            }
        }
        
        /// <summary>
        /// Called to check if the player has reached the end of the game
        /// </summary>
        /// <returns></returns>
        private bool TryEndGame()
        {
            //Creates a bool that represents if the current enemy index has reached the end of the enemies array
            bool gameOver = _currentEnemyIndex >= _enemies.Length;
            //Creates a bool that represents if the player has reached the final index in the array
            bool fightBoss = _currentEnemyIndex == _enemies.Length - 1;

            //If gameover is true...
            if (gameOver)
            { 
                //..Tells the player that they have reached the end of the game
                Console.WriteLine("Congradulations, you have made it to the top of the tower! You are the best!");
                Console.ReadKey(true);
                Console.Clear();

                //Change the scene to the restart menu
                _currentScene = Scene.RESTARTMENU;
            }

            //If fightboss variable is true..
            if (fightBoss)
            {
                //Tells the player that the boss is on the next floor
                Console.WriteLine("You have reached the second highest floor. The final boss awaits you.");
                Console.ReadKey(true);
                Console.Clear();

                //Changes the scene to the between battles one
                _currentScene = Scene.BETWEENBATTLES;
            }

            return gameOver;
        }
        /// <summary>
        /// Allows the player to select an item to equip out of the player inventory
        /// </summary>
        private void DisplayEquipItemMenu()
        
        {
            //Gets the player's input on which item they want to select
            int choice = GetInput("Select an item to equip.", _player.GetItemNames());

            //If the player's choice was in the acceptable range of the options...
            if (choice >= 0 && choice < _player.GetItemNames().Length)
            {
                //If the player's choice was a health potion, and the equip item function return true,
                if (_player.GetItemNames()[choice] == "Health Potion" && _player.TryEquipItem(choice) )
                {
                    //Tell the player they used a health potion
                    Console.WriteLine("You used a health potion!");
                    Console.ReadKey(true);
                    Console.Clear();
                    return;
                }
                //Else if the player didn't use a health potion, but the equip item function returned true,
                else if (_player.TryEquipItem(choice))
                    //Tell the player that they equipped the item they selected
                    Console.WriteLine($"You equipped the {_player.CurrentItem.Name}");
            }
            //else if the player's choice wasn't in the list of items...
            else
            {
                //...Tell the player that the item could not be found
                Console.WriteLine("You couldn't find that item in your bag.");
            }
            Console.ReadKey(true);
            Console.Clear();

        }

        /// <summary>
        /// Creates an array filled with the shop menu options
        /// </summary>
        /// <returns>An array filled with the names of the shop items, and the "leave shop", "save game", and "quit game" options at the end</returns>
        private string[] GetShopMenuOptions()
        {
            //Creates a new array of the shop item names using the shop's GetItemNames function that returns a string array
            string[] shopItems = _shop.GetItemNames();
            //Creates a new array that will be used for the menu options that is three slots longer so that the last three options will fit
            string[] menuOptions = new string[shopItems.Length + 3];

            //For each slot in the menuOptions array up until the shopItems length...
            for (int i = 0; i < shopItems.Length; i++)
            {
                //..Make the menuOptions equal to the shopItems names
                menuOptions[i] = shopItems[i];
            }

            //Adds the last three options at the end of the array
            menuOptions[shopItems.Length] = "Leave Shop";
            menuOptions[shopItems.Length + 1] = "Save Game";
            menuOptions[shopItems.Length + 2] = "Quit Game";

            //Returns the array with the item names, and the last three options
            return menuOptions;
        }

        /// <summary>
        /// Function used for displaying the shop menu
        /// </summary>
        private void DisplayShopMenu()
        {
            //Gets the player's item names and stores them in an array using the player's getItemNames function
            string[] playerItemNames = _player.GetItemNames();
            //Gets the class of the items in the shop inventory using the shop's GetItemClasses function
            string[] shopItemClasses = _shop.GetItemClasses();

            //Displays the player's health, gold, and inventory
            Console.WriteLine($"Your Health: {_player.Health}");
            Console.WriteLine($"Your gold: {_player.Gold}\n");
            Console.WriteLine("Your inventory:");
            //For each name in the player's item names...
            for (int i = 0; i < playerItemNames.Length; i++)
            {
                //...Print the name of the item
                Console.WriteLine(playerItemNames[i]);
            }
            Console.WriteLine();

            //Asks the player what they would like to purchase and prints the shop options
            int choice = GetInput("What would you like to purchase?", GetShopMenuOptions());

            //If the player's choice wasn't one of the class specific options or one of the non-item options...
            if (choice >= 3 && choice < GetShopMenuOptions().Length - 3)
            {
                Console.Clear();
                //..and if the player had enough gold for the itemm...
                if (_shop.Sell(_player, choice))
                {
                    //Tell the player that they purchased their selected item
                    Console.WriteLine($"You purchased the {_shop.GetItemNames()[choice]}!");
                }
                //...otherwise if the player didn't have enough gold for the item
                else
                {
                    //Tell the player that they didn't have enough gold
                    Console.WriteLine("You don't have enough gold for that.");
                }

                Console.ReadKey(true);
                Console.Clear();
            }

            //Otherwise if the choice was one of the class-specific items...
            else if (choice >= 0 && choice < GetShopMenuOptions().Length - 3)
            {
                //...and if the player's class selected their player choice
                if (shopItemClasses[choice] == $"{_player.Job}")
                {
                    //...and if the player had enough gold for the item...
                    if (_shop.Sell(_player, choice))
                    {
                        //Tell the player that they purchased the item
                        Console.Clear();
                        Console.WriteLine($"You purchased the {_shop.GetItemNames()[choice]}!");
                    }
                    //otherwise tell the player that they didn't have enough gold for the item
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("You don't have enough gold for that.");
                    }
                }
                //otherwise tell the player that they aren't the correct class to use that item
                else
                {
                   
                    Console.WriteLine($"You would have to be a {shopItemClasses[choice]} to use that...");
                }
                Console.ReadKey(true);
                Console.Clear();

            }

            //If the player selected the leave shop items...
            else if (choice == GetShopMenuOptions().Length - 3)
            {
                //TEll the player that they left the shop
                Console.WriteLine("You leave the shop...");
                Console.ReadKey(true);
                Console.Clear();

                //Change the current scene to the between battles one
                _currentScene = Scene.BETWEENBATTLES;
            }

            //If the player selected the save game options...
            else if (choice == GetShopMenuOptions().Length - 2)
            {
                //..save the game..
                Save();

                //..and tell the player that they successfully saved the game
                Console.WriteLine("Game saved successfully!");
                Console.ReadKey(true);
                Console.Clear();

            }
            
            //If the player selected the quit game option...
            else if (choice == GetShopMenuOptions().Length - 1)
            {
                //...change the game over variable to be true
                _gameOver = true;
            }
        }
        
        /// <summary>
        /// Displays the restart menu
        /// </summary>
        private void DisplayRestartMenu()
        {
            //Asks the player if they would like to load the game, restart the game, or quit the game, and gets their input
            int choice = GetInput("The game is over. What would you like to do?", "Load Game", "Restart Game",  "Quit Game");

            //If they would like to load the game...
            if (choice == 0)
            {
                //...if the load fails...
                if (!Load())
                {
                    //...tells the player that the load fails
                    Console.WriteLine("Load failed!");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }

            //...If they choose to restart the game...
            if (choice == 1)
            {
                //..Reinitializes the enemies...
                InitializeEnemies();
                //..Sets the current floor text index to 0...
                _currentFloorTextIndex = 0;
                //..and sets the current scene to 0
                _currentScene = 0;
            }

            //If the player chose to quit the game...
            if (choice == 2)
            {
                //Sets the game over variable to be true
                _gameOver = true;
            }
        }
    }
}
