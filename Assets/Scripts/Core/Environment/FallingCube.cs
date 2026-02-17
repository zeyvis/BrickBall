using System.Collections;
using UnityEngine;

public class FallingCube : MonoBehaviour
{
    private bool _isFalling = false;
    private float _fallSpeed = 8.0f;
    private float _destroyDelay = 3.0f;

    private Color _originalColor;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();

        if (_renderer != null)
        {
            _originalColor = _renderer.material.color;
        }

        StartCoroutine(WarningAndFall());
    }

    private void Update()
    {
        if (_isFalling)
        {
            transform.Translate(Vector3.down * _fallSpeed * Time.deltaTime, Space.World);
        }
    }

    private IEnumerator WarningAndFall()
    {
        float duration = 2.0f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float flash = Mathf.PingPong(Time.time * 10f, 1f);

            if (_renderer != null)
            {
                _renderer.material.color = Color.Lerp(_originalColor, Color.red, flash);
            }
            yield return null;
        }

        if (_renderer != null) _renderer.material.color = Color.red;

        transform.SetParent(null);
        _isFalling = true;

        Destroy(gameObject, _destroyDelay);
    }
}