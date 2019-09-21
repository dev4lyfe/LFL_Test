The first change I made was importing SimpleJSON.

Then I created a folder in Resources called GameJSONData and added a file called MainMenuJSON to that.

I modified MainMenu.cs to read JSON instead of using player prefs, then assigned the values from the JSON to UI text objects.

Next I redid the layout in mainmenu and made all fields read from JSON apart from high score, which will read from playerprefs.