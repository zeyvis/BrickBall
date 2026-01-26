using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int _gridSize = 10;
    [SerializeField] private float _cubeSize = 3f;

    [Header("prefab and Rotation Settings")]
    [SerializeField] private GameObject[] _cubePrefabs;

    [SerializeField] private float _fixedXRotation = -90f;

    private readonly float[] _possibleYRotations = { 0f, 90f, 180f, 270f };

    private void Start()
    {
        if (_cubePrefabs == null || _cubePrefabs.Length == 0)
        {
            Debug.LogError("küp prefabý yok");
            return;
        }

        CreatePlatform();
    }

    public void CreatePlatform()
    {
        GameObject container = new GameObject("Platform_Container");
        container.transform.SetParent(this.transform);

        float offset = (_gridSize * _cubeSize) / 2f - (_cubeSize / 2f);

        for (int x = 0; x < _gridSize; x++)
        {
            //pozisyon hesapla
            for (int z = 0; z < _gridSize; z++)
            {
                Vector3 spawnPosition = new Vector3(
                    (x * _cubeSize) - offset,
                    0,
                    (z * _cubeSize) - offset
                );


                int randomPrefabIndex = Random.Range(0, _cubePrefabs.Length);
                GameObject selectedPrefab = _cubePrefabs[randomPrefabIndex];

                int randomYIndex = Random.Range(0, _possibleYRotations.Length);
                float selectedYAngle = _possibleYRotations[randomYIndex];
                Quaternion currentRotation = Quaternion.Euler(_fixedXRotation, selectedYAngle, 0);

                GameObject newCube = Instantiate(selectedPrefab, spawnPosition, currentRotation);
                newCube.transform.SetParent(container.transform);
                newCube.name = $"Cube_{x}_{z}_(Prefab_{randomPrefabIndex})";
            }
        }
    }
}