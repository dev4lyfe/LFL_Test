using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Data Driven Values
    [HideInInspector]
    public int _scoreValue;
    private float _verticalAmplitude;
    private float _verticalFrequency;

    [Header("Physics")]
    [SerializeField] private Rigidbody _rigidBody = null;

    private Vector3 _startPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        object JSONobj = Resources.Load("GameJSONData/EnemyJSON");
        var enemyData = JSON.Parse(JSONobj.ToString());

        _scoreValue = enemyData["ScoreValue"].AsInt;
        _verticalAmplitude = enemyData["VerticalAmplitude"].AsFloat;
        _verticalFrequency = enemyData["VerticalFrequency"].AsFloat;
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float positionOffset = Mathf.Sin( Time.timeSinceLevelLoad / _verticalFrequency ) * _verticalAmplitude;
        transform.position = new Vector3( _startPosition.x, _startPosition.y + positionOffset, _startPosition.z );
    }

    void Die()
    {
        _rigidBody.useGravity = true;
        GameSession.s_instance.AddScore(_scoreValue);
        Destroy(this);
    }

    void OnCollisionEnter( Collision collision )
    {
        if( collision.gameObject.GetComponent<Cannonball>() )
        {
            _rigidBody.AddForceAtPosition(collision.transform.forward, collision.GetContact(0).point, ForceMode.Impulse);
            Die();
        }
    }
}
