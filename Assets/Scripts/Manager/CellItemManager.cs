using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CellItemManager : BaseMonoBehaviour
{
    [SerializeField]
    private GameObject Holder;

    [SerializeField]
    private GameObject cellPrefab;

    [SerializeField]
    private Sprite[] randomSprites;

    [SerializeField]
    private List<GameObject> cellRandomPrefabs; //random

    [SerializeField]
    private List<GameObject> cellPrefabs; //cell

    [SerializeField]
    private Dictionary<Type, int> dict = new Dictionary<Type, int>();

    public List<GameObject> CellPrefabs
    {
        get => cellPrefabs;
        set => cellPrefabs = value;
    }
    public List<GameObject> CellRandomPrefabs
    {
        get => cellRandomPrefabs;
        set => cellRandomPrefabs = value;
    }
    public Dictionary<Type, int> Dict
    {
        get => dict;
        set => dict = value;
    }

    [SerializeField]
    private bool isWin;
    public bool IsWin
    {
        get => isWin;
        set => isWin = value;
    }
    public event Action<int> OnWinGame;

    protected override void Update()
    {
        this.DoneLevel();
    }

    private void DoneLevel()
    {
        if (cellRandomPrefabs.Count == 0 && cellPrefabs.Count == 0 && !isWin)
        {
            OnWinGame?.Invoke(999);
            isWin = true;
        }
    }

    public virtual void addItem()
    {
        var newCell = Instantiate(cellPrefab, Holder.transform);
        newCell.name = $"CellRandom";
        cellRandomPrefabs.Add(newCell);
        SpriteRenderer sr = newCell.GetComponent<SpriteRenderer>();
        if (sr != null && randomSprites.Length > 0)
        {
            sr.sprite = randomSprites[this.randomItem()];
            TypeItem typeItem = newCell.GetComponent<TypeItem>();
            if (typeItem != null)
            {
                typeItem.getTypeByIndex(
                    int.Parse(sr.sprite.name[sr.sprite.name.Length - 1].ToString())
                );
                typeItem.IsClick = isClick.False;
                this.addCountItem(typeItem.Type);
            }
        }
    }

    public void addCountItem(Type typeItem)
    {
        if (dict.ContainsKey(typeItem))
        {
            dict[typeItem] += 1;
        }
        else
        {
            dict.Add(typeItem, 1);
        }
    }

    public int randomItem()
    {
        var needFix = new List<int>();
        foreach (var kvp in dict)
        {
            Debug.Log(kvp.Key + ": " + kvp.Value);
            if (kvp.Value % 3 != 0)
            {
                needFix.Add(TypeHelper.ToInt(kvp.Key));
            }
        }

        for (int i = 0; i < needFix.Count; i++)
        {
            Debug.Log(needFix[i]);
        }

        if (needFix.Count > 0)
        {
            int randIndex = UnityEngine.Random.Range(0, needFix.Count); // random index
            return needFix[randIndex]; 
        }
        return UnityEngine.Random.Range(0, 7);
    }
}
