using UnityEngine;
using System.Collections;

public class GhostHealth : BaseEnemyHealth
{
    private ParticleSystem _ghostParticle;
    private Renderer _renderer;
    private Collider _collider;
    private GhostPool _myPool;

    protected override void Awake()
    {
        base.Awake();
        _ghostParticle = GetComponentInChildren<ParticleSystem>();
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
    }

    
    public void Initialize(GhostPool pool)
    {
        _myPool = pool;

        _isDead= false;

        if (_renderer != null) _renderer.enabled = true;
        if (_collider != null) _collider.enabled = true;
        if (_animator != null) _animator.enabled = true;
        if (_movement != null) _movement.enabled = true;
    }
    
    protected override void PerformDeathEffect()
    {

        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        if (_renderer != null) _renderer.enabled = false;
        if (_collider != null) _collider.enabled = false;

        if (_ghostParticle != null)
        {
            _ghostParticle.Play();
        }

        yield return new WaitForSeconds(1.5f);

        if (_myPool != null)
        {
            _myPool.ReturnGhost(this.gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}