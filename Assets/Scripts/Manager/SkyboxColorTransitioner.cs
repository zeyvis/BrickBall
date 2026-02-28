using UnityEngine;

public class SkyboxColorTransitioner : MonoBehaviour
{
    [Header("Skybox")]
    [SerializeField] private string _colorPropertyName = "_TopColor";

    [Header("Defaults")]
    [SerializeField] private string _baseHexColor = "#245A9B";
    [SerializeField] private float _defaultDuration = 2f;

    private Material _skyboxMaterial;
    private Color _currentColor;

    private bool _isTransitioning;
    private float _timer;
    private float _duration;

    private Color _from;
    private Color _to;

    public Color BaseColor { get; private set; }

    private void Awake()
    {
        _skyboxMaterial = RenderSettings.skybox;

        ColorUtility.TryParseHtmlString(_baseHexColor, out var baseColor);
        BaseColor = baseColor;

        _currentColor = BaseColor;
        ApplyColor(_currentColor);
    }

    private void Update()
    {
        if (!_isTransitioning || _skyboxMaterial == null)
            return;

        _timer += Time.deltaTime;
        float t = Mathf.Clamp01(_timer / _duration);

        _currentColor = Color.Lerp(_from, _to, t);
        ApplyColor(_currentColor);

        if (t >= 1f)
            _isTransitioning = false;
    }

    public void TransitionToHex(string hexColor, float duration = -1f)
    {
        if (_skyboxMaterial == null || !_skyboxMaterial.HasProperty(_colorPropertyName))
            return;

        if (!ColorUtility.TryParseHtmlString(hexColor, out var target))
            return;

        _from = _currentColor;
        _to = target;

        _duration = duration <= 0f ? _defaultDuration : duration;
        _timer = 0f;
        _isTransitioning = true;
    }

    public void TransitionToBase(float duration = -1f)
    {
        TransitionToColor(BaseColor, duration);
    }

    public void TransitionToColor(Color target, float duration = -1f)
    {
        if (_skyboxMaterial == null || !_skyboxMaterial.HasProperty(_colorPropertyName))
            return;

        _from = _currentColor;
        _to = target;

        _duration = duration <= 0f ? _defaultDuration : duration;
        _timer = 0f;
        _isTransitioning = true;
    }

    private void ApplyColor(Color c)
    {
        if (_skyboxMaterial == null) return;
        _skyboxMaterial.SetColor(_colorPropertyName, c);
    }
}