using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject Player;

    Vector3 cameraPos = new Vector3(0, 4.9f, -2.8f);



    // Update is called once per frame
    void Update()
    {//카메라 따라다님
        gameObject.transform.position = Player.transform.position + cameraPos;
    }


}
