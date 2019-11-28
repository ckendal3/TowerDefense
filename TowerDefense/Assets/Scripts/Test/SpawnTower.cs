using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    public int TowerID = 0;

    // Start is called before the first frame update
    void Start()
    {
        TowerFactory.CreateTower(TowerID, transform.position, transform.rotation);
    }
}
