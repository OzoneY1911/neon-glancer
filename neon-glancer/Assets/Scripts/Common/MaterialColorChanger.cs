using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorChanger
{
    public static void SetMaterialColor(Material matTarget, Color matColor, Color emitColor)
    {
        matTarget.SetColor("_Color", matColor);
        matTarget.SetColor("_EmissionColor", emitColor);
    }

    public static void SetMaterialColor(Material matTarget, Color color)
    {
        matTarget.SetColor("_Color", color);
        matTarget.SetColor("_EmissionColor", color);
    }
}
