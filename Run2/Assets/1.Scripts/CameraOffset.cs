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
        TransparentObject();
    }

    void TransparentObject()
    {
        Vector3 dir = Player.Instance.transform.position - transform.position;
        Vector3 direction = dir.normalized;
        Debug.DrawRay(transform.position, Player.Instance.transform.position - transform.position, Color.blue);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, dir.magnitude, 1 << LayerMask.NameToLayer("EnvironmentObject"));

       

        for (int i = 0; i < hits.Length; i++)
        {
            TransparentObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentObject>();
            Debug.Log(obj[i].name);

            for (int j = 0; j < obj.Length; j++)
            {
                obj[j]?.BecomeTransparent();
            }
        }
    }
}
