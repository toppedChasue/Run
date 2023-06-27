using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTaget : MonoBehaviour
{
    public enum clickTarget
    {
        NPC = 0,
        Enemy,
        Item,
        Portal,
        Building,
        ETC
    }

    public clickTarget _clickToTarget;
    string objectTag;

    public void Start()
    {
        objectTag = gameObject.tag;
        _clickToTarget = ((clickTarget)Enum.Parse(typeof(clickTarget), objectTag));
    }

    public void _clickTagSwitch(RaycastHit _hitTarget)
    {
        clickTarget obj = _hitTarget.transform.GetComponent<ClickTaget>()._clickToTarget;

        switch (obj)
        {
            case clickTarget.NPC:
                NPC _npc = _hitTarget.transform.GetComponent<NPC>();
                _npc.NpcClick();
                //NPC눌렀을때 해야할 함수
                break;
            case clickTarget.Enemy:
                //적일때는 공격
                Debug.Log("공격");
                break;
            case clickTarget.Item:
                _hitTarget.transform.gameObject.SetActive(false);
                Debug.Log("Item 습득");
                //아이템은 습득
                break;
            case clickTarget.Portal:
                Debug.Log("포탈 사용");
                break;
            case clickTarget.Building:
                Debug.Log("건물입니다");
                break;
            default:
                Debug.Log("아무것도아님");
                break;

        }
    }
}
