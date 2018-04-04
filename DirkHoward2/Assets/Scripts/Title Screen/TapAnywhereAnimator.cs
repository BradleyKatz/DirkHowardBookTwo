using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAnywhereAnimator : MonoBehaviour
{
    public static TapAnywhereAnimator instance = null;

    private const int VISIBLE = 1;
    private const int INVISIBLE = 0;

    [Range(0,1)]
    public float delay;

    private CanvasGroup canvasGroup;
    private bool hiding = false;

	private void Start ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine("AnimateText");
	}
	
	private IEnumerator AnimateText()
    {
        while (true)
        {
            if (!hiding)
            {
                canvasGroup.alpha = VISIBLE;
                hiding = !hiding;
            }
            else
            {
                canvasGroup.alpha = INVISIBLE;
                hiding = !hiding;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}
