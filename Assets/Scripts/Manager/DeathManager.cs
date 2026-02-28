using UnityEngine;

public class DeathManager : MonoBehaviour
{
    private GameOverPanel _gameOverPanel;
    private PlayerHealth _playerHealth;
    private PlayerMover _playerMover;
    private PlayerDeathEffect _playerDeathEffect;
    private EnemyManager _enemyManager;
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

        _gameOverPanel = GetComponent<GameOverPanel>();
        _scoreManager = GetComponent<ScoreManager>();
        _enemyManager = GetComponent<EnemyManager>();
    }

    private void OnEnable()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnPlayerDied += HandlePlayerDied;
            _playerHealth.OnPlayerRevived += HandlePlayerRevived;
        }
    }

    private void OnDisable()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnPlayerDied -= HandlePlayerDied;
            _playerHealth.OnPlayerRevived -= HandlePlayerRevived;
        }
    }

    private void HandlePlayerDied()
    {
        if (_playerMover != null) _playerMover.StopMovementFromManager();
        if (_enemyManager != null) _enemyManager.StopAllEntities();
        if (_playerDeathEffect != null) _playerDeathEffect.PlayFromManager();
        if (_scoreManager != null) _scoreManager.StopScoreFromManager();

        _scoreManager.SaveBestIfNeeded();
        int best = _scoreManager.GetBestScore();
        int current = _scoreManager.CurrentScore;

        _gameOverPanel.SetScores(current, best);

        if (_gameOverPanel != null) _gameOverPanel.ShowGameOverPanel();
    }

    private void HandlePlayerRevived()
    {
        if (_playerMover != null) _playerMover.ResumeMovementFromManager();
        if (_enemyManager != null) _enemyManager.ResumeAllEntities();
        if (_scoreManager != null) _scoreManager.ResumeScoreFromManager();
        if (_gameOverPanel != null) _gameOverPanel.HideGameOverPanel();
    }
}