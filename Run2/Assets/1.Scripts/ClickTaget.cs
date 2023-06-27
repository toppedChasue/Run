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
                //NPC�������� �ؾ��� �Լ�
                break;
            case clickTarget.Enemy:
                //���϶��� ����
                Debug.Log("����");
                break;
            case clickTarget.Item:
                _hitTarget.transform.gameObject.SetActive(false);
                Debug.Log("Item ����");
                //�������� ����
                break;
            case clickTarget.Portal:
                Debug.Log("��Ż ���");
                break;
            case clickTarget.Building:
                Debug.Log("�ǹ��Դϴ�");
                break;
            default:
                Debug.Log("�ƹ��͵��ƴ�");
                break;

        }
    }
}
