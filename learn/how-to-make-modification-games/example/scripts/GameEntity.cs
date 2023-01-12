using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour
{
    [SerializeField] string id;
    [SerializeField] Sprite sprite;
    public DynValue function;

    void Start()
    {
        Initial();
    }
    public void SetEntity(string id, Sprite sprite, DynValue awakeFunction)
    {
        this.id = id;
        this.sprite = sprite;
        this.function = awakeFunction;
    }
    void Initial()
    {
        gameObject.name = id;
        GetComponent<SpriteRenderer>().sprite = sprite;

        if (function != null)
        {
            function.Function.Call();
        }
    }
        
}
