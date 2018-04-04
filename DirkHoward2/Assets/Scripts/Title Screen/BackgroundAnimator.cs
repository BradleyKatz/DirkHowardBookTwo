using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BackgroundAnimator : MonoBehaviour
{
    private const float SCROLL_SPEED = 0.5f;
    private const float LERP_DURATION = 5.0f;
    private const float LERP_SMOOTHNESS = 0.02f;
    private const string BG_MATERIAL_NAME = "_MainTex";
    private const string BG_MATERIAL_TINT = "_EmisColor";

    private static BackgroundAnimator instance = null;
    private Image bgImage;
    private int nextColorIndex = 0;

    public Color[] backgroundTints;

	private void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        bgImage = GetComponent<Image>();
        StartCoroutine("LerpBackgroundColor");
	}
	
	private void Update ()
    {
        bgImage.material.SetTextureOffset(BG_MATERIAL_NAME, new Vector2(Time.time * SCROLL_SPEED, 0));
	}

    private IEnumerator LerpBackgroundColor()
    {
        if (backgroundTints.Length > 0)
        {
            float progress = 0;
            float increment = LERP_SMOOTHNESS / LERP_DURATION;

            while (true)
            {
                bgImage.material.SetColor(BG_MATERIAL_TINT, Color.Lerp(bgImage.material.GetColor(BG_MATERIAL_TINT), backgroundTints[nextColorIndex], progress));
                progress += increment;


                if (bgImage.material.GetColor(BG_MATERIAL_TINT) == backgroundTints[nextColorIndex])
                {
                    nextColorIndex = ((nextColorIndex + 1) < backgroundTints.Length) ? nextColorIndex + 1 : 0;
                    progress = 0;
                }

                yield return new WaitForSeconds(LERP_SMOOTHNESS);
            }
        }
    }
}
