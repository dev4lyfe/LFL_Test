using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text _titleText;
    [SerializeField] private Text _highScoreText;
    [SerializeField] private Text _highScoreValue;
    [SerializeField] private Text _versionText;
    [SerializeField] private Text _versionNumber;
    [SerializeField] private Text _startButtonText;

    // Start is called before the first frame update
    void Start()
    {
        //Read in JSON data for main menu
        if(_highScoreText) _highScoreText.text = PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore").ToString() : 0.ToString();
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
            if (_titleText) _titleText.text = mainMenuData["TitleText"].Value;
            if (_highScoreText) _highScoreText.text = mainMenuData["HighScoreText"].Value;
            if (_versionText) _versionText.text = mainMenuData["VersionText"].Value;
            if(_versionNumber) _versionNumber.text = mainMenuData["VersionNumber"].Value;
            if (_startButtonText) _startButtonText.text = mainMenuData["StartButtonText"].Value;

        }
    }

    public void LoadLevel(string inSceneName)
    {
        SceneManager.LoadScene(inSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
