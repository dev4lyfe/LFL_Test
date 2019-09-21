using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;


public class GameSession : MonoBehaviour
{
    public System.Action OnSessionStart;
    public System.Action OnSessionEnd;

    [HideInInspector]//since this will be data driven
    public float timeLeft = 0;

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
        timeLeft = gamesessionData["GameSessionLength"].AsFloat;
        StartSession();
    }

    public void LoadLevel(string inSceneName)
    {
        SceneManager.LoadScene(inSceneName);
    }

    // Update is called once per frame
    void Update()
    {
        if( _state == SessionState.Active )
        {
            timeLeft -= Time.deltaTime;
            
            if( timeLeft <= 0 )
            {
                timeLeft = 0;
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
