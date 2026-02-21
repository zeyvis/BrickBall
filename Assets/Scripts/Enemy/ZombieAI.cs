using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    private Transform _playerCurrentPos;

    [SerializeField] private float _searchInterval = 0.5f;
    private float _timer;
    private ZombieManager _zombieManager;
    private ZombieMovement _movement;
    private Animator _animator;
    public Transform playerCurrentPos => _playerCurrentPos;

    private void Awake()
    {
        _zombieManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ZombieManager>();
        _movement = GetComponent<ZombieMovement>();
        _animator= GetComponent<Animator>();
    }
    private void Update()
    {
        if (_playerCurrentPos != null)
            return;

        _timer += Time.deltaTime;

        if (_timer >= _searchInterval)
        {
            _timer = 0f;
            FindPlayerLocation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IKillable>(out var killable))
        {
            
            killable.Kill();
        }
            
    }

    private void FindPlayerLocation()
    {
        _playerCurrentPos = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (_playerCurrentPos == null)
            Debug.LogWarning("Player dont find");
    }
    public void StopZombie()
    {
        if (_movement != null)
        {
            _movement.enabled = false;
            _animator.enabled = false;
        }

            
    }
    public void ResumeZombie()
    {
        if (_movement != null)
        {
            _movement.enabled = true;
            _animator.enabled = true;
        }
    }

    private void OnEnable()
    {
        _zombieManager.RegisterZombie(this);
        if (_movement != null)
            _movement.enabled = true;

        if (_animator != null)
            _animator.enabled = true;
    }

    private void OnDisable()
    {
        _zombieManager.UnregisterZombie(this);
    }
}
