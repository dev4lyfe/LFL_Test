using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;


public class GameSession : MonoBehaviour
{
    public System.Action OnSessionStart;
    public System.Action OnSessionEnd;

    public HUD hud;

    [HideInInspector]//since this will be data driven
    public float _timeLeft = 0;

    float _gameSessionScore = 0;

    //create singleton so enemy's can notify gamesession when they are killed
    public static GameSession s_instance;
    private void Awake()
    {
        if (!s_instance)
        {
            s_instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public enum SessionState
    {
        Paused,
        Active,
        Finished
    }

    private SessionState _state = SessionState.Paused;

    // Start is called before the first frame update
    void Start()
    {
        object JSONobj = Resources.Load("GameJSONData/GameSessionJSON");
        var gamesessionData = JSON.Parse(JSONobj.ToString());
        _timeLeft = gamesessionData["GameSessionLength"].AsFloat;
        StartSession();
    }

    public void LoadLevel(string inSceneName)
    {
        SceneManager.LoadScene(inSceneName);
    }

    public void AddScore(float val)
    {
        _gameSessionScore += val;
        if(hud)
        {
            hud.SetScoreText(_gameSessionScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( _state == SessionState.Active )
        {
            _timeLeft -= Time.deltaTime;
            
            if( _timeLeft <= 0 )
            {
                _timeLeft = 0;
                EndSession();
            }
        }
    }



    void StartSession()
    {
        _state = SessionState.Active;

        if( OnSessionStart != null )
        {
            OnSessionStart();
        }
    }

    void EndSession()
    {
        if( OnSessionEnd != null )
        {
            OnSessionEnd();
        }
    }
}
