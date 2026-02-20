using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private PlayerMover _playerMover;
    private PlayerDeathEffect _playerDeathEffect;
    private ZombieManager _zombieManager;

    private void Awake()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerHealth = player.GetComponent<PlayerHealth>();
            _playerMover = player.GetComponent<PlayerMover>();
            _playerDeathEffect = player.GetComponent<PlayerDeathEffect>();
        }


        _zombieManager = GetComponent<ZombieManager>(); 
    }

    private void OnEnable()
    {
        if (_playerHealth != null)
            _playerHealth.OnPlayerDied += HandlePlayerDied;
    }

    private void OnDisable()
    {
        if (_playerHealth != null)
            _playerHealth.OnPlayerDied -= HandlePlayerDied;
    }

    private void HandlePlayerDied()
    {

        if (_playerMover != null)
            _playerMover.StopMovementFromManager();


        if (_zombieManager != null)
            _zombieManager.StopAllZombiesFromManager();


        if (_playerDeathEffect != null)
            _playerDeathEffect.PlayFromManager();


    }
}