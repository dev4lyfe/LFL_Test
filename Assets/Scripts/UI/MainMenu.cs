using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text _highScore = null;
    [SerializeField] private Text _title = null;
    [SerializeField] private Text _version = null;


    // Start is called before the first frame update
    void Start()
    {
        //Going to use JSON data and not player prefs for everything in the main menu
        //_highScore.text = PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore").ToString() : 0.ToString();
        UpdateMainMenuData();
    }

    void UpdateMainMenuData()
    {
        object JSONobj = Resources.Load("GameJSONData/MainMenuJSON");
        var mainMenuData = JSON.Parse(JSONobj.ToString());

        if (mainMenuData == null)
        {
            Debug.LogWarning("JSON data for the main menu has not been assigned");
        }
        else
        {
            _version.text = mainMenuData["HighScore"].ToString();
            _highScore.text = mainMenuData["Version"].ToString();
            _title.text = mainMenuData["Title"].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
