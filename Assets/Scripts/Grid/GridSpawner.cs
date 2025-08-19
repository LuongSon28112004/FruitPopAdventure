using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // cần cho Image

public class GridCellSpawner : BaseGridSpawner
{
    public Sprite[] randomSprites;    // danh sách sprite ngẫu nhiên để gán vào cell

    protected override void Start()
    {
        SpawnGrid();
    }

    protected override void SpawnGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (row == rows - 1 && col == cols - 1) return;
                // Tạo ô vuông UI (Grid Layout Group sẽ tự sắp xếp)
                GameObject newCell = Instantiate(cellPrefab, transform);
                newCell.name = $"Cell ({row}, {col})";

                // Gán sprite ngẫu nhiên
                SpriteRenderer sr = newCell.GetComponent<SpriteRenderer>();
                if (sr != null && randomSprites.Length > 0)
                {
                    sr.sprite = randomSprites[Random.Range(0, randomSprites.Length)];
                    TypeItem typeItem = newCell.GetComponent<TypeItem>();
                    if (typeItem != null)
                    {
                        typeItem.getTypeByIndex(int.Parse(sr.sprite.name[sr.sprite.name.Length - 1].ToString()));
                    }
                }
            }
        }
    }

}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GridCellSpawner : BaseGridSpawner
// {
//     public Sprite[] randomSprites; // danh sách sprite ngẫu nhiên

//     protected override void Start()
//     {
//         SpawnGrid();
//     }

//     protected override void SpawnGrid()
//     {
//         int index = 0;
//         for (int row = 0; row < rows; row++)
//         {
//             for (int col = 0; col < cols; col++)
//             {
//                 GameObject newCell = Instantiate(cellPrefab, transform);
//                 newCell.name = $"Cell ({row}, {col})";

//                 SpriteRenderer sr = newCell.GetComponent<SpriteRenderer>();
//                 if (sr != null)
//                 {
//                     if (index < spritePool.Count) // 48 quân
//                     {
//                         sr.sprite = spritePool[index];
//                         TypeItem typeItem = newCell.GetComponent<TypeItem>();
//                         if (typeItem != null)
//                         {
//                             typeItem.getTypeByIndex(int.Parse(sr.sprite.name[sr.sprite.name.Length - 1].ToString()));
//                             typeItem.IsClick = isClick.True;
//                         }
//                         index++;
//                     }
//                     else
//                     {
//                         newCell.SetActive(false);
//                     }
//                 }
//             }
//         }
//     }
// }


