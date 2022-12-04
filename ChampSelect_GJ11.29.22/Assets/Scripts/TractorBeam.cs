using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    [SerializeField] GameObject holdPosition;

    private GrabbableObject holding = null;

    private void OnTriggerEnter2D(Collider2D collision) {
        GrabbableObject go = collision.gameObject.GetComponent<GrabbableObject>();
        if (go != null && holding == null) {
            go.GrabThisObject(holdPosition.transform, this);
            holding = go;
            AkSoundEngine.PostEvent("TractorBeam_Grab", gameObject);
        }
    }

    public void ReleaseObject() {
        if (holding != null) {
            holding.ReleaseObject();
            holding = null;
        }
    }

    public void holdingObjectDestroyed() {
        holding = null;
    }
}
