using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryLight : MonoBehaviour {
	private Sentry sentry;

	void Start() {
		sentry = GetComponentInParent<Sentry> ();
	}

	void Update () {

        transform.position = sentry.transform.position;
        transform.rotation = sentry.transform.rotation;
        transform.Rotate(10f, 0, 0);
    }

    private void OnBecameInvisible()
    {
        this.enabled = false;
    }

    private void OnBecameVisible()
    {
        this.enabled = true;
    }
}
