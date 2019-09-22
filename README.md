The first change I made was importing SimpleJSON.

Then I created a folder in Resources called GameJSONData and added a file called MainMenuJSON to that.

Next I redid the layout in mainmenu and made all fields read from JSON apart from high score, which will read from playerprefs.

Next I added level loading functionality to MainMenu.cs.

Next I made all variables in gameplay section data driven by creating JSON objects for gameplay classes.

Next I made the EnemySpawner's use a timer to respawn the Enemy which is activated once Enemy is null. 

Added a singleton to GameSession so that enemies can call it to add score when they die.

Nexted I added in high score tracking using player prefs.

Added in Cannon auto rotation to Cannon.cs and modified Cannon prefab transform hierarchy to make my method of rotation easier math wise. 