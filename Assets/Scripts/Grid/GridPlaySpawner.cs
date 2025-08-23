using System.Collections;
using System.Collections.Generic;
using DG.Tweening; // cần cho DOTween
using UnityEngine;

public class GridPlaySpawner : BaseGridSpawner
{
    private static GridPlaySpawner instance;
    public static GridPlaySpawner Instance => instance;

    [Header("Play Grid Spawner Components")]
    [SerializeField]
    private List<GameObject> cellPlayPrefabs; // các ô

    [SerializeField]
    private List<GameObject> placedItems; // item đang nằm trong từng ô

    [SerializeField]
    private int countIndex = 0;

    [SerializeField]
    private bool isFull;

    public List<GameObject> CellPlayPrefabs => cellPlayPrefabs;
    public List<GameObject> PlacedItems => placedItems;
    public int CountIndex
    {
        get => countIndex;
        set => countIndex = value;
    }
    public bool IsFull
    {
        get => isFull;
        set => isFull = value;
    }

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
                Transform newCell = CellItemSpawner.Instance.spawnCellItem(
                    CellItemSpawner.cellItemName,
                    Vector3.zero,
                    Quaternion.identity
                );
                newCell.SetParent(transform, false);
                newCell.name = "cellBackground";
                cellPlayPrefabs.Add(newCell.gameObject);

                // Cho scale = 0 trước khi animate
                newCell.transform.localScale = Vector3.zero;

                TypeItem typeItem = newCell.GetComponent<TypeItem>();
                if (typeItem != null)
                    typeItem.IsClick = isClick.False;
            }
        }

        // danh sách item song song với số ô
        placedItems = new List<GameObject>(cellPlayPrefabs.Count);
        for (int i = 0; i < cellPlayPrefabs.Count; i++)
            placedItems.Add(null);

        // Bắt đầu hiệu ứng hiển thị lần lượt
        StartCoroutine(PlaySpawnEffect());
    }

    private IEnumerator PlaySpawnEffect()
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
