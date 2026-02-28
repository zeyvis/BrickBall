using UnityEngine;
using System.Collections;

public class ZombieHealth : BaseEnemyHealth
{
    [SerializeField] private float _upForce = 5f;
    [SerializeField] private float _backForce = 10f;
    [SerializeField] private Rigidbody _impactBody;

    private CapsuleCollider _mainCollider;
    private Rigidbody[] _ragdollBodies;
    private Collider[] _ragdollColliders;

    private ZombiePool _myPool;
    public void Initialize(ZombiePool pool)
    {
        _myPool = pool;

        _isDead = false;

        SetRagdoll(false);

        if (_animator != null) _animator.enabled = true;
        if (_movement != null) _movement.enabled = true;
    }

    protected override void Awake()
    {
        base.Awake(); 

        _ragdollBodies = GetComponentsInChildren<Rigidbody>();
        _ragdollColliders = GetComponentsInChildren<Collider>();
        _mainCollider = GetComponent<CapsuleCollider>();

        SetRagdoll(false);
    }

    protected override void PerformDeathEffect()
    {
        SetRagdoll(true);

        if (_impactBody != null)
        {
            Vector3 forceDir = Vector3.up * _upForce + (-transform.forward) * _backForce;
            _impactBody.AddForce(forceDir, ForceMode.Impulse);
        }

        StartCoroutine(ReturnToPoolAfterDelay(5f));
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
    private IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_myPool != null)
        {
            _myPool.ReturnZombie(this.gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}