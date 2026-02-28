using UnityEngine;


public class EnemyJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _forwardCheckDistance = 1.5f;
    [SerializeField] private float _gapDepth = 2f;
    [Space(5)]

    [SerializeField] private LayerMask _groundLayer; 

    private Rigidbody _rb;
    private Animator _animator;
    private bool _isGrounded;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, _groundLayer);

        
        if (_isGrounded)
        {
            CheckGapAndJump();
        }
    }

    private void CheckGapAndJump()
    {

        Vector3 checkPos = transform.position + (transform.forward * _forwardCheckDistance);

        bool isGapAhead = !Physics.Raycast(checkPos, Vector3.down, _gapDepth, _groundLayer);

        if (isGapAhead)
        {
            
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z); 
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

            if (_animator != null)
            {
                _animator.SetTrigger("Jump"); 
            }
        }
    }

  
}