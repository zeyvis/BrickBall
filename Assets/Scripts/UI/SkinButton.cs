using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class SkinButton : MonoBehaviour
{

    public static event Action<SkinData> OnSkinSelected;

    [SerializeField] private SkinData skinData;
    private Button skinButton;

    private void Awake()
    {
        skinButton = GetComponent<Button>();


        skinButton.onClick.AddListener(SelectSkin);
    }

    private void SelectSkin()
    {

        OnSkinSelected?.Invoke(skinData);
    }
}