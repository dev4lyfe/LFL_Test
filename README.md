The first change I made was importing SimpleJSON.

Then I created a folder in Resources called GameJSONData and added a file called MainMenuJSON to that.

Next I redid the layout in mainmenu and made all fields read from JSON apart from high score, which will read from playerprefs.

Next I added level loading functionality to MainMenu.cs.

Next I made all variables in gameplay section data driven by creating JSON objects for gameplay classes.