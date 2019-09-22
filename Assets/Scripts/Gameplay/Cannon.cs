using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;


public class Cannon : MonoBehaviour
{
    [Header("Cannon Motion")]
    [SerializeField] private Transform _cannonTransform = null;
    [SerializeField] private Transform _cannonVerticalOffsetTransform = null;// I wanted to separate pivot transform from x-axis offset for my auto rotate fnc
    [SerializeField] private Transform _cannonballSpawnPoint = null;


    [Header("Cannon Firing")]
    [SerializeField] private GameObject _cannonballPrefab = null;

    //Data Driven Variables
    private float _rotationRate;
    private float _cannonballFireVelocity;
    private float _rateOfFire;

    private float _timeOfLastFire = 0.0f;

    //these variables allow the cannon to animate toward any of the spawners
    [SerializeField]
    Transform _spawner1, _spawner2, _spawner3;
    Quaternion _lookAngle1, _lookAngle2, _lookAngle3;
    Quaternion _startingCannonRotation;//used to handle the cannon's x-axis offset when animating using look rotation

    bool bLookToward1, bLookToward2, bLookToward3;//switches to handle animation toggling
    
    // Start is called before the first frame update
    void Start()
    {
        object JSONobj = Resources.Load("GameJSONData/CannonJSON");
        var gameplayData = JSON.Parse(JSONobj.ToString());

        _rotationRate = gameplayData["RotationRate"].AsFloat;
        _cannonballFireVelocity = gameplayData["CannonballFireVelocity"].AsFloat;
        _rateOfFire = gameplayData["RateOfFire"].AsFloat;

        _startingCannonRotation = _cannonTransform.rotation;

        FindObjectOfType<GameSession>().OnSessionEnd += () => { enabled = false; };

        Vector3 dir1 = (_spawner1.position - _cannonTransform.position);
        dir1.y = 0;
        _lookAngle1 = Quaternion.LookRotation(dir1, Vector3.up);

        Vector3 dir2 = (_spawner2.position - _cannonTransform.position);
        dir2.y = 0;
        _lookAngle2 = Quaternion.LookRotation(dir2);

        Vector3 dir3 = (_spawner3.position - _cannonTransform.position);
        dir3.y = 0;
        _lookAngle3 = Quaternion.LookRotation(dir3);

    }

    // Update is called once per frame
    void Update()
    {
        //cancel auto rotation 
        if(Input.anyKeyDown)
        {
            bLookToward1 = false;
            bLookToward2 = false;
            bLookToward3 = false;
        }
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

        //enable autorotation
        if (Input.GetKey(KeyCode.Alpha1))
        {
            bLookToward1 = true;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            bLookToward2 = true;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            bLookToward3 = true;
        }
        HandleAutoRotation();
    }

    void HandleAutoRotation()
    {
        if(bLookToward1)
            _cannonTransform.rotation = Quaternion.RotateTowards(_cannonTransform.rotation, _lookAngle1, 1f);
        if(bLookToward2)
            _cannonTransform.rotation = Quaternion.RotateTowards(_cannonTransform.rotation, _lookAngle2, 1f);
        if (bLookToward3)
            _cannonTransform.rotation = Quaternion.RotateTowards(_cannonTransform.rotation, _lookAngle3, 1f);
    }

    public void FireCannon()
    {
        if( Time.timeSinceLevelLoad > _timeOfLastFire + _rateOfFire )
        {
            var spawnedBall = GameObject.Instantiate( _cannonballPrefab, _cannonballSpawnPoint.transform.position, _cannonVerticalOffsetTransform.rotation);

            spawnedBall.GetComponent<Rigidbody>().AddForce(_cannonVerticalOffsetTransform.forward * _cannonballFireVelocity, ForceMode.Impulse );
            _timeOfLastFire = Time.timeSinceLevelLoad;
        }
    }
}
