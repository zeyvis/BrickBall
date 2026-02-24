using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHealhtManager : MonoBehaviour, IKillable
{
    private HazardPool _myPool;
    private ParticleSystem _statueParticle;


    private Renderer _renderer;
    private Collider _collider;

    private void Awake()
    {

        _statueParticle = GetComponentInChildren<ParticleSystem>();

        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
    }

    public void Initialize(HazardPool pool)
    {
        _myPool = pool;


        if (_renderer != null) _renderer.enabled = true;
        if (_collider != null) _collider.enabled = true;
    }

    public void Kill()
    {
        Debug.Log("statue died");


        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        if (_renderer != null) _renderer.enabled = false;
        if (_collider != null) _collider.enabled = false;

        if (_statueParticle != null)
        {
            _statueParticle.Play();
        }
        else
        {
            Debug.LogWarning("StateHealthManager: Particle System not founded:(");
        }

       
        yield return new WaitForSeconds(1.5f);


        if (_myPool != null)
        {
            _myPool.ReturnHazard(this.gameObject);
        }
    }
}