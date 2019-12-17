using UnityEngine;
using WDIB.Utilities;

[CreateAssetMenu(fileName = "TowerParameters", menuName = "Parameters/TowerParameters")]
public class TowerParameters : SingletonScriptableObject<TowerParameters>
{
    [SerializeField]
    private GameObject[] TowerPrefabs = null;

    public GameObject GetTowerPrefab(int ID)
    {
#if UNITY_EDITOR
        // if it is within range
        if (TowerPrefabs.Length - 1 < ID)
        {
            Debug.LogError($"TowerPrefab {ID} does not exist. Returning first TowerPrefab.");
            return TowerPrefabs[0];
        }
#endif

        return TowerPrefabs[ID];
    }
}
