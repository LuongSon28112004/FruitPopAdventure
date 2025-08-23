using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stars", menuName = "SO/Stars", order = 0)]
[Serializable]
public class Stars : ScriptableObject
{
    public List<Sprite> stars;
}

