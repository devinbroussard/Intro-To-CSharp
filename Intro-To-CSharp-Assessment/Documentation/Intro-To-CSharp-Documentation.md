## **Devin Broussard** ##
>s218014  
Introduction to C#  
Text Based Adventure Application  
 # **I. Requirements**  #

>**1. Description of Problem**  
>>**Name**: Text Based Adventure
>>  
>>**Problem Statement**:  
>>>Demonstrate competence in foundational data structures and file handling, array processing, and debugging.  
>>>
>>**Problem Specifictations:**  
>>> * Demonstrate basic language syntax rules and best practices
>>> * Demonstrate the application of arrays, including arrays of objects to introductory programming tasks.
>>> * Demonstrate readingand writing data, from and to, text files, and recoding the outcomes
>>> * Demonstrate implementation of classes, constructors, user-defined aggregation, inheritance, polymorphism, the adherence to organisation guidelines, documentation, and basic object-oriented design  
>>
>>**Description:**
>>> A text-based console application that has the player fight multiple enemies, earning gold and new items on the way to the final boss.

# **II. Design** #
>**1. System Archetecture:**
>> In this game, all gameplay is contained within the Game.cs file. 

>**2. Object Information**  
>> * **File:** Game.cs
>> * **Attributes:**   
>>>> * Name: _playerName
>>>>> * Description: Holds the player's name until the player object is initialized
>>>>> * Type: string
>>>> * Name: _gameOver 
>>>>> * Description: Holds whether or not the game is over
>>>>> * Type: Bool:
>>>> * Name: _currentScene
>>>>> * Description: Holds the current game scene
>>>>> * Type: Scene
>>>> * Name: _currentEnemy
>>>>> * Description: Holds the entity that is the current Enemy
>>>>> * Type: Entity
>>>> * Name: _currentEnemyIndex
>>>>> * Description: Holds the index of the current enemy in the _enemies array
>>>>> * Type: int
>>>> * Name: _shop
>>>>> * Description: The defined shop object
>>>>> * Type: Shop
>>>> * Name: _currentFloorTextIndex
>>>>> * Description: Holds the current floor the player is entering
>>>>> * Type: int
>>>> * Name: _floorText
>>>>> * Description: Used to display what floor the player is entering
>>>>> * Type: string[]
>>>> * Name: _knightItems
>>>>> * Description: Holds the knight's starting items
>>>>> * Type: Item[]
>>>> * Name: _assassinItems
>>>>> * Description: Holds the assassin's starting items
>>>>> * Type: Item[]
>>>> * Name: _wizardItems
>>>>> * Description: Holds the wizard's starting items
>>>>> * Type: Item[]
>>>> * Name: _shopItems
>>>>> * Description: Holds the items that the shop sells
>>>>> * Type: Item[]
>>>> * Name: _player
>>>>> * Description: The defined player object
>>>>> * Type: Player
>>>> * Name: Run() 
>>>>> * Description: Function that is called by the program to execute the game
>>>>> * Type: void
>>>> * Name: Start()
>>>>> * Description: Called at the start of the program to initialize variables
>>>>> * Type: void
>>>> * Name: Update() 
>>>>> * Description: Called continously in a while loop until game is over to update the display
>>>>> * Type: void
>>>> * Name: End()
>>>>> * Description: Called at the end of the game to give the player a goodbye message
>>>>> * Type: void
>>>> * Name: DisplayCurrentScene()
>>>>> * Description: contains a swich statement that changes the display based on the current scene
>>>> * Type: void
>>>> * Name: DisplayStartMenu() 
>>>>> * Description: Displays the start menu that asks the player whether they would like to start a new game or load
>>>>> * Type: void
>>>> * Name: DisplayBetweenBattles
>>>>> * Description: Displays the between battles menu where the player is asked what they want to do next
>>>>> * Type: void
>>>> * Name: DisplayEntranceScene()
>>>>> * Description: Tells the player that there is a shop near the front entrance, and that they are in front of it
>>>>> * Type: void
>>>> * Name: GetPlayerName()
>>>>> * Description: Asks and gets the player's input on what their name is
>>>>> * Type: void
>>>> * Name: GetPlayerClass()
>>>>> * Description: Asks and gets the player's input on which class they want to play
>>>>> * Type: void
>>>> * Name: InitializeItems()
>>>>> * Description: Initializes all of the items used in the game and stores them in arrays
>>>>> * Type: void
>>>> * Name: IntitializeEnemies()
>>>>> * Description: Initiailizes all of the eemies in the game and stores them in an array, also sets the current enemy to be the first index of the array
>>>>> * Type: void
>>>> * Name: GetInput(string description, params string[] options)
>>>>> * Description: Gets the player's input by asking the question in the description, and displaying the options to them
>>>>> * Type: int 
>>>> * Name: DisplayStats(Entity entity)
>>>>> * Description: Displays the stats of an entity
>>>>> * Type: void
>>>> * Name: DisplayStats(Player player)
>>>>> * Description: Displays the stats of the player; has additional stats such as the inventory and the equipped item
>>>>> * Type: void
>>>> * Name: Save()
>>>>> * Description: Saves the game and calls the save function from the player and the current enemy 
>>>>> * Type: void
>>>> * Name: Load()
>>>>> * Description: Loads the game and calls the load function from the player and the current enemy; returns true if successful
>>>>> * Type: bool 
>>>> * Name: DisplayBattle()
>>>>> * Description: Displays the battle between the player and the currentEnemy
>>>>> * Type: void
>>>> * Name: CheckBattleResults()
>>>>> * Description: Checks to see if there is a winner after every turn in the battle funciton
>>>>> * Type: void
>>>> * Name: TryEndGame()
>>>>> * Description: Checks to see if the player has defeated all of the enemies after a battle
>>>>> * Type: bool
>>>> * Name: DisplayEquipItemMenu()
>>>>> * Description: Menu for a player to equip or use items
>>>>> * Type: void
>>>> * Name: GetShopMenuOptions()
>>>>> * Description: Creates an array filled with the name of the shop items, as well as other options players have to choose in the shop menu
>>>>> * Type: string[]
>>>> * Name: DisplayShopMenu()
>>>>> * Description: Displays the shop menu to the player and asks for their input on what they want to do
>>>>> * Type: void
>>>> * Name: DisplayRestartMenu()
>>>>> * Description: Asks the player if they would like to restart, load, or quit the game
>>>>> * Type:  void
>> * **File:** Entity.cs
>> * **Attributes:**  
>>>> * Name: _name
>>>>> * Description: variable used to store the entity's name 
>>>>> * Type: string
>>>> * Name: _health
>>>>> * Description: variable used to store teh entity's current health 
>>>>> * Type: float
>>>> * Name: _attackPower 
>>>>> * Description: variable used to store the entity's current attack power
>>>>> * Type: flaot
>>>> * Name: _defensePower
>>>>> * Description: variable used to store the enity's current defense power
>>>>> * Type: float
>>>> * Name: _rewardMoney
>>>>> * Description: a variable used to store the gold the entity will give upon defeat
>>>>> * Type: 
>>>> * Name: Name
>>>>> * Description: public property used to read, but not set the _name variable 
>>>>> * Type: string
>>>> * Name: Health
>>>>> * Description: public property used to read, but not set the _health variable
>>>>> * Type: float
>>>> * Name: RewardMoney
>>>>> * Description: public property used to read, but not set the _rewardMoney variable
>>>>> * Type: int
>>>> * Name: AttackPower
>>>>> * Description: public virtual property allows for reading of _defensePower
>>>>> * Type: float
>>>> * Name: DefensePower
>>>>> * Description: public virtual property that allows for reading of _defensePower
>>>>> * Type: float
>>>> * Name: Entity(string name, float attackPower, float defensePower, int rewardMoney)
>>>>> * Description: Constructor that allows for setting entity stats
>>>>> * Type: constructor
>>>> * Name: Entity( string name, float health, float attackPower, float defensePower)
>>>>> * Description: Constructor that is used for enemies without rewardMoney
>>>>> * Type: constructor
>>>> * Name: TakeDamage(float DamageAmount)
>>>>> * Description: Calculates the damage taken to the entity and returns it
>>>>> * Type: float
>>>> * Name: Heal(float healthAmount)
>>>>> * Description: Heals the player by the amount specified in the parameter and returns the health increased
>>>>> * Type: float
>>>> * Name: Attack(Entity defender)
>>>>> * Description: takes in an entity and passes in their TakeDamage function and passes in the attackers attackpower; returns the damage taken
>>>>> * Type: float
>>>> * Name: Save(StreamWriter writer)
>>>>> * Description: virtual function that Saves the stats of the entity into a text file
>>>>> * Type: void
>>>> * Name: Load(StreamReader reader)
>>>>> * Description: Virtual function that loads the stats of the entity from a text file; returns true if successful
>>>>> * Type: bool
>> * **File:** Player.cs
>> * **Attributes:**  
>>>> * Name: _inventory
>>>>> * Description: array that holds the player's items
>>>>> * Type: Item[]
>>>> * Name: _currentItem
>>>>> * Description: Item that represents the player's currently selected item
>>>>> * Type: Item
>>>> * Name: _currentItemIndex
>>>>> * Description: the index of the current item in the player's inventory array
>>>>> * Type: int
>>>> * Name: _job
>>>>> * Description: a variable that stores the player's selected class 
>>>>> * Type: PlayerClass
>>>> * Name: Job
>>>>> * Description: public property that can read and set the _job variable 
>>>>> * Type: PlayerClass
>>>> * Name: AttackPower
>>>>> * Description: overriden propery that calculates the player's attackPower based on their current item
>>>>> * Type: float
>>>> * Name: DefensePower 
>>>>> * Description: overriden property that calcualtes the player's defense power based on their current item
>>>>> * Type: float
>>>> * Name: CurrentItem
>>>>> * Description: a public property used to read the _currentItem variable
>>>>> * Type:   Item
>>>> * Name: Gold 
>>>>> * Description: a public property used to read the _gold variable 
>>>>> * Type: itn
>>>> * Name: Player(string name, float health, float attackPower, float defensePower, Item[] inventory, PlayerClass job) : base(name, health, attackPower, defensePower)
>>>>> * Description: constructor that allows the user to set the stats and inventory of the player; uses the base entity's constructor as well
>>>>> * Type: constructor
>>>> * Name: Player(Item[] inventory)
>>>>> * Description: Sets the player's current item to nothing, and gives them an inventory
>>>>> * Type: constructor
>>>> * Name: TryEquipItem(int itemIndex)
>>>>> * Description: Tries to equip and item in the inventory based on the index given by the player, returns false if the equip was unsuccessful
>>>>> * Type: bool
>>>> * Name: TryRemoveCurrentItem()
>>>>> * Description: tries to unequip the current item, returns false if there is already no item equipped
>>>>> * Type: bool
>>>> * Name: GetItemNames()
>>>>> * Description: creates a string array of the item names of the inventory array and returns it
>>>>> * Type: string[]
>>>> * Name: Save(StreamWriter writer)
>>>>> * Description: Saves the player's stats, gold, and inventory; overriden from entity0
>>>>> * Type: void
>>>> * Name: Load(StreamReader reader)
>>>>> * Description: Loads the data stored by the Save function; returns true if successful
>>>>> * Type: bool
>>>> * Name: Buy(Item item)
>>>>> * Description: Function used to buy an item from the shop; called in the shop's sell function
>>>>> * Type: void
>> * **File:** Shop.cs
>> * **Attributes:**
>>>> * Name: _gold
>>>>> * Description: stores the shop's gold; used for debugging only 
>>>>> * Type: int
>>>> * Name: _inventory
>>>>> * Description: array that stores the inventory of the shop 
>>>>> * Type: Item[]
>>>> * Name: Shop(Item[] inventory})
>>>>> * Description: Called when an instance of a shop is called; Sets the shop's inventory to array passed in
>>>>> * Type: Constructor
>>>> * Name: Sell(Player player, int itemIndex)
>>>>> * Description: Sells and item to the player and calls the player's buy function; returns true if successful
>>>>> * Type: bool
>>>> * Name: GetItemNames()
>>>>> * Description: creates an array with each of the shop item's name and cost
>>>>> * Type: string[]
>>>> * Name: GetItemClasses()
>>>>> * Description: creates an array that stores the class needed to use each item
>>>>> * Type: string[]
>> * **File:** Structs-Enums.cs
>> * **Attributes:**
>>>> * Name: ItemType
>>>>> * Description: Holds the different types of items
>>>>> * Type: enum
>>>> * Name: PlayerClass
>>>>> * Description: Holds the different classes
>>>>> * Type: enum
>>>> * Name: Item
>>>>> * Description: creates a new item type that is given to all items 
>>>>> * Type: struct
>>>> * Name: Scene
>>>>> * Description: Holds the different game scenes 
>>>>> * Type: enum









        
