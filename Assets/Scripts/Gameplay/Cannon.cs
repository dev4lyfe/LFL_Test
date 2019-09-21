using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;


public class Cannon : MonoBehaviour
{
    [Header("Cannon Motion")]
    [SerializeField] private Transform _cannonTransform = null;
    [SerializeField] private Transform _cannonballSpawnPoint = null;


    [Header("Cannon Firing")]
    [SerializeField] private GameObject _cannonballPrefab = null;

    //Data Driven Variables
    private float _rotationRate;
    private float _cannonballFireVelocity;
    private float _rateOfFire;

    private float _timeOfLastFire = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        object JSONobj = Resources.Load("GameJSONData/CannonJSON");
        var gameplayData = JSON.Parse(JSONobj.ToString());

        _rotationRate = gameplayData["RotationRate"].AsFloat;
        _cannonballFireVelocity = gameplayData["CannonballFireVelocity"].AsFloat;
        _rateOfFire = gameplayData["RateOfFIre"].AsFloat;

        FindObjectOfType<GameSession>().OnSessionEnd += () => { enabled = false; };
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            FireCannon();
        }

        if( Input.GetKey( KeyCode.LeftArrow ) )
        {
            _cannonTransform.Rotate( 0.0f, -(Time.deltaTime * _rotationRate), 0.0f, Space.World );
        }

        if( Input.GetKey( KeyCode.RightArrow ) )
        {
            _cannonTransform.Rotate( 0.0f, Time.deltaTime * _rotationRate, 0.0f, Space.World );
        }
    }

    public void FireCannon()
    {
        if( Time.timeSinceLevelLoad > _timeOfLastFire + _rateOfFire )
        {
            var spawnedBall = GameObject.Instantiate( _cannonballPrefab, _cannonballSpawnPoint.transform.position, _cannonTransform.rotation);

            spawnedBall.GetComponent<Rigidbody>().AddForce( _cannonTransform.forward * _cannonballFireVelocity, ForceMode.Impulse );
            _timeOfLastFire = Time.timeSinceLevelLoad;
        }
    }
}
