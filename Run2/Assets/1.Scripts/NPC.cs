using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    /*
     * NPC한테 필요한것
     * 필수요소 - 이름, 애니메이션, 음성, 텍스트(대사)
     * 직업 - 판매를 하는지
              수리를 하는지
              퀘스트를 주는지
     */
    protected string npc_name;
    protected int npc_Id; //구별할 아이디
    protected NPCJob npc_job;
    protected GameObject npc_Prefab;
    //private Animator npc_Anim;

    Text npc_Text;

    public enum NPCJob
    {
        None=0,
        Equipment, //장비상인
        Used, //소비용품상인
        Enchant, //마법부여상인
        repair //수리상인
    }

    private void Awake()
    {
        //npc_Anim = GetComponent<Animator>();
    }

    public virtual void NpcClick()
    {
        //npc별로 맞는 ui호출
        Debug.Log("npc호출");
    }

}
