using UnityEngine;
using UnityEngine.EventSystems;

public sealed class StartOverlayTapForwarder : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameStartController _gameStartController;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_gameStartController == null)
        {
            Debug.LogError($"{nameof(StartOverlayTapForwarder)}: GameStartControlle missing", this);
            return;
        }

        _gameStartController.StartGame();
    }
}