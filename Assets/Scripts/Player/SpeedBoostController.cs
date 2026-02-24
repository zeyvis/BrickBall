using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostController : MonoBehaviour
{
    [Header("Trail Time Settings")]
    [SerializeField] private float _speedLimitYellow = 20f; 
    [SerializeField] private float _speedLimitRed = 25f;

    [Header("VFX Trail References")]
    [SerializeField] private TrailRenderer _distortionTrail;
    [SerializeField] private TrailRenderer _trail02;
    [SerializeField] private TrailRenderer _trail01;

    [Header("Speed Settings")]
    [SerializeField] private float _baseSpeedMultiplier = 2f; 
    [SerializeField] private float _multiplierIncreaseRate = 5f; 
    [SerializeField] private float _maxSpeedMultiplier = 25f; 
    [SerializeField] private float _maxSpeed = 40f; 

    [Header("VFX Settings")]
    [SerializeField] private ParticleSystem _speedLinesParticle;

    private float _currentSpeedMultiplier; 

    private PlayerMover _playerMover;
    private float _baseSpeed;
    private bool _beastMode = false;
    public bool playerBeastMode=> _beastMode;

    private void Start()
    {
        _playerMover = GetComponent<PlayerMover>();
        _baseSpeed = _playerMover.moveSpeed;
       

        _currentSpeedMultiplier = _baseSpeedMultiplier; 

        UpdateVFXState();
    }

    public void ResetPlayerSpeed()
    {
        _playerMover.moveSpeed = _baseSpeed;
        _currentSpeedMultiplier = _baseSpeedMultiplier;
        UpdateVFXState();
    }

    public void UpdatePlayerSpeed()
    {
       
        _currentSpeedMultiplier += _multiplierIncreaseRate * Time.deltaTime;

      
        _currentSpeedMultiplier = Mathf.Min(_currentSpeedMultiplier, _maxSpeedMultiplier);

      
        float calculatedSpeed = _playerMover.moveSpeed + (_currentSpeedMultiplier * Time.deltaTime);
        _playerMover.moveSpeed = Mathf.Clamp(calculatedSpeed, _baseSpeed, _maxSpeed);

        UpdateVFXState();
    }

    private void UpdateVFXState()
    {
        float currentSpeed = _playerMover.moveSpeed;

       
        if (currentSpeed < _speedLimitYellow)
        {
            SetTrailState(_distortionTrail, false);
            SetTrailState(_trail02, false);
            SetTrailState(_trail01, false);
            SetParticleState(_speedLinesParticle, false); 

            _beastMode = false;
        }

        else if (currentSpeed >= _speedLimitYellow && currentSpeed < _speedLimitRed)
        {
            SetTrailState(_distortionTrail, true);
            SetTrailState(_trail02, true);
            SetTrailState(_trail01, false);
            SetParticleState(_speedLinesParticle, false); 

            _beastMode = false;
        }

        else if (currentSpeed >= _speedLimitRed)
        {
            SetTrailState(_distortionTrail, true);
            SetTrailState(_trail02, true);
            SetTrailState(_trail01, true);
            SetParticleState(_speedLinesParticle, true); 

            _beastMode = true;
        }
    }

    private void SetTrailState(TrailRenderer trail, bool isEmitting)
    {
        if (trail == null) return;


        if (trail.emitting != isEmitting)
        {
            trail.emitting = isEmitting;
        }
    }
    private void SetParticleState(ParticleSystem ps, bool shouldPlay)
    {
        if (ps == null) return;


        if (shouldPlay && !ps.isEmitting)
        {
            ps.Play();
        }
        else if (!shouldPlay && ps.isEmitting)
        {
            ps.Stop();
        }
    }

}