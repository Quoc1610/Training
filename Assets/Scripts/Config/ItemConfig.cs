using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/ItemConfig")]
// public class ItemConfig : ScriptableObject
// {
//     public List<ItemData> itemDataList = new List<ItemData>();
// }
public class ItemConfig: ScriptableObject
{
    public int id;
    public GameObject _itemPrefab;
}