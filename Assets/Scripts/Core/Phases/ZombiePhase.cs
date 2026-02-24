using UnityEngine;

public class ZombiePhase : IGamePhase
{
    private ZombiePool _pool;
    private Transform _spawnPoint; 
    private GameObject _currentZombie;
    public float Duration => 20f;
    public ZombiePhase(ZombiePool pool, Transform spawnPoint)
    {
        _pool = pool;
        _spawnPoint = spawnPoint;
    }

    public void Enter()
    {

        _currentZombie = _pool.GetZombie();


        if (_spawnPoint != null)
        {
            _currentZombie.transform.position = _spawnPoint.position + Vector3.up * 2.0f;
            _currentZombie.transform.rotation = Quaternion.identity;
        }
    }

    public void Update()
    {

    }

    public void Exit()
    {

        if (_currentZombie != null)
        {
            _pool.ReturnZombie(_currentZombie);
            _currentZombie = null;
        }
    }
}