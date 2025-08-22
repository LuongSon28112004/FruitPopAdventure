using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TotalItem
{
    Leve1 = 31,
    Level2 = 63,
}

public class GameManager : BaseMonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get => instance;
    }

    [Header("Setting GameManager")]
    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private CellItemManager cellItemManager;

    [SerializeField]
    private DonePanelManager donePannelManager;

    [SerializeField]
    private int remainingQuantity = 0;

    [Header("Getter & Setter")]
    public CellItemManager CellItemManager
    {
        get => cellItemManager;
        set => cellItemManager = value;
    }
    public DonePanelManager DonePannelManager
    {
        get => donePannelManager;
        set => donePannelManager = value;
    }
    public int RemainingQuantity
    {
        get => remainingQuantity;
        set => remainingQuantity = value;
    }

    [SerializeField]
    private bool isDone;
    public bool IsDone
    {
        get => isDone;
        set => isDone = value;
    }

    [SerializeField]
    private bool isGameOver;
    public bool IsGameOver
    {
        get => isGameOver;
        set => isGameOver = value;
    }

    [SerializeField]
    private bool checkLoss;
    public bool CheckLoss
    {
        get => checkLoss;
        set => checkLoss = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if (levelManager == null)
            levelManager = transform.Find("LevelManager").GetComponent<LevelManager>();
        if (cellItemManager == null)
            cellItemManager = transform.Find("CellItemManager").GetComponent<CellItemManager>();
        if (donePannelManager == null)
            donePannelManager = transform.Find("WinPanelManager").GetComponent<DonePanelManager>();
        if (instance == null)
            instance = this;
    }

    protected override void Start()
    {
        base.Start();
        levelManager.InitLevel(LevelCtrl.Instance.Level);
        levelManager.LoadLevel();
        remainingQuantity = getTotalItem(LevelCtrl.Instance.Level);
        remainingQuantity = remainingQuantity - cellItemManager.CellPrefabs.Count;
    }

    protected override void Update()
    {
        this.DoneLevel();
        this.DontLevel();
    }

    private void DoneLevel()
    {
        if (
            CellItemManager.CellPrefabs.Count == 0
            && CellItemManager.CellRandomPrefabs.Count == 0
            && !isDone
        )
        {
            StartCoroutine(ShowDoneLevel());
        }
    }

    private IEnumerator ShowDoneLevel()
    {
        isDone = true;
        yield return new WaitForSeconds(1f);
        isGameOver = true;
        donePannelManager.Show(999, isDone);
    }

    private void DontLevel()
    {
        if (GridPlaySpawner.Instance.IsFull && !checkLoss)
        {
            StartCoroutine(ShowDontLevel());
        }
    }

    private IEnumerator ShowDontLevel()
    {
        isDone = false;
        checkLoss = true;
        yield return new WaitForSeconds(1f);
        isGameOver = true;
        donePannelManager.Show(999, isDone);
    }

    public int getTotalItem(Level level)
    {
        if (level == Level.Level1)
            return (int)TotalItem.Leve1;
        else
            return (int)TotalItem.Level2;
    }
}
