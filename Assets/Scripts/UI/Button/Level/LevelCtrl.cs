using UnityEngine;

public class LevelCtrl : BaseMonoBehaviour
{
    private static LevelCtrl instance;

    public static LevelCtrl Instance
    {
        get => instance;
    }

    [SerializeField]
    private Level level;
    public Level Level { get => level; set => level = value; }

    protected override void Awake()
    {
        base.Awake();
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
