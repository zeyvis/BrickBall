using UnityEngine;

public class PlayerVfx : MonoBehaviour
{
    public SkinData defaultSkin;

    private static SkinData lastSelectedSkin;

    private GameObject activeVFXInstance;

    private void Start()
    {
        if (lastSelectedSkin != null)
        {
            UpdateVFX(lastSelectedSkin);
        }
        else if (defaultSkin != null)
        {
            UpdateVFX(defaultSkin);
            lastSelectedSkin = defaultSkin;
        }
    }

    private void OnEnable()
    {
        SkinButton.OnSkinSelected += OnSkinSelectedHandler;
    }

    private void OnDisable()
    {
        SkinButton.OnSkinSelected -= OnSkinSelectedHandler;
    }

    private void OnSkinSelectedHandler(SkinData selectedSkin)
    {

        lastSelectedSkin = selectedSkin;

        UpdateVFX(selectedSkin);
    }

    private void UpdateVFX(SkinData selectedSkin)
    {
        if (activeVFXInstance != null)
        {
            Destroy(activeVFXInstance);
        }

        if (selectedSkin != null && selectedSkin.vfxTrailPrefab != null)
        {
            activeVFXInstance = Instantiate(selectedSkin.vfxTrailPrefab, transform);
            activeVFXInstance.transform.localPosition = Vector3.zero;
            activeVFXInstance.transform.localRotation = Quaternion.identity;

            SkinVFXBundle vfxBundle = activeVFXInstance.GetComponent<SkinVFXBundle>();

            if (vfxBundle != null)
            {
                GetComponent<SpeedBoostController>().SetNewVFX(vfxBundle);
            }
            else
            {
                Debug.LogWarning("Playervfx:the SkinVFXBundle script is missing from the VFX Prefab");
            }
        }
    }
}