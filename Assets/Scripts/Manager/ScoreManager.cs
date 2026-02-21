using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTxt;

    private bool _playerDied = false;
    private bool _gameStarted = false;
    private int _score = 0;
    private float _timer = 0f;
    public int CurrentScore => _score;

    private void Start()
    {
        UpdateScoreText(); 
    }

    private void Update()
    {
        if (!_gameStarted || _playerDied)
            return;


        _timer += Time.deltaTime;
       
        if (_timer >= 1f)
        {
            _timer -= 1f;   
            _score++;
            UpdateScoreText();
        }
    }
    public void StartScore()
    {
        _gameStarted = true;
    }
    private void UpdateScoreText()
    {
        _scoreTxt.SetText(_score.ToString());
    }
    public void StopScoreFromManager()
    {
        _playerDied= true;
    }
    public void ResumeScoreFromManager()
    {
        _playerDied = false;
    }
    public int GetBestScore()
    {
        return PlayerPrefs.GetInt("BEST_SCORE", 0);
    }
    public void SaveBestIfNeeded()
    {
        int best = GetBestScore();
        if (_score > best)
        {
            PlayerPrefs.SetInt("BEST_SCORE", _score);
            PlayerPrefs.Save();
        }
    }
}