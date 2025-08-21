using TMPro;
using UnityEngine;

public abstract class BaseText : BaseMonoBehaviour
{
    [Header("BaseText Components")]
    [SerializeField] protected TextMeshProUGUI text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponent<TextMeshProUGUI>();
    }
}
