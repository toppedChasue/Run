using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    /*
     * NPC���� �ʿ��Ѱ�
     * �ʼ���� - �̸�, �ִϸ��̼�, ����, �ؽ�Ʈ(���)
     * ���� - �ǸŸ� �ϴ���
              ������ �ϴ���
              ����Ʈ�� �ִ���
     */
    protected string npc_name;
    protected int npc_Id; //������ ���̵�
    protected NPCJob npc_job;
    protected GameObject npc_Prefab;
    //private Animator npc_Anim;

    Text npc_Text;

    public enum NPCJob
    {
        None=0,
        Equipment, //������
        Used, //�Һ��ǰ����
        Enchant, //�����ο�����
        repair //��������
    }

    private void Awake()
    {
        //npc_Anim = GetComponent<Animator>();
    }

    public virtual void NpcClick()
    {
        //npc���� �´� uiȣ��
        Debug.Log("npcȣ��");
    }

}
