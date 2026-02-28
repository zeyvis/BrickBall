using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private BaseEnemyAI _enemyAI;
    private Animator _animator;
    [SerializeField] private float _speed = 5f;

    private void Start()
    {
        _enemyAI = GetComponent<BaseEnemyAI>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_enemyAI.PlayerCurrentPos == null)
            return;

        Vector3 lookPos = _enemyAI.PlayerCurrentPos.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);


        Vector3 targetPos = _enemyAI.PlayerCurrentPos.position;
        targetPos.y = transform.position.y;


        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            _speed * Time.deltaTime
        );

        if (_animator != null)
        {
            _animator.SetBool("Walk", true);
        }
    }
}