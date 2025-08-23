using System.Collections;
using System.Collections.Generic;
using DG.Tweening; // cần cho DOTween
using UnityEngine;
using UnityEngine.UI;

public class GridCellSpawner : BaseGridSpawner
{
    [Header("Cell Grid Spawner Components")]
    private static GridCellSpawner instance;
    public Sprite[] randomSprites; // danh sách sprite ngẫu nhiên để gán vào cell
    public Camera mainCamera; // Camera chính để shake

    public static GridCellSpawner Instance
    {
        get => instance;
    }

    protected override void Awake()
    {
        base.Awake();
        instance = this;
        mainCamera = Camera.main;
    }

    public override void SpawnGrid()
    {
        StartCoroutine(SpawnGridCoroutine());
    }

    private IEnumerator SpawnGridCoroutine()
    {
        int index = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (row == rows - 1 && col == cols - 1)
                    continue;

                // Tạo ô vuông UI (Grid Layout Group sẽ tự sắp xếp)
                Transform newCell = CellItemSpawner.Instance.spawnCellItem(
                    CellItemSpawner.cellItemName,
                    Vector3.zero,
                    Quaternion.identity
                );
                newCell.SetParent(transform, false);
                newCell.name = "cellBackground";
                GameManager.Instance.CellItemManager.CellPrefabs.Add(newCell.gameObject);

                // Reset scale về 0 để chuẩn bị hiệu ứng
                newCell.transform.localScale = Vector3.zero;
                // Hiệu ứng nảy lên bằng DOTween
                newCell
                    .transform.DOScale(Vector3.one, 0.4f)
                    .SetEase(Ease.OutBack)
                    .SetDelay(index * 0.03f);

                // Gán sprite ngẫu nhiên
                SpriteRenderer sr = newCell.GetComponent<SpriteRenderer>();
                if (sr != null && randomSprites.Length > 0)
                {
                    sr.sprite = randomSprites[Random.Range(0, randomSprites.Length)];
                    TypeItem typeItem = newCell.GetComponent<TypeItem>();
                    if (typeItem != null)
                    {
                        typeItem.getTypeByIndex(
                            int.Parse(sr.sprite.name[sr.sprite.name.Length - 1].ToString())
                        );
                        typeItem.IsClick = isClick.True;
                        Type type = typeItem.Type;
                        GameManager.Instance.CellItemManager.addCountItem(type);
                    }
                }

                index++;
            }
        }

        // Chờ đủ thời gian spawn hết grid
        float totalDelay = index * 0.03f + 0.5f; // delay + scale time
        yield return new WaitForSeconds(totalDelay);

        // Camera shake nhẹ
        if (mainCamera != null)
        {
            mainCamera.transform.DOShakePosition(0.3f, 0.2f, 10, 90, false, true);
        }
    }
}
