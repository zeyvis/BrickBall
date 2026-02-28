using UnityEngine;

public class BaseEnemyAI : MonoBehaviour, IManageable
{
    protected Transform _playerCurrentPos;
    protected EnemyManager _enemyManager;
    protected EnemyMovement _movement;
    protected Animator _animator;


    public Transform PlayerCurrentPos => _playerCurrentPos;

    protected virtual void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            _playerCurrentPos = player.transform;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: Player not found!");
        }


        _enemyManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyManager>();

        _movement = GetComponent<EnemyMovement>();
        _animator = GetComponent<Animator>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IKillable>(out var killable))
        {
            killable.Kill();
        }
    }

    protected virtual void OnEnable()
    {
        if (_enemyManager != null)
            _enemyManager.RegisterEntity(this);

        if (_movement != null)
            _movement.enabled = true;

        if (_animator != null)
            _animator.enabled = true;
    }

    protected virtual void OnDisable()
    {
        if (_enemyManager != null)
            _enemyManager.UnregisterEntity(this);
    }

    public virtual void StopAction()
    {
        if (_movement != null) _movement.enabled = false;
        if (_animator != null) _animator.enabled = false;
    }

    public virtual void ResumeAction()
    {
        if (_movement != null) _movement.enabled = true;
        if (_animator != null) _animator.enabled = true;
    }
}