using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public abstract class BaseGridSpawner : BaseMonoBehaviour
{
    [Header("Base Grid Spawner Components")]
    [SerializeField] protected GameObject cellPrefab;     // Prefab của ô vuông (UI - có Image)
    [SerializeField] protected int rows = 7;              // số hàng
    [SerializeField] protected int cols = 7;              // số cột

    public int Rows { get => rows; set => rows = value; }
    public int Cols { get => cols; set => cols = value; }

    public abstract void SpawnGrid();
   
}