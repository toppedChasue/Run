using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    [SerializeField]
    Transform targetPos;
    Transform cameraOriginPos;

    Vector3 offset;

    private void Awake()
    {
        offset = new Vector3(0, 7.5f, -5.5f);
        cameraOriginPos = this.transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = targetPos.position + offset;
    }
}
