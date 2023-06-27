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

    //�÷��̾ ������ �����͵�


    private void Awake()
    {
        if (instance == null) { Player.instance = this; DontDestroyOnLoad(this.gameObject); }
        else { Destroy(this.gameObject); }
    }
}
