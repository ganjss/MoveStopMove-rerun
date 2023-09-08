using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    None = 0,
    Red = 1,
    Blue = 2,
    Green = 3,
    Yellow = 4
}

[CreateAssetMenu(menuName = "Color Data")]
public class ColorData : ScriptableObject
{
    [SerializeField] Material[] materials;

    public Material GetMaterial(ColorType colorType)
    {
        return materials[(int)colorType];
    }
}


