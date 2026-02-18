using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostController : MonoBehaviour
{
    [Header("Trail Time Settings")]
    [SerializeField] private float _speedLimitStart = 15f;  
    [SerializeField] private float _speedLimitYellow = 20f; 
    [SerializeField] private float _speedLimitRed = 25f;    

    [Header("Trail Colors")]
    [SerializeField] private Color _yellowColor = new Color(1f, 0.92f, 0.016f, 0.5f);
    [SerializeField] private Color _redColor = new Color(1f, 0.2f, 0.2f, 0.75f);

    
    [SerializeField] private float _speedMutltipler = 1.2f;
    private TrailRenderer _trail;
    private PlayerMover _playerMover;
    private float _baseSpeed;
    private bool _beastMode = false;
    public bool playerBeastMode=> _beastMode;

    private void Start()
    {
        _playerMover = GetComponent<PlayerMover>();
        _baseSpeed = _playerMover.moveSpeed;
        _trail = GetComponent<TrailRenderer>();

        UpdateTrailColor();
    }

    public void UpdatePlayerSpeed()
    {
        float calculatedSpeed = _playerMover.moveSpeed + _speedMutltipler;
        _playerMover.moveSpeed = calculatedSpeed;
        UpdateTrailColor();
        UpdatePlayerMode();
    }

    public void ResetPlayerSpeed()
    {
        _playerMover.moveSpeed = _baseSpeed;
        UpdateTrailColor();
        UpdatePlayerMode();
    }

    private void UpdateTrailColor()
    {
        if (_trail == null) return;

        float currentSpeed = _playerMover.moveSpeed;


        if (currentSpeed < _speedLimitStart)
        {
            _trail.emitting = false;
            return;
        }

        _trail.emitting = true;
        Color targetColor;


        if (currentSpeed <= _speedLimitYellow)
        {
            targetColor = _yellowColor;
        }
        else if (currentSpeed <= _speedLimitRed)
        {
            float t = Mathf.InverseLerp(_speedLimitYellow, _speedLimitRed, currentSpeed);
            targetColor = Color.Lerp(_yellowColor, _redColor, t);
        }

        else
        {
            targetColor = _redColor;
        }

        _trail.startColor = targetColor;
        _trail.endColor = new Color(targetColor.r, targetColor.g, targetColor.b, 0f);
    }

    private void UpdatePlayerMode()
    {
        float currentSpeed = _playerMover.moveSpeed;
        if(currentSpeed>_speedLimitRed)
            _beastMode= true;
        else
            _beastMode= false;
        
    }
}