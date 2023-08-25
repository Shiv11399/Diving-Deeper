using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum ElementType
{
    Stone = 0,
    Gold = 1,
}
public class Element : MonoBehaviour
{

    [SerializeField]private ElementType type;

    public ElementType Type { get => type; set => type = value; }

    public async void Destroy()
    {
        PlayBreakEffect();
        await Task.Delay(1000);
        Destroy(gameObject);
        await Task.Delay(1000);
    }

    private void PlayBreakEffect()
    {

    }
}
