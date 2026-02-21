using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private float _spawnHeightOffset = 2f;

    private Transform _player;
    private Transform _platformContainer;
    private GameObject _lastContactObject;

    private void Start()
    {
        FindReferences();
    }

    private void FindReferences()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("ContinueButton: PlayerNull");
        }

        GameObject containerObj = GameObject.Find("Platform_Container");
        if (containerObj != null)
        {
            _platformContainer = containerObj.transform;
        }
        else
        {
            Debug.LogWarning("ContinueButton: PlatformNull");
        }
    }

    public void RegisterDeath(GameObject contactObject)
    {
        _lastContactObject = contactObject;
    }

    public void SavePlayer()
    {
        if (_lastContactObject != null && _lastContactObject.CompareTag("Enemy"))
        {
            if (_lastContactObject.TryGetComponent<IKillable>(out var killableTarget))
            {
                killableTarget.Kill();
            }
            _lastContactObject = null;

            RestoreGameState();
            return;
        }

        _lastContactObject = null;
        RescuePlayer();
    }

    private void RescuePlayer()
    {
        if (_player == null || _platformContainer == null)
        {
            FindReferences();
        }

        if (_player == null || _platformContainer == null)
        {
            Debug.LogError("ContinueButton: Player or PlatformContainer null");
            return;
        }

        if (_player.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.velocity = Vector3.zero;
        }

        _player.position = GetSafePosition();
        RestoreGameState();
    }

    private void RestoreGameState()
    {
        if (_player != null && _player.TryGetComponent<PlayerDeathEffect>(out var deathEffect))
        {
            deathEffect.RestorePlayer();
        }

        if (_player != null && _player.TryGetComponent<PlayerHealth>(out var health))
        {
            health.Revive();
        }
    }

    private Vector3 GetSafePosition()
    {
        foreach (Transform child in _platformContainer)
        {
            if (!child.TryGetComponent<FallingCube>(out _))
            {
                return child.position + (Vector3.up * _spawnHeightOffset);
            }
        }

        if (_platformContainer.childCount > 0)
        {
            return _platformContainer.GetChild(0).position + (Vector3.up * _spawnHeightOffset);
        }

        return Vector3.zero + (Vector3.up * _spawnHeightOffset);
    }
}