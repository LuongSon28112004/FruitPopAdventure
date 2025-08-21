using TMPro;
using UnityEngine;

public abstract class BaseText : BaseMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI text;

    protected override void Awake()
    {
        base.Awake();
        text = GetComponent<TextMeshProUGUI>();
    }
}
