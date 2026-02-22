using UnityEngine;

public class PlayerSkinHandler : MonoBehaviour
{

    [SerializeField] private Renderer playerRenderer;


    [SerializeField] private SkinData[] availableSkins;


    private const string PREF_SKIN_KEY = "SelectedSkinName";

    private void Start()
    {

        LoadSavedSkin();
    }

    private void OnEnable()
    {
        SkinButton.OnSkinSelected += ApplySkin;
    }

    private void OnDisable()
    {
        SkinButton.OnSkinSelected -= ApplySkin;
    }

    private void ApplySkin(SkinData selectedSkin)
    {
        if (selectedSkin != null && selectedSkin.skinTexture != null && playerRenderer != null)
        {

            playerRenderer.material.mainTexture = selectedSkin.skinTexture;

            PlayerPrefs.SetString(PREF_SKIN_KEY, selectedSkin.skinName);
            PlayerPrefs.Save(); 
        }
        else
        {
            Debug.LogWarning("Skin change  failed skinData, texture or player rnderer null");
        }
    }

    private void LoadSavedSkin()
    {

        if (PlayerPrefs.HasKey(PREF_SKIN_KEY))
        {
            string savedSkinName = PlayerPrefs.GetString(PREF_SKIN_KEY);

            foreach (SkinData skin in availableSkins)
            {
                if (skin.skinName == savedSkinName)
                {
                    ApplySkin(skin);
                    return;
                }
            }
        }


        if (availableSkins.Length > 0)
        {
            ApplySkin(availableSkins[0]);
        }
    }
}