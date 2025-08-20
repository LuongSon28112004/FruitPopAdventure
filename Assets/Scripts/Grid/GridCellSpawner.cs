using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // cần cho Image

public class GridCellSpawner : BaseGridSpawner
{
    private static GridCellSpawner instance;
    public Sprite[] randomSprites;    // danh sách sprite ngẫu nhiên để gán vào cell

    public static GridCellSpawner Instance
    {
        get => instance;
    }

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public override void SpawnGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (row == rows - 1 && col == cols - 1) return;
                // Tạo ô vuông UI (Grid Layout Group sẽ tự sắp xếp)
                GameObject newCell = Instantiate(cellPrefab, transform);
                newCell.name = $"Cell ({row}, {col})";
                GameManager.Instance.CellItemManager.CellPrefabs.Add(newCell);
                // Gán sprite ngẫu nhiên
                SpriteRenderer sr = newCell.GetComponent<SpriteRenderer>();
                if (sr != null && randomSprites.Length > 0)
                {
                    sr.sprite = randomSprites[Random.Range(0, randomSprites.Length)];//GameManager.Instance.CellItemManager.randomItem()
                    TypeItem typeItem = newCell.GetComponent<TypeItem>();
                    if (typeItem != null)
                    {
                        typeItem.getTypeByIndex(int.Parse(sr.sprite.name[sr.sprite.name.Length - 1].ToString()));
                        typeItem.IsClick = isClick.True;
                        Type type = typeItem.Type;
                        GameManager.Instance.CellItemManager.addCountItem(type);
                    }
                }
            }
        }
    }

}

