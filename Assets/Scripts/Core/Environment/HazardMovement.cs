using UnityEngine;

public class HazardMovement : MonoBehaviour
{
    [SerializeField]private float _fallSpeed = 5f;
    private bool _iscontactPlatform = false;
    
    void Update()
    {
        if (!_iscontactPlatform)
        transform.Translate(Vector3.down * _fallSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlatformBox"))
        {
            _iscontactPlatform = true;
        }
    }
    private void OnDisable()
    {
        _iscontactPlatform= false;
    }
}