using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientBackground : MonoBehaviour
{
    public Color topColor;
    public Color bottomColor;
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        if (mainCamera != null) {
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
            mainCamera.backgroundColor = bottomColor;
            mainCamera.backgroundColor = topColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null) {
            float t = Mathf.PingPong(Time.time, 1.0f) / 1.0f;
            mainCamera.backgroundColor = Color.Lerp(bottomColor, topColor, t);
        }
    }
}