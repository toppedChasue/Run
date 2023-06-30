using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject //게임오브젝트에 붙일 필요없음
{
    public string itemName; //아이템 이름
    public Sprite itemImage; //인벤토리에 띄울 이미지
    public GameObject itemPrefab; //아이템 실체

    public enum ItemType
    {
        Equip,
        Used,
        Ingedient,
        ETC
    }

    public ItemType type;
}
