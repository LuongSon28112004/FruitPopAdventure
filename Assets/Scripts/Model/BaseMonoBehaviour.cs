using UnityEngine;

public class BaseMonoBehaviour : MonoBehaviour
{
    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void Resert()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {

    }
}
