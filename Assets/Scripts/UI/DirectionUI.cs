using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DirectionUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RandomDirectionManager _directionManager;
    [SerializeField] private Image _currentArrow;
    [SerializeField] private Image _nextArrow;

    [Header("Settings")]
    [SerializeField] private float _rotateDuration = 0.14f;
    [SerializeField] private float _popScale = 1.15f;
    [SerializeField] private float _popDuration = 0.1f;

    [SerializeField] private bool _useAlphaFlash = true;
    [SerializeField] private float _flashAlpha = 0.75f;
    [SerializeField] private float _flashDuration = 0.08f;

    private Coroutine _animationRoutine;

    private void OnEnable()
    {
        if (_directionManager == null) return;

        _directionManager.OnDirectionsChanged += UpdateUI;
        UpdateUI(_directionManager.GetCurrentDirection(), _directionManager.GetNextDirection());
    }

    private void OnDisable()
    {
        if (_directionManager != null)
            _directionManager.OnDirectionsChanged -= UpdateUI;

        StopActiveAnimation();
    }

    private void UpdateUI(Vector3 current, Vector3 next)
    {
        StopActiveAnimation();

        if (_currentArrow != null)
            _animationRoutine = StartCoroutine(AnimateTransition(_currentArrow, current));

        if (_nextArrow != null)
            SetArrowInstant(_nextArrow, next);
    }

    private IEnumerator AnimateTransition(Image arrow, Vector3 direction)
    {
        RectTransform rt = arrow.rectTransform;
        float targetZ = GetAngleFromDirection(direction);
        float startZ = rt.localEulerAngles.z;
        Color baseColor = arrow.color;

        float elapsed = 0;
        float maxDuration = Mathf.Max(_rotateDuration, _popDuration, _useAlphaFlash ? _flashDuration : 0);

        while (elapsed < maxDuration)
        {
            elapsed += Time.unscaledDeltaTime;

            if (elapsed <= _rotateDuration)
            {
                float t = EaseOutCubic(elapsed / _rotateDuration);
                float currentZ = startZ + Mathf.DeltaAngle(startZ, targetZ) * t;
                rt.localEulerAngles = new Vector3(0, 0, currentZ);
            }

            if (elapsed <= _popDuration)
            {
                float t = elapsed / _popDuration;
                float scale = 1f + Mathf.Sin(t * Mathf.PI) * (_popScale - 1f);
                rt.localScale = Vector3.one * scale;
            }

            if (_useAlphaFlash && elapsed <= _flashDuration)
            {
                float t = elapsed / _flashDuration;
                float alpha = baseColor.a + Mathf.Sin(t * Mathf.PI) * (_flashAlpha - baseColor.a);
                arrow.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            }

            yield return null;
        }

        ApplyFinalState(arrow, targetZ, baseColor);
    }

    private void SetArrowInstant(Image arrow, Vector3 direction)
    {
        arrow.rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromDirection(direction));
        arrow.rectTransform.localScale = Vector3.one;
    }

    private void ApplyFinalState(Image arrow, float angle, Color color)
    {
        arrow.rectTransform.localEulerAngles = new Vector3(0, 0, angle);
        arrow.rectTransform.localScale = Vector3.one;
        arrow.color = color;
    }

    private float GetAngleFromDirection(Vector3 dir)
    {
        if (dir == Vector3.forward) return 90f;
        if (dir == Vector3.left) return 180f;
        if (dir == Vector3.back) return -90f;
        return 0f; // Vector3.right
    }

    private float EaseOutCubic(float t) => 1f - Mathf.Pow(1f - t, 3f);

    private void StopActiveAnimation()
    {
        if (_animationRoutine != null) StopCoroutine(_animationRoutine);
        _animationRoutine = null;
    }
}