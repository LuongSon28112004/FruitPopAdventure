using System.Collections.Generic;
using UnityEngine;

public class CellItemManager : MonoBehaviour
{
    [SerializeField] private GameObject Holder;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Sprite[] randomSprites;
    [SerializeField] private List<GameObject> cellRandomPrefabs;
    [SerializeField] private List<GameObject> cellPrefabs;

    public List<GameObject> CellPrefabs { get => cellPrefabs; set => cellPrefabs = value; }
    public List<GameObject> CellRandomPrefabs { get => cellRandomPrefabs; set => cellRandomPrefabs = value; }

    public virtual void addItem()
    {
        var newCell = Instantiate(cellPrefab, Holder.transform);
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
