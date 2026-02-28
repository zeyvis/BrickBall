using UnityEngine;

[CreateAssetMenu(fileName = "NewSkinData", menuName = "Skin System/Skin Data")]
public class SkinData : ScriptableObject
{
    public string skinName;
    public Texture2D skinTexture;
    public GameObject vfxTrailPrefab;
}