using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPhase :IGamePhase
{
    private GhostPool _pool;
    private Transform _spawnPoint;

    public float Duration => 15f;
    private const float kSpawnInterval = 5f;
    private float _spawnTimer;

    private readonly SkyboxColorTransitioner _skybox;


  
    public GhostPhase(GhostPool pool, Transform spawnPoint,SkyboxColorTransitioner skybox)
    {
        _pool = pool;
        _spawnPoint = spawnPoint;
        _skybox = skybox;
    }

    public void Enter()
    {
        _skybox.TransitionToHex("#A5F3FC", 2f);
      
    }

    public void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= kSpawnInterval)
        {
            _spawnTimer -= kSpawnInterval;
            SpawnGhost();
        }
    }

    public void Exit()
    {
        _skybox.TransitionToHex("#245A9B", 1.5f);

    }

    private void SpawnGhost()
    {
        if (_pool == null || _spawnPoint == null) return;

        GameObject ghost = _pool.GetRandomGhost();
        if (ghost == null) return;

        ghost.transform.position = _spawnPoint.position + Vector3.up * 2.0f;
        ghost.transform.rotation = Quaternion.identity;
    }
}
