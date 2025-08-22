using System.Collections.Generic;
using UnityEngine;

public class GridRandomSpawner : BaseGridSpawner
{
    private static GridRandomSpawner instance;
    public static GridRandomSpawner Instance { get => instance; set => instance = value; }
    [Header("Random Grid Spawner Components")]
    public Sprite[] randomSprites; 

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    public override void SpawnGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                var newCell = Instantiate(cellPrefab, transform);
                newCell.name = $"CellRandom ({row}, {col})";
                GameManager.Instance.CellItemManager.CellRandomPrefabs.Add(newCell);
                SpriteRenderer sr = newCell.GetComponent<SpriteRenderer>();
                if (sr != null && randomSprites.Length > 0)
                {
                    sr.sprite = randomSprites[GameManager.Instance.CellItemManager.randomItem()];
                    TypeItem typeItem = newCell.GetComponent<TypeItem>();
                    if (typeItem != null)
                    {
                        typeItem.getTypeByIndex(int.Parse(sr.sprite.name[sr.sprite.name.Length - 1].ToString()));
                        typeItem.IsClick = isClick.False;
                        Type type = typeItem.Type;
                        GameManager.Instance.CellItemManager.addCountItem(type);
                    }
                }
            }
        }
    }
}
