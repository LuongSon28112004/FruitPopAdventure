using System.Collections.Generic;
using UnityEngine;


public enum TotalItem
{
    Leve1 = 55,
    Level2 = 63,
}


public class GameManager : BaseMonoBehaviour
{
    [Header("Setting GameManager")]
    private static GameManager instance;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private CellItemManager cellItemManager;
    [SerializeField] private WinPanelManager winPannelManager;
    [SerializeField] private int remainingQuantity = 0;

    [Header("Getter & Setter")]
    public static GameManager Instance { get => instance; }
    public CellItemManager CellItemManager { get => cellItemManager; set => cellItemManager = value; }
    public WinPanelManager WinPannelManager { get => winPannelManager; set => winPannelManager = value; }
    public int RemainingQuantity { get => remainingQuantity; set => remainingQuantity = value; }


    protected override void Awake()
    {
        base.Awake();
        if (levelManager == null) levelManager = transform.Find("LevelManager").GetComponent<LevelManager>();
        if (cellItemManager == null) cellItemManager = transform.Find("CellItemManager").GetComponent<CellItemManager>();
        if (winPannelManager == null) winPannelManager = transform.Find("WinPanelManager").GetComponent<WinPanelManager>();
        if (instance == null) instance = this;
    }

    protected override void Start()
    {
        base.Start();
        levelManager.InitLevel(levelManager.Level);
        levelManager.LoadLevel();
        remainingQuantity = getTotalItem(levelManager.Level);
        remainingQuantity = remainingQuantity - cellItemManager.CellPrefabs.Count;
        GameManager.Instance.WinPannelManager.ShowWin(999);
    }

    public int getTotalItem(Level level)
    {
        if (level == Level.Level1) return (int)TotalItem.Leve1;
        else return (int)TotalItem.Level2;
    }
}
