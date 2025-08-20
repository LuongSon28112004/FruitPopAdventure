using UnityEngine;
using UnityEngine.UI;

public enum Level
{
    Level1 = 5,
    Level2 = 6,
}

public class LevelManager : BaseMonoBehaviour
{
    public Level Level { get => level; set => level = value; }
    [SerializeField] private Level level;
    protected override void Start()
    {
        base.Start();
        level = Level.Level1;
    }
    public void InitLevel(Level level)
    {
        GridLayoutGroup gridLayoutCell;
        GridLayoutGroup gridLayoutPlay;
        GridLayoutGroup gridLayoutRandom;
        switch (level)
        {

            case Level.Level1:
                gridLayoutCell = GameObject.Find("GridCell").GetComponent<GridLayoutGroup>();
                gridLayoutCell.constraintCount = 5;
                gridLayoutPlay = GameObject.Find("GridPlay").GetComponent<GridLayoutGroup>();
                gridLayoutPlay.constraintCount = 5;
                gridLayoutRandom = GameObject.Find("GridRandom").GetComponent<GridLayoutGroup>();
                gridLayoutRandom.constraintCount = 5;
                GridCellSpawner.Instance.Cols = 5;
                GridCellSpawner.Instance.Rows = 5;
                GridPlaySpawner.Instance.Cols = 4;
                GridPlaySpawner.Instance.Rows = 1;
                GridRandomSpawner.Instance.Cols = 5;
                GridRandomSpawner.Instance.Rows = 1;
                break;
            case Level.Level2:
                gridLayoutCell = GameObject.Find("GridCell").GetComponent<GridLayoutGroup>();
                gridLayoutCell.constraintCount = 6;
                gridLayoutPlay = GameObject.Find("GridPlay").GetComponent<GridLayoutGroup>();
                gridLayoutPlay.constraintCount = 6;
                gridLayoutRandom = GameObject.Find("GridRandom").GetComponent<GridLayoutGroup>();
                gridLayoutRandom.constraintCount = 6;
                GridCellSpawner.Instance.Cols = 6;
                GridCellSpawner.Instance.Rows = 6;
                GridPlaySpawner.Instance.Cols = 5;
                GridPlaySpawner.Instance.Rows = 1;
                GridRandomSpawner.Instance.Cols = 6;
                GridRandomSpawner.Instance.Rows = 1;
                break;
        }
    }

    public void LoadLevel()
    {
        GridCellSpawner.Instance.SpawnGrid();
        GridPlaySpawner.Instance.SpawnGrid();
        GridRandomSpawner.Instance.SpawnGrid();
    }

}
