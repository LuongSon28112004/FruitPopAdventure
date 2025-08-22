using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ItemClickController : MonoBehaviour
{
    [Header("Cell Grid Components")]
    [SerializeField]
    private GameObject CellGrid;

    void Update()
    {
        this.ClickItem();
    }

    private void ClickItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == null)
                return;
            TypeItem typeItem = hit.collider.GetComponent<TypeItem>();
            if (typeItem == null || typeItem.IsClick == isClick.False)
            {
                AudioManager.Instance.PlaySound(AudioManager.Instance.errorSound);
                return;
            }

            var item = hit.collider.gameObject;
            MoveToBottomSlot(item);
            AudioManager.Instance.PlaySound(AudioManager.Instance.clickSound);
            if (GameManager.Instance.CellItemManager.CellRandomPrefabs.Count != 0)
            {
                GameObject randomItem = GameManager.Instance.CellItemManager.CellRandomPrefabs[0];
                TypeItem typeItem1 = randomItem.GetComponent<TypeItem>();
                if (typeItem1 != null)
                    typeItem1.IsClick = isClick.True;
                randomItem.transform.SetParent(CellGrid.transform, false);
                GameManager.Instance.CellItemManager.CellPrefabs.Add(randomItem);
                GameManager.Instance.CellItemManager.CellRandomPrefabs.RemoveAt(0);
            }
            if (GameManager.Instance.RemainingQuantity != 0)
            {
                GameManager.Instance.CellItemManager.addItem();
                GameManager.Instance.RemainingQuantity--;
            }
        }
    }

    [SerializeField]
    private GameObject fragmentPrefab;

    [SerializeField]
    private int fragmentCount = 8;

    [SerializeField]
    private float explosionRadius = 1f;

    // danh sách màu explosion
    [SerializeField]
    private Color[] explosionColors =
    {
        Color.red,
        new Color(1f, 0.5f, 0f), 
        Color.yellow,
        Color.cyan,
        Color.magenta,
    };

    private void DoExplosion(Vector3 position, Color baseColor)
    {
        for (int i = 0; i < fragmentCount; i++)
        {
            // Random hướng bay ra
            Vector2 dir2D = UnityEngine.Random.insideUnitCircle.normalized * explosionRadius;
            Vector3 target = position + new Vector3(dir2D.x, dir2D.y, 0f);

            // Random màu (không phải lúc nào cũng màu đỏ)
            Color chosenColor = explosionColors[
                UnityEngine.Random.Range(0, explosionColors.Length)
            ];

            GameObject frag = Instantiate(fragmentPrefab, position, Quaternion.identity);
            var sr = frag.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = chosenColor;

            // Tween bay ra + nổ to + biến mất
            Sequence seq = DOTween.Sequence();
            seq.Append(frag.transform.DOScale(2f, 0.15f)); // phóng to nhanh -> cảm giác nổ
            seq.Append(frag.transform.DOScale(0f, 0.35f)); // rồi nhỏ dần biến mất
            seq.Join(frag.transform.DOMove(target, 0.5f).SetEase(Ease.OutCubic));
            if (sr != null)
                seq.Join(sr.DOFade(0f, 0.5f));
            seq.OnComplete(() => Destroy(frag));
        }
    }

    protected virtual void MoveToBottomSlot(GameObject item)
    {
        TypeItem typeItem = item.GetComponent<TypeItem>();
        if (typeItem == null)
            return;
        typeItem.IsClick = isClick.False;
        var spawner = GridPlaySpawner.Instance;
        if (spawner.CountIndex >= spawner.CellPlayPrefabs.Count)
            return;

        int index = spawner.CountIndex;
        Transform target = spawner.CellPlayPrefabs[index].transform;

        item.transform.DOMove(target.position, 0.5f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                spawner.PlacedItems[index] = item;
                item.transform.SetParent(target);

                CheckMatchAny();
            });
        GameManager.Instance.CellItemManager.CellPrefabs.Remove(item);

        spawner.CountIndex++;
    }

    // Chỉ cần 3 item cùng loại trong 5 ô đầu (không cần liên tiếp)
    protected virtual void CheckMatchAny()
    {
        var spawner = GridPlaySpawner.Instance;
        var items = spawner.PlacedItems;

        // Gom nhóm theo loại
        Dictionary<string, List<int>> groups = new Dictionary<string, List<int>>();
        for (int i = 0; i < spawner.CountIndex; i++)
        {
            var obj = items[i];
            if (obj == null)
                continue;
            var typeComp = obj.GetComponent<TypeItem>();
            if (typeComp == null)
                continue;

            string key = typeComp.Type.ToString();
            if (!groups.TryGetValue(key, out var list))
            {
                list = new List<int>();
                groups[key] = list;
            }
            list.Add(i);
        }

        // Lấy tất cả nhóm có >= 3
        HashSet<int> toRemove = new HashSet<int>();
        foreach (var kv in groups)
            if (kv.Value.Count >= 3)
            {
                foreach (int idx in kv.Value)
                    toRemove.Add(idx);
                AudioManager.Instance.PlaySound(AudioManager.Instance.matchSound);
            }

        if (toRemove.Count == 0)
            return;

        // Xóa với DOTween (tránh capture biến i)
        foreach (int idx in toRemove)
        {
            var obj = items[idx];
            if (obj == null)
                continue;

            items[idx] = null; // bỏ tham chiếu trước cho chắc

            var sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                // Tween đồng thời scale và color
                Sequence seq = DOTween.Sequence();
                seq.Join(obj.transform.DOScale(Vector3.zero, 0.25f));
                seq.Join(sr.DOColor(Color.red, 0.25f)); // đổi sang màu đỏ khi biến mất
                seq.OnComplete(() =>
                {
                    // gọi hiệu ứng nổ
                    DoExplosion(obj.transform.position, sr.color);
                    Destroy(obj);
                });
            }
            else
            {
                obj.transform.DOScale(Vector3.zero, 0.25f)
                    .OnComplete(() =>
                    {
                        DoExplosion(obj.transform.position, Color.yellow);
                        Destroy(obj);
                    });
            }
        }
        // Dịch các item còn lại để không bị lỗ hổng & cập nhật CountIndex
        CompactSlots();
    }

    private void CompactSlots()
    {
        var spawner = GridPlaySpawner.Instance;
        var slots = spawner.CellPlayPrefabs;
        var items = spawner.PlacedItems;

        int write = 0; // vị trí sẽ lấp tiếp theo
        for (int read = 0; read < items.Count; read++)
        {
            var obj = items[read];
            if (obj == null)
                continue;

            if (write != read)
            {
                items[write] = obj;
                items[read] = null;

                // Tween về vị trí ô mới
                obj.transform.DOMove(slots[write].transform.position, 0.25f).SetEase(Ease.OutQuad);
                obj.transform.SetParent(slots[write].transform);
            }
            write++;
        }

        spawner.CountIndex = write; // ô trống tiếp theo
    }
}
