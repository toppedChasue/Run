using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject //���ӿ�����Ʈ�� ���� �ʿ����
{
    public string itemName; //������ �̸�
    public Sprite itemImage; //�κ��丮�� ��� �̹���
    public GameObject itemPrefab; //������ ��ü

    public enum ItemType
    {
        Equip,
        Used,
        Ingedient,
        ETC
    }

    public ItemType type;
}
