using UnityEngine;

public class ZombiePhase : IGamePhase
{
    private readonly ZombiePool _pool;
    private readonly Transform _spawnPoint;

    public float Duration => 15f; 

    private readonly SkyboxColorTransitioner _skybox;


    private const float kSpawnInterval = 5f;
    private float _spawnTimer;

    public ZombiePhase(ZombiePool pool, Transform spawnPoint, SkyboxColorTransitioner skybox)
    {
        _pool = pool;
        _spawnPoint = spawnPoint;
        _skybox = skybox;
    }

    public void Enter()
    {
        _skybox.TransitionToHex("#1E3A1E", 2f);


        _spawnTimer = 0f;
    }

    public void Update()
    {

        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= kSpawnInterval)
        {
            _spawnTimer -= kSpawnInterval;
            SpawnZombie();
        }
    }

    public void Exit()
    {
        _skybox.TransitionToHex("#245A9B", 1.5f);
    }


    private void SpawnZombie()
    {
        if (_pool == null || _spawnPoint == null) return;

        GameObject zombie = _pool.GetZombie();
        if (zombie == null) return;

        zombie.transform.position = _spawnPoint.position + Vector3.up * 2.0f;
        zombie.transform.rotation = Quaternion.identity;
    }
}