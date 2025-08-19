using UnityEngine;

public class GameManager : BaseMonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private CellItemManager cellItemManager;

    public static GameManager Instance { get => instance; }
    public CellItemManager CellItemManager { get => cellItemManager; set => cellItemManager = value; }

    protected override void Awake()
    {
        base.Awake();
        if (levelManager == null) levelManager = transform.Find("LevelManager").GetComponent<LevelManager>();
        if (cellItemManager == null) cellItemManager = transform.Find("CellItemManager").GetComponent<CellItemManager>();
        if(instance == null) instance = this;
    }

    protected override void Start()
    {
        base.Start();
        levelManager.InitLevel(levelManager.Level);
        levelManager.LoadLevel();
    }
}
