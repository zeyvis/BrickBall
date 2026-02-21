using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameStartController : MonoBehaviour
{
    [Header("Overlay")]
    [SerializeField] private GameObject _startOverlayRoot;

    [Header("Gameplay Components")]
    [SerializeField] private Behaviour _phaseManager;
    [SerializeField] private Behaviour _playerMover;
    [SerializeField] private ScoreManager _scoreManager;

    [Header("UI To Enable On Start")]
    [SerializeField] private GameObject _bgUI;
    [SerializeField] private GameObject _gameScoreUI;

    [Header("Options")]
    [SerializeField] private bool _disableGameplayOnAwake = true;

    private bool _hasStarted;

    private void Awake()
    {
        if (_disableGameplayOnAwake)
            DisableGameplay();

        if (_bgUI != null)
            _bgUI.SetActive(false);

        if (_gameScoreUI != null)
            _gameScoreUI.SetActive(false);
    }

    public void StartGame()
    {
        if (_hasStarted)
            return;

        if (_startOverlayRoot == null || _phaseManager == null || _playerMover == null)
        {
            Debug.LogError(
                $"{nameof(GameStartController)} missing reference.",
                this
            );
            return;
        }

        _hasStarted = true;


        _startOverlayRoot.SetActive(false);


        if (_bgUI != null)
            _bgUI.SetActive(true);

        if (_gameScoreUI != null)
            _gameScoreUI.SetActive(true);

        if (_scoreManager != null)
            _scoreManager.StartScore();

        _phaseManager.enabled = true;
        _playerMover.enabled = true;
    }

    public void DisableGameplay()
    {
        if (_phaseManager != null)
            _phaseManager.enabled = false;

        if (_playerMover != null)
            _playerMover.enabled = false;
    }
}