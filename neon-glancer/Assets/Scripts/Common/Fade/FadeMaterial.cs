using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMaterial
{
    public bool fadingOut;
    public bool fadingIn;

    public IEnumerator FadeOut(Material originalMaterial)
    {
        if (originalMaterial.color.a < 1f)
        {
            fadingOut = true;

            Color tempColor = originalMaterial.color;

            if (tempColor.a == 0f)
            {
                yield return new WaitForSeconds(0.5f);
            }

            tempColor.a += 1f * Time.deltaTime;
            originalMaterial.color = tempColor;

            fadingOut = false;

            yield return null;
        }
    }

    public IEnumerator FadeIn(Material originalMaterial)
    {
        if (originalMaterial.color.a > 0f)
        {
            fadingIn = true;

            Color tempColor = originalMaterial.color;

            tempColor.a -= 1f * Time.deltaTime;
            originalMaterial.color = tempColor;

            fadingIn = false;

            yield return null;
        }
    }
}
