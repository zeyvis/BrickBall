using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    private Transform _playerCurrentPos;

    [SerializeField] private float _searchInterval = 0.5f;
    private float _timer;

    public Transform playerCurrentPos => _playerCurrentPos;

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
            killable.Kill();
    }

    private void FindPlayerLocation()
    {
        _playerCurrentPos = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (_playerCurrentPos == null)
            Debug.LogWarning("Player dont find");
    }
}
