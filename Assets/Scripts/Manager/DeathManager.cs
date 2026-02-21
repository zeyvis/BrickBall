using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private GameOverPanel _gameOverPanel;
    private PlayerHealth _playerHealth;
    private PlayerMover _playerMover;
    private PlayerDeathEffect _playerDeathEffect;
    private ZombieManager _zombieManager;
    private ScoreManager _scoreManager;

    private void Awake()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerHealth = player.GetComponent<PlayerHealth>();
            _playerMover = player.GetComponent<PlayerMover>();
            _playerDeathEffect = player.GetComponent<PlayerDeathEffect>();
        }

        _gameOverPanel=GetComponent<GameOverPanel>();
        _scoreManager= GetComponent<ScoreManager>();
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


        if (_scoreManager != null)
            _scoreManager.StopScoreFromManager();

        _scoreManager.SaveBestIfNeeded();
        int best = _scoreManager.GetBestScore();
        int current = _scoreManager.CurrentScore;

        _gameOverPanel.SetScores(current, best);

        if (_gameOverPanel != null)
            _gameOverPanel.ShowGameOverPanel();

    }
}