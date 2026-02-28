using UnityEngine;


public abstract class BaseEnemyHealth : MonoBehaviour, IKillable
{
    protected Animator _animator;
    protected EnemyMovement _movement;
    protected bool _isDead;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<EnemyMovement>();
    }

    public void Kill()
    {
        if (_isDead) return;
        Die();
    }

    public virtual void Die()
    {
        _isDead = true;
        DisableComponents();


        PerformDeathEffect();
    }

    protected virtual void DisableComponents()
    {
        if (_animator != null) _animator.enabled = false;
        if (_movement != null) _movement.enabled = false;
    }


    protected abstract void PerformDeathEffect();
}