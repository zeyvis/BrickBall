using System.Collections;
using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private GameObject _bg;
    [SerializeField] private GameObject _gameScoreTxt;
    [SerializeField] private GameObject _darkOverlay;
    [SerializeField] private GameObject _niceTryImage;
    [SerializeField] private TextMeshProUGUI _bestTxt;
    [SerializeField] private TextMeshProUGUI _scoreTxt;
    [SerializeField] private GameObject _retryButton;
    [SerializeField] private GameObject _contiuneButton;
    [Space(5)]

    [Header("Timings")]
    [SerializeField] private float _niceTryFadeDuration = 1f;
    [SerializeField] private float _textsFadeDuration = 1f;
    [SerializeField] private float _buttonsFadeDuration = 1f;
    [SerializeField] private float _showDelay = 1f;
    [Space(5)]

    [Header("Target UI Alpha")]
    [Range(0, 255)][SerializeField] private int _niceTryTargetAlpha = 190;
    [Range(0, 255)][SerializeField] private int _bestTargetAlpha = 185;
    [Range(0, 255)][SerializeField] private int _scoreTargetAlpha = 255;
    [Range(0, 255)][SerializeField] private int _buttonsTargetAlpha = 255;
    [Space(5)]

    private CanvasGroup _niceTryCg;
    private CanvasGroup _bestCg;
    private CanvasGroup _scoreCg;
    private CanvasGroup _retryCg;
    private CanvasGroup _continueCg;

    private Coroutine _showRoutine;
    private Coroutine _delayRoutine;

    private void Awake()
    {
        _niceTryCg = GetOrAddCanvasGroup(_niceTryImage);
        _bestCg = GetOrAddCanvasGroup(_bestTxt.gameObject);
        _scoreCg = GetOrAddCanvasGroup(_scoreTxt.gameObject);
        _retryCg = GetOrAddCanvasGroup(_retryButton);
        _continueCg = GetOrAddCanvasGroup(_contiuneButton);

        SetActiveSafe(_darkOverlay, false);
        SetAlphaAndActive(_niceTryCg, _niceTryImage, 0f, false);
        SetAlphaAndActive(_bestCg, _bestTxt.gameObject, 0f, false);
        SetAlphaAndActive(_scoreCg, _scoreTxt.gameObject, 0f, false);
        SetAlphaAndActive(_retryCg, _retryButton, 0f, false);
        SetAlphaAndActive(_continueCg, _contiuneButton, 0f, false);
    }

    public void ShowGameOverPanel()
    {
        if (_delayRoutine != null)
            StopCoroutine(_delayRoutine);

        _delayRoutine = StartCoroutine(DelayStart());
    }

    public void HideGameOverPanel()
    {
        if (_showRoutine != null) StopCoroutine(_showRoutine);
        if (_delayRoutine != null) StopCoroutine(_delayRoutine);

        SetActiveSafe(_darkOverlay, false);
        SetAlphaAndActive(_niceTryCg, _niceTryImage, 0f, false);
        SetAlphaAndActive(_bestCg, _bestTxt.gameObject, 0f, false);
        SetAlphaAndActive(_scoreCg, _scoreTxt.gameObject, 0f, false);
        SetAlphaAndActive(_retryCg, _retryButton, 0f, false);
        SetAlphaAndActive(_continueCg, _contiuneButton, 0f, false);

        SetActiveSafe(_bg, true);
        SetActiveSafe(_gameScoreTxt, true);
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSecondsRealtime(_showDelay);
        StartToGameOverPanel();
    }

    public void StartToGameOverPanel()
    {
        if (_showRoutine != null)
            StopCoroutine(_showRoutine);

        _showRoutine = StartCoroutine(ShowSequence());
    }

    private IEnumerator ShowSequence()
    {
        SetActiveSafe(_darkOverlay, true);
        SetActiveSafe(_bg, false);
        SetActiveSafe(_gameScoreTxt, false);

        SetAlphaAndActive(_niceTryCg, _niceTryImage, 0f, true);
        SetAlphaAndActive(_bestCg, _bestTxt.gameObject, 0f, true);
        SetAlphaAndActive(_scoreCg, _scoreTxt.gameObject, 0f, true);
        SetAlphaAndActive(_retryCg, _retryButton, 0f, true);
        SetAlphaAndActive(_continueCg, _contiuneButton, 0f, true);

        float niceTryTarget = To01(_niceTryTargetAlpha);
        yield return FadeCanvasGroup(_niceTryCg, 0f, niceTryTarget, _niceTryFadeDuration);

        float bestTarget = To01(_bestTargetAlpha);
        float scoreTarget = To01(_scoreTargetAlpha);

        Coroutine bestRoutine = null;
        Coroutine scoreRoutine = null;

        if (_bestCg != null)
            bestRoutine = StartCoroutine(FadeCanvasGroup(_bestCg, 0f, bestTarget, _textsFadeDuration));
        if (_scoreCg != null)
            scoreRoutine = StartCoroutine(FadeCanvasGroup(_scoreCg, 0f, scoreTarget, _textsFadeDuration));

        if (bestRoutine != null) yield return bestRoutine;
        if (scoreRoutine != null) yield return scoreRoutine;

        float buttonsTarget = To01(_buttonsTargetAlpha);

        Coroutine retryRoutine = null;
        Coroutine continueRoutine = null;

        if (_retryCg != null)
            retryRoutine = StartCoroutine(FadeCanvasGroup(_retryCg, 0f, buttonsTarget, _buttonsFadeDuration));
        if (_continueCg != null)
            continueRoutine = StartCoroutine(FadeCanvasGroup(_continueCg, 0f, buttonsTarget, _buttonsFadeDuration));

        if (retryRoutine != null) yield return retryRoutine;
        if (continueRoutine != null) yield return continueRoutine;

        _showRoutine = null;
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to, float duration)
    {
        if (cg == null)
            yield break;

        if (duration <= 0f)
        {
            cg.alpha = to;
            yield break;
        }

        cg.alpha = from;

        float time = 0f;
        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(time / duration);
            cg.alpha = Mathf.Lerp(from, to, t);
            yield return null;
        }

        cg.alpha = to;
    }

    private static float To01(int alpha255)
    {
        alpha255 = Mathf.Clamp(alpha255, 0, 255);
        return alpha255 / 255f;
    }

    private static void SetActiveSafe(GameObject go, bool active)
    {
        if (go != null)
            go.SetActive(active);
    }

    private static void SetAlphaAndActive(CanvasGroup cg, GameObject go, float alpha, bool active)
    {
        if (go != null)
            go.SetActive(active);

        if (cg != null)
            cg.alpha = alpha;
    }

    private static CanvasGroup GetOrAddCanvasGroup(GameObject go)
    {
        if (go == null) return null;

        var cg = go.GetComponent<CanvasGroup>();
        if (cg == null)
            cg = go.AddComponent<CanvasGroup>();

        return cg;
    }

    public void SetScores(int score, int best)
    {
        _bestTxt.SetText($"BEST: {best}");
        _scoreTxt.SetText($"SCORE: {score}");
    }
}