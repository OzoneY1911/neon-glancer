using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SineFadeMaterial : MonoBehaviour
{
    [SerializeField] Material originalMaterial;
    [SerializeField] float fadeSpeed = 1f;

    Color tempColor;

    void Awake()
    {
        tempColor = originalMaterial.color;
    }

    void Update()
    {
        SineFade();
    }

    void SineFade()
    {
        tempColor.a = 0.5f * Mathf.Sin(fadeSpeed * Time.time) + 0.5f;
        originalMaterial.color = tempColor;
    }
}
