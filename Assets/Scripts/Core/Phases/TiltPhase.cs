using System.Collections; // IEnumerator için gerekli
using UnityEngine;

public class TiltPhase : IGamePhase
{
    private Transform _transformToRotate;
    private Quaternion _targetRotation;
    private float _smoothSpeed = 2.0f;

    private MonoBehaviour _coroutineRunner;


    public TiltPhase(Transform transformObj, MonoBehaviour runner)
    {
        _transformToRotate = transformObj;
        _coroutineRunner = runner; 

        if (_transformToRotate != null)
        {
            _targetRotation = _transformToRotate.localRotation;
        }
    }

    public void Enter()
    {
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

        _transformToRotate.localRotation = Quaternion.Slerp(
            _transformToRotate.localRotation,
            _targetRotation,
            Time.deltaTime * _smoothSpeed
        );
    }

    public void Exit()
    {
        Debug.Log("Tilt phase finished");


        if (_coroutineRunner != null && _transformToRotate != null)
        {
            _coroutineRunner.StartCoroutine(ResetRoutine());
        }
    }

    private void SetNewTarget()
    {
        float randomX = Random.Range(-30f, 30f);
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