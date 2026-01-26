using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{

    private bool _isHolding = false;
    private bool _wasHolding = false;
    [SerializeField]private float _moveSpeed = 5f;

    [SerializeField] private RandomDirectionManager _randomDirectionManager;


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
        }

        if (!_isHolding && _wasHolding)
        {
            Debug.Log("Basýlý tutma bitti");
            _randomDirectionManager.UpdateDirections();
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

        transform.position += currentDirection * _moveSpeed * Time.deltaTime;


    }
}

