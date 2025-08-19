using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public abstract class BaseGridSpawner : BaseMonoBehaviour
{
    [SerializeField] protected GameObject cellPrefab;     // Prefab của ô vuông (UI - có Image)
    [SerializeField] protected int rows = 7;              // số hàng
    [SerializeField] protected int cols = 7;              // số cột

    protected override void Start()
    {
        SpawnGrid();
    }

    protected abstract void SpawnGrid();
   
}