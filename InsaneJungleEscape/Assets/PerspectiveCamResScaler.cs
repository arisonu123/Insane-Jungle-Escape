using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveCamResScaler : MonoBehaviour {
    private Camera Cam;
    private Vector3 StartScale;
    private void Start()
    {
        StartScale = transform.localScale;
        if (GvrViewer.Instance.VRModeEnabled)
        {
            if (Cam == null)
            {
                Cam = GameObject.FindGameObjectWithTag("VRCamera").GetComponent<Camera>();
            }
            // ensure calculations are done when the camera is not rotated. Otherwise the z-axis will incorrectly have some depth
            Vector3 camRotation = Cam.transform.rotation.eulerAngles;
            Cam.transform.rotation = Quaternion.Euler(Vector3.zero);
            // find corners of the cameras view frustrum at the distance of the gameobject
            float distance = Vector3.Distance(this.transform.position, Cam.transform.position);
            Vector3 viewBottomLeft = Cam.ViewportToWorldPoint(new Vector3(0, 0, distance));
            Vector3 viewTopRight = Cam.ViewportToWorldPoint(new Vector3(1, 1, distance));
            // scale the gameobject so it touches the cameras view frustrum
            Vector3 scale = viewTopRight - viewBottomLeft;
            scale.z = transform.localScale.z;
            transform.localScale = scale;
            //return the camera to it's original rotation
            Cam.transform.rotation = Quaternion.Euler(camRotation);
        }
        else
        {
            transform.localScale = StartScale;
        }
    }
}
