using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed = 5f;

    [SerializeField] private RandomDirectionManager _randomDirectionManager;

    private bool _isHolding = false;
    private bool _wasHolding = false;
    private SpeedBoostController _speedBoostController;


    private void Start()
    {
        _speedBoostController= GetComponent<SpeedBoostController>();
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


    }
}

