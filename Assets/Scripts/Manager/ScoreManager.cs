using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTxt;

    private int _score = 0;
    private float _timer = 0f;

    private void Start()
    {
        UpdateScoreText(); 
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        
        if (_timer >= 1f)
        {
            _timer -= 1f;   
            _score++;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        _scoreTxt.SetText(_score.ToString());
    }
}