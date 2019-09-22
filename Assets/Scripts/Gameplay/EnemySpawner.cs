using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint = null;

    [SerializeField] private Enemy _enemyPrefab = null;

    private Enemy _lastSpawned = null;
    private GameObject _lastSpawnedGO;
    float _respawnTime;
    float _respawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        object JSONobj = Resources.Load("GameJSONData/EnemySpawnerJSON");
        var enemySpawnerData = JSON.Parse(JSONobj.ToString());
        _respawnTime = enemySpawnerData["RespawnTime"].AsFloat;
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        _lastSpawned = Instantiate( _enemyPrefab, _spawnPoint.position, _spawnPoint.rotation );
        _lastSpawnedGO = _lastSpawned.gameObject;
        _respawnTimer = _respawnTime;
    }

    private void Update()
    {
        if(_lastSpawned == null)
        {
            if(_respawnTimer > 0)
            {
                _respawnTimer -= Time.deltaTime;
            }
            else
            {
                Destroy(_lastSpawnedGO);//clean up dead pumpkin
                SpawnEnemy();
            }
        }

    }
}
