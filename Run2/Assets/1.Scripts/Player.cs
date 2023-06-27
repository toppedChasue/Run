using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance = null;

    public static Player Instance 
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance; 
        } 
    }

    //플레이어가 가지고 있을것들


    private void Awake()
    {
        if (instance == null) { Player.instance = this; DontDestroyOnLoad(this.gameObject); }
        else { Destroy(this.gameObject); }
    }
}
