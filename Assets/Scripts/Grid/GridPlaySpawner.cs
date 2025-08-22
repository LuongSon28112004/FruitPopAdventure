using System.Collections.Generic;
using UnityEngine;

public class GridPlaySpawner : BaseGridSpawner
{
    private static GridPlaySpawner instance;
    public static GridPlaySpawner Instance => instance;
    [Header("Play Grid Spawner Components")]
    [SerializeField] private List<GameObject> cellPlayPrefabs;   // các ô
    [SerializeField] private List<GameObject> placedItems;       // item đang nằm trong từng ô
    [SerializeField] private int countIndex = 0;

    public List<GameObject> CellPlayPrefabs => cellPlayPrefabs;
    public List<GameObject> PlacedItems => placedItems;
    public int CountIndex { get => countIndex; set => countIndex = value; }
    public bool IsFull { get => isFull; set => isFull = value; }

    [SerializeField] private bool isFull;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public override void SpawnGrid()
    {
        cellPlayPrefabs = new List<GameObject>();
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                var newCell = Instantiate(cellPrefab, transform);
                newCell.name = $"CellPlay ({row}, {col})";
                cellPlayPrefabs.Add(newCell);
                TypeItem typeItem = newCell.GetComponent<TypeItem>();
                if(typeItem != null) typeItem.IsClick = isClick.False;
            }
        }

        // danh sách item song song với số ô
        placedItems = new List<GameObject>(cellPlayPrefabs.Count);
        for (int i = 0; i < cellPlayPrefabs.Count; i++) placedItems.Add(null);
    }
}
