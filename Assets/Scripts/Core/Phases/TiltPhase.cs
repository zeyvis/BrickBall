using System.Collections; 
using UnityEngine;

public class TiltPhase : IGamePhase
{
    private Transform _transformToRotate;
    private Quaternion _targetRotation;
    private float _smoothSpeed = 2.0f;

    private MonoBehaviour _coroutineRunner;
    public float Duration => 15f;
    private const float kRetargetInterval = 4f; 
    private float _retargetTimer;


    private readonly SkyboxColorTransitioner _skybox;


 


    public TiltPhase(Transform transformObj, MonoBehaviour runner, SkyboxColorTransitioner skybox)
    {
        _transformToRotate = transformObj;
        _coroutineRunner = runner;
        _skybox = skybox;

        if (_transformToRotate != null)
        {
            _targetRotation = _transformToRotate.localRotation;
        }
    }

    public void Enter()
    {
        _skybox.TransitionToHex("#B91C1C", 2f);
        _retargetTimer = 0f;
        Debug.Log("Tilt phase  started.");
        SetNewTarget();
    }

    public void Update()
    {
        

        if (_transformToRotate == null) return;

        if (Input.GetKeyDown(KeyCode.D))
        {
            SetNewTarget();
        }
        _retargetTimer += Time.deltaTime;

        if (_retargetTimer >= kRetargetInterval)
        {
            _retargetTimer -= kRetargetInterval;
            SetNewTarget();
        }
        _transformToRotate.localRotation = Quaternion.Slerp(
            _transformToRotate.localRotation,
            _targetRotation,
            Time.deltaTime * _smoothSpeed
        );
    }

    public void Exit()
    {
        Debug.Log("Tilt phase finished");
        _skybox.TransitionToHex("#1E40AF", 1.5f);

        if (_coroutineRunner != null && _transformToRotate != null)
        {
            _coroutineRunner.StartCoroutine(ResetRoutine());
        }
    }

    private void SetNewTarget()
    {
        float randomX = Random.Range(-12f, 12f);
        _targetRotation = Quaternion.Euler(randomX, 0, 0);
    }


    private IEnumerator ResetRoutine()
    {
        float resetSpeed = 2.0f;


        while (Quaternion.Angle(_transformToRotate.localRotation, Quaternion.identity) > 0.1f)
        {

            _transformToRotate.localRotation = Quaternion.Slerp(
                _transformToRotate.localRotation,
                Quaternion.identity,
                Time.deltaTime * resetSpeed
            );


            yield return null;
        }


        _transformToRotate.localRotation = Quaternion.identity;
    }
}