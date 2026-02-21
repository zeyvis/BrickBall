using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathParticle;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private float _fadeDuration = 0.5f;

    private Material _mat;
    private Color _startColor;

    private void Start()
    {
        _mat = _renderer.material;
        _startColor = _mat.color;
    }

    public void PlayFromManager()
    {
        ParticleEffect();
        StartCoroutine(FadeOut());
    }

    public void RestorePlayer()
    {
        StopAllCoroutines();
        if (_mat != null)
        {
            _mat.color = _startColor;
        }
    }

    private IEnumerator FadeOut()
    {
        float time = 0f;

        while (time < _fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / _fadeDuration;

            Color c = _startColor;
            c.a = Mathf.Lerp(1f, 0f, t);
            _mat.color = c;

            yield return null;
        }

        Color finalColor = _startColor;
        finalColor.a = 0f;
        _mat.color = finalColor;
    }

    private void ParticleEffect()
    {
        if (_deathParticle == null) return;

        _deathParticle.transform.position = transform.position;
        _deathParticle.transform.rotation = Quaternion.identity;

        _deathParticle.Play();
    }
}