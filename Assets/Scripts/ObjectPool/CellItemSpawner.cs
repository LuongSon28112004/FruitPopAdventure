using UnityEngine;

public class CellItemSpawner : BaseobjectPool
{
    private static CellItemSpawner instance;

    public static CellItemSpawner Instance
    {
        get => instance;
    }
    public static string cellItemName = "cellBackground";

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
            instance = this;
    }

    public Transform spawnCellItem(string prefabName, Vector3 spawnPos, Quaternion rotation)
    {
        Transform prefab = this.Spawn(prefabName, spawnPos, rotation);
        prefab.gameObject.SetActive(true);
        prefab.GetComponent<SpriteRenderer>().sortingOrder = -1;
        prefab.GetComponent<SpriteRenderer>().color = Color.white;
        prefab.localScale = new Vector3(1, 1, 1);
        return prefab;
    }
}
