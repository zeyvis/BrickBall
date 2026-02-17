using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private ZombieAI _zombieAI;
    private Animator _animator;
    [SerializeField] private float _speed = 5f;

    void Start()
    {
        _zombieAI = GetComponent<ZombieAI>();     
        _animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (_zombieAI.playerCurrentPos == null)
            return;

        Vector3 lookPos = _zombieAI.playerCurrentPos.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        transform.position= Vector3.MoveTowards(
            transform.position,
            _zombieAI.playerCurrentPos.position,
            _speed * Time.deltaTime
            );
        _animator.SetBool("Walk", true);
        
    }
}
