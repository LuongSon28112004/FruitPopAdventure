using System.Collections.Generic;
using UnityEngine;

public class GridRandomSpawner : BaseGridSpawner
{
    private static GridRandomSpawner instance;
    public Sprite[] randomSprites; 
    [SerializeField] private List<GameObject> cellRandomPrefabs;   // các ô
    public static GridRandomSpawner Instance { get => instance; set => instance = value; }
    public List<GameObject> CellRandomPrefabs => cellRandomPrefabs;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    protected override void SpawnGrid()
    {
        cellRandomPrefabs = new List<GameObject>();
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                var newCell = Instantiate(cellPrefab, transform);
                newCell.name = $"CellPlay ({row}, {col})";
                cellRandomPrefabs.Add(newCell);
                SpriteRenderer sr = newCell.GetComponent<SpriteRenderer>();
                if (sr != null && randomSprites.Length > 0)
                {
                    sr.sprite = randomSprites[Random.Range(0, randomSprites.Length)];
                    TypeItem typeItem = newCell.GetComponent<TypeItem>();
                    if (typeItem != null)
                    {
                        typeItem.getTypeByIndex(int.Parse(sr.sprite.name[sr.sprite.name.Length - 1].ToString()));
                        typeItem.IsClick = isClick.False;
                    }
                }
            }
        }
    }

    public virtual void addItem()
    {
        var newCell = Instantiate(cellPrefab, transform);
        newCell.name = $"CellRandom";
        cellRandomPrefabs.Add(newCell);
        SpriteRenderer sr = newCell.GetComponent<SpriteRenderer>();
        if (sr != null && randomSprites.Length > 0)
        {
            sr.sprite = randomSprites[Random.Range(0, randomSprites.Length)];
            TypeItem typeItem = newCell.GetComponent<TypeItem>();
            if (typeItem != null)
            {
                typeItem.getTypeByIndex(int.Parse(sr.sprite.name[sr.sprite.name.Length - 1].ToString()));
                typeItem.IsClick = isClick.False;
            }
        }
    }
}
