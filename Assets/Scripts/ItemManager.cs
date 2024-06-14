using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo
{
    public Transform itemObj;
    float speed;
    public bool isNeedDestroy;
    public int typeId;

    public ItemInfo()
    {

    }
    public ItemInfo(Transform obj, int typeId)
    {
        this.itemObj = obj;
        this.typeId = typeId;
        Setup();
    }
    public void Setup()
    {
        isNeedDestroy = false;
        //meteoObj.localScale = Vector3.one * Random.Range(1, 3);
        speed = 5;
    }
    public void Move()
    {
        itemObj.Translate(Vector3.down * speed * Time.deltaTime);
        if (itemObj.position.y < -10)
        {
            isNeedDestroy = true;
        }
    }
}
public class ItemManager// : MonoBehaviour
{
    //[SerializeField] float yPosMeteoDestroy;
    public Dictionary<int, ItemInfo> itemInfoDict = new Dictionary<int, ItemInfo>();

    public List<ItemInfo> itemInfoList = new List<ItemInfo>();
    //[SerializeField] float minSize, maxSize, minSpeed, maxSpeed;
    public ItemConfig itemBeamConfig, itemBurstConfig,itemHealConfig;


    public void MyUpdate()
    {
        for (int i = 0; i < itemInfoList.Count; i++)
        {
            itemInfoList[i].Move();
        }
        for (int i = 0; i < itemInfoList.Count; i++)
        {
            if (itemInfoList[i].itemObj.position.y <-10)
            {
                
                itemInfoList[i].isNeedDestroy = true;
            }
        }
    }
    public void LateUpdate()
    {
        GameObject go;
        for (int i = itemInfoList.Count - 1; i >= 0; i--)
        {
            if (itemInfoList[i].isNeedDestroy)
            {
                go = itemInfoList[i].itemObj.gameObject;
                GameObject.Destroy(go);
                itemInfoList.RemoveAt(i);
                itemInfoDict.Remove(go.GetInstanceID());
            }
        }
    }

    public void SpawnItem()
    {
        int rand = Random.Range(1, 100) % 3;
        GameObject obj;
        if (rand == 0)
        {
            obj = GameObject.Instantiate(itemBeamConfig._itemPrefab, new Vector3(Random.Range(-5, 5), 10, 0), Quaternion.identity);
            ItemInfo info = new ItemInfo(obj.transform, 0);
            itemInfoList.Add(info);
            itemInfoDict.Add(obj.gameObject.GetInstanceID(), info);
        }
        else if(rand==1)
        {
            obj = GameObject.Instantiate(itemBurstConfig._itemPrefab,  new Vector3(Random.Range(-5, 5), 10, 0), Quaternion.identity);
            ItemInfo info = new ItemInfo(obj.transform, 1);
            itemInfoList.Add(info);
            itemInfoDict.Add(obj.gameObject.GetInstanceID(), info);
        }
        else
        {
            obj = GameObject.Instantiate(itemHealConfig._itemPrefab,  new Vector3(Random.Range(-5, 5), 10, 0), Quaternion.identity);
            ItemInfo info = new ItemInfo(obj.transform, 2);
            itemInfoList.Add(info);
            itemInfoDict.Add(obj.gameObject.GetInstanceID(), info);
        }
        
    }

    public void OnLootItem(int id)
    {
        ItemInfo itemInfo;
        if (itemInfoDict.TryGetValue(id, out itemInfo))
        {
            itemInfo.isNeedDestroy = true;
            Player.GetInstance().LootItem(itemInfo);
            //GameObject.Instantiate(AllManager.GetInstance().vfxHit, itemInfo.itemObj.position, Quaternion.identity);
        }
    }
    
}