using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryLight : MonoBehaviour {
	private Sentry sentry;
    private Light sentryLight;

	void Start() {
		sentry = GetComponentInParent<Sentry> ();
        sentryLight = GetComponent<Light>();
	}

	void Update () {

        transform.position = sentry.transform.position;
        transform.rotation = sentry.transform.rotation;
        transform.Rotate(10f, 0, 0);
    }

    private void OnBecameInvisible()
    {
        sentryLight.enabled = false;
        this.enabled = false;
    }

    private void OnBecameVisible()
    {
        sentryLight.enabled = true;
        this.enabled = true;
    }
}
