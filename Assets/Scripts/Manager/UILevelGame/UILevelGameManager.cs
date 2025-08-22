using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UILevelGameManager : BaseMonoBehaviour
{
    private static UILevelGameManager instance;
    public static UILevelGameManager Instance
    {
        get => instance;
        set => instance = value;
    }

    [Header("UI Level Game Components")]
    [SerializeField]
    private List<StarsEffect> allStars = new List<StarsEffect>();
    public List<StarsEffect> AllStars { get => allStars;}
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        this.LoadStarsEffect();
    }
    private void LoadStarsEffect()
    {
       foreach (Transform level in transform) // duyệt qua từng Level
        {
            Transform stars = level.Find("Stars"); // tìm Stars trong Level
             StarsEffect effect = stars.GetComponent<StarsEffect>();
            if (effect != null)
            {
                allStars.Add(effect);
            }
        }
    }
}
