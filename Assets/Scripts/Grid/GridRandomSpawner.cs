using System.Collections.Generic;
using UnityEngine;

public class GridRandomSpawner : BaseGridSpawner
{
    private static GridRandomSpawner instance;
    public Sprite[] randomSprites; 
    public static GridRandomSpawner Instance { get => instance; set => instance = value; }

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
                newCell.name = $"CellPlay ({row}, {col})";
                GameManager.Instance.CellItemManager.CellRandomPrefabs.Add(newCell);
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
}
