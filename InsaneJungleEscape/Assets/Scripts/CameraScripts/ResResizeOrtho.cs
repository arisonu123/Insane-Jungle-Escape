using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResResizeOrtho : MonoBehaviour {
    [SerializeField]
    private float orthographicSize = 5;
    [SerializeField]
    private float aspect = 1.33333f;
    private void Start()
    {
        Camera.main.projectionMatrix = Matrix4x4.Ortho(
                -orthographicSize * aspect, orthographicSize * aspect,
                -orthographicSize, orthographicSize,
                Camera.main.nearClipPlane, Camera.main.farClipPlane);
    }
}
