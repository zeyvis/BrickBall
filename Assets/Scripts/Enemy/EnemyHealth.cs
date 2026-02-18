using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IKillable
{
    [SerializeField] private float _upForce = 5f;
    [SerializeField] private float _backForce = 10f;
    [SerializeField] private Rigidbody _impactBody; 


    private Animator _animator;
    private ZombieMovement _movement;
    private CapsuleCollider _mainCollider;
    private Rigidbody[] _ragdollBodies;
    private Collider[] _ragdollColliders;
    private bool _isDead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement= GetComponent<ZombieMovement>();

        _ragdollBodies = GetComponentsInChildren<Rigidbody>();
        _ragdollColliders = GetComponentsInChildren<Collider>();
        _mainCollider = GetComponent<CapsuleCollider>();

        SetRagdoll(false); 
    }

   
    public void Kill()
    {
        Debug.Log("zombie died");
        Die();
    }


    public void Die()
    {
        if (_isDead) return;
        _isDead = true;

        DisableAnimator();
        EnableRagdoll();
        DisableEnemyMovement();
        DieEffect();
    }

  
    private void DisableAnimator()
    {
        if (_animator != null)
            _animator.enabled = false;
    }
    
    private void EnableRagdoll()
    {
        SetRagdoll(true);
    }
    private void DisableEnemyMovement()
    {
        if (_movement != null) 
            _movement.enabled = false;
    }

    private void DieEffect()
    {
        if (_impactBody == null) return;

        Vector3 forceDir =
            Vector3.up * _upForce +
            (-transform.forward) * _backForce;

        _impactBody.AddForce(forceDir, ForceMode.Impulse);
    }

    private void SetRagdoll(bool state)
    {
        foreach (var rb in _ragdollBodies)
        {
            if (rb.gameObject == gameObject) continue; 
            rb.isKinematic = !state;
        }

        foreach (var col in _ragdollColliders)
        {
            if (col.gameObject == gameObject) continue; 
            col.enabled = state;
        }
        if (_mainCollider != null)
            _mainCollider.enabled = !state;

    }
}
