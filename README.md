Changes:

The first change I made was importing SimpleJSON into the Plugins folder.

Created a folder in Resources called GameJSONData and added a file all of the JSON files to that.

Redid the layout in mainmenu and made all fields read from JSON apart from high score, which will read from playerprefs.

Added level loading functionality to MainMenu.cs.

Made all variables in gameplay section data driven by creating JSON objects for gameplay classes.

Made the EnemySpawner's use a timer to respawn the Enemy which is activated once Enemy is null. 

Added a singleton to GameSession so that enemies can call it to add score when they die.

Added in high score tracking using player prefs.

Finally I added in Cannon auto rotation to Cannon.cs and modified Cannon prefab transform hierarchy to make my method of rotation easier math wise. 

Project Structure:

I left the project structure more or less same apart from adding a subfolder to resources where I am storing all of the JSON files. I tried to follow the naming convention of writing member variables with an _ at the beginning.

I chose the auto rotation polish task.

Last bit of info - I found myself wishing that I had a more streamlined way of reading in the JSON info - I basically read it in in the start fnc of each relevant class, but found myself writing the same code a lot. That would be the first thing I would think about refactoring.
