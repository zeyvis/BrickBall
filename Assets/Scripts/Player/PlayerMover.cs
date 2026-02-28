using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float _ballRadius;

    private bool _isHolding = false;
    private bool _wasHolding = false;
    private bool _canMove = true;

    [SerializeField] private RandomDirectionManager _randomDirectionManager;
    private SpeedBoostController _speedBoostController;

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDistance = 0.1f;
    private bool _isGrounded;
    private float _airborneTimer = 0f;
    public bool IsMoving => _canMove && _isHolding;
    private void Start()
    {
        _speedBoostController = GetComponent<SpeedBoostController>();
        _ballRadius = GetComponent<SphereCollider>().bounds.extents.x;
    }

    private void Update()
    {
        if (!_canMove) return;

        CheckGroundedState();
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
    private void CheckGroundedState()
    {
        _isGrounded = Physics.CheckSphere(
            transform.position - new Vector3(0, _ballRadius, 0),
            _ballRadius * 0.9f,
            _groundLayer
        );

        if (_isGrounded)
            _airborneTimer = 0f;
        else
            _airborneTimer += Time.deltaTime;
    }
    private void Move()
    {
        if (_randomDirectionManager == null) return;
        if (!_isGrounded && _airborneTimer > 0.17f) return;

        Vector3 currentDirection = _randomDirectionManager.GetCurrentDirection();
        transform.position += currentDirection * moveSpeed * Time.deltaTime;
        RotateBall(currentDirection);
    }

    private void RotateBall(Vector3 direction)
    {
        float angle = (moveSpeed / _ballRadius) * Mathf.Rad2Deg * Time.deltaTime;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);
        transform.Rotate(rotationAxis, angle, Space.World);
    }

    public void StopMovementFromManager()
    {
        _canMove = false;
        _isHolding = false;
        _wasHolding = false;

        if (_speedBoostController != null)
            _speedBoostController.ResetPlayerSpeed();
    }

    public void ResumeMovementFromManager()
    {
        _canMove = true;
    }
}