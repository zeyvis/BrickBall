using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    //movement
    public float moveSpeed = 5f;
    private float _ballRadius;
    
    //input states
    private bool _isHolding = false;
    private bool _wasHolding = false;

    // References
    [SerializeField] private RandomDirectionManager _randomDirectionManager;
    private SpeedBoostController _speedBoostController;
    private void Start()
    {
        _speedBoostController= GetComponent<SpeedBoostController>();
        _ballRadius = transform.localScale.x / 2f;
    }


    private void Update()
    {
        HandleInput();
        CheckHoldState();
    }
    private void HandleInput()
    {
        _isHolding = Input.GetMouseButton(0) || (Input.touchCount > 0);
    }
    private void CheckHoldState()
    {
        if (_isHolding && !_wasHolding)
        {
            Debug.Log("Basýlý tutma baþladý");
        }

        if (_isHolding)
        {
            Move();
            _speedBoostController.UpdatePlayerSpeed();
        }

        if (!_isHolding && _wasHolding)
        {
            Debug.Log("Basýlý tutma bitti");
            _randomDirectionManager.UpdateDirections();
            _speedBoostController.ResetPlayerSpeed();
        }

        _wasHolding = _isHolding;
    }
    private void Move()
    {
        if (_randomDirectionManager == null) 
        {
            Debug.LogWarning("RandomDirectionManager null");
            return;
        }
        Vector3 currentDirection=_randomDirectionManager.GetCurrentDirection();

        transform.position += currentDirection * moveSpeed * Time.deltaTime;

        RotateBall(currentDirection);
    }
    private void RotateBall(Vector3 direction)
    {

        float angle = (moveSpeed / _ballRadius) * Mathf.Rad2Deg * Time.deltaTime;

        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

        transform.Rotate(rotationAxis, angle, Space.World);
    }
}

