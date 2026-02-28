using UnityEngine;

public class DirectionalCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _target;
    [SerializeField] private RandomDirectionManager _directionManager;

    [Header("Base Settings")]
    [SerializeField] private float _distance = 20f;
    [SerializeField] private Vector3 _angle = new Vector3(40f, 0f, 0f);

    [Header("Look Ahead")]
    [SerializeField] private float _lookAheadDistance = 3f;
    [SerializeField] private float _followSmoothness = 8f;

    private float _shakeTimer;
    private float _shakeMagnitude;

    private void LateUpdate()
    {
        if (_target == null || _directionManager == null)
            return;

        Vector3 baseTargetPosition = _target.position;
        Vector3 currentDirection = _directionManager.GetCurrentDirection();

        if (currentDirection != Vector3.zero)
        {
            baseTargetPosition += currentDirection.normalized * _lookAheadDistance;
        }

        Quaternion targetRotation = Quaternion.Euler(_angle);
        Vector3 desiredPosition = baseTargetPosition + targetRotation * Vector3.back * _distance;

        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            _followSmoothness * Time.deltaTime
        );

        if (_shakeTimer > 0)
        {
            smoothedPosition += Random.insideUnitSphere * _shakeMagnitude;
            _shakeTimer -= Time.deltaTime;
        }

        transform.position = smoothedPosition;
        transform.rotation = targetRotation;
    }

    public void ShakeCamera(float duration = 1f, float magnitude = 0.5f)
    {
        _shakeTimer = duration;
        _shakeMagnitude = magnitude;
    }
}