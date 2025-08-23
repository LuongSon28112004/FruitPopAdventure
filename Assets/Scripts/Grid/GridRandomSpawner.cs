using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GridRandomSpawner : BaseGridSpawner
{
    private static GridRandomSpawner instance;
    public static GridRandomSpawner Instance
    {
        get => instance;
        set => instance = value;
    }

    [Header("Random Grid Spawner Components")]
    public Sprite[] randomSprites;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public override void SpawnGrid()
    {
        List<GameObject> cellRandomPrefabs = new List<GameObject>();
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                 Transform newCell = CellItemSpawner.Instance.spawnCellItem(
                    CellItemSpawner.cellItemName,
                    Vector3.zero,
                    Quaternion.identity
                );
                newCell.SetParent(transform, false);
                newCell.name = "cellBackground";
                GameManager.Instance.CellItemManager.CellRandomPrefabs.Add(newCell.gameObject);
                SpriteRenderer sr = newCell.GetComponent<SpriteRenderer>();
                if (sr != null && randomSprites.Length > 0)
                {
                    sr.sprite = randomSprites[GameManager.Instance.CellItemManager.randomItem()];
                    TypeItem typeItem = newCell.GetComponent<TypeItem>();
                    if (typeItem != null)
                    {
                        typeItem.getTypeByIndex(
                            int.Parse(sr.sprite.name[sr.sprite.name.Length - 1].ToString())
                        );
                        typeItem.IsClick = isClick.False;
                        Type type = typeItem.Type;
                        GameManager.Instance.CellItemManager.addCountItem(type);
                    }
                }
                newCell.transform.localScale = Vector3.zero;
                cellRandomPrefabs.Add(newCell.gameObject);
            }
        }

        // Bắt đầu hiệu ứng hiển thị lần lượt
        StartCoroutine(PlaySpawnEffect(cellRandomPrefabs));
    }

    private IEnumerator PlaySpawnEffect(List<GameObject> cellPlayPrefabs)
    {
        int index = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int cellIndex = row * cols + col;
                GameObject cell = cellPlayPrefabs[cellIndex];

                // Animate từ nhỏ -> lớn
                cell.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);

                // Chờ một chút trước khi tới cell tiếp theo
                yield return new WaitForSeconds(0.1f);

                index++;
            }
        }
    }
}
