using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo
{
    public Transform bulletObj;
    float speed;
    public bool isNeedDestroy;

    public BulletInfo()
    {

    }
    public BulletInfo(Transform obj)
    {
        this.bulletObj = obj;
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
        bulletObj.Translate(Vector3.up * speed * Time.deltaTime);
        
        if (bulletObj.position.y > 10)
        {
            isNeedDestroy = true;
        }
    }
}
public class BulletManager// : MonoBehaviour
{
    //[SerializeField] float yPosMeteoDestroy;
    public List<BulletInfo> bulletInfoList = new List<BulletInfo>();
    //[SerializeField] float minSize, maxSize, minSpeed, maxSpeed;
    public BulletConfig bulletConfig, beamBulletConfig, burstBulletConfig;


    public void MyUpdate()
    {
        for (int i = 0; i < bulletInfoList.Count; i++)
        {
            bulletInfoList[i].Move();
        }
        for (int i = 0; i < bulletInfoList.Count; i++)
        {
            if (bulletInfoList[i].bulletObj.position.y >= 6)
            {
                
                bulletInfoList[i].isNeedDestroy = true;
            }
        }
    }
    public void LateUpdate()
    {
        for (int i = bulletInfoList.Count - 1; i >= 0; i--)
        {
            if (bulletInfoList[i].isNeedDestroy)
            {
                GameObject.Destroy(bulletInfoList[i].bulletObj.gameObject);
                bulletInfoList.RemoveAt(i);
            }
        }
    }

    public void SetDelete(int id)
    {
        foreach (var check in bulletInfoList)
        {
            if (check.bulletObj.gameObject.GetInstanceID() == id)
            {
                check.isNeedDestroy = true;
            }
        }
    }
    public void SpawnBullet(Vector3 posSpawn, int bulletId)
    {
        Debug.Log("Spawn Bullet");
        GameObject obj;
        if (bulletId == -1)
        {
            obj = GameObject.Instantiate(bulletConfig._bulletPrefab, posSpawn, Quaternion.identity);
            bulletInfoList.Add(new BulletInfo(obj.transform));
        }
        else if (bulletId == 0)
        {
            obj = GameObject.Instantiate(burstBulletConfig._bulletPrefab, posSpawn, Quaternion.identity);
            bulletInfoList.Add(new BulletInfo(obj.transform));
            obj = GameObject.Instantiate(burstBulletConfig._bulletPrefab, posSpawn, Quaternion.Euler(new Vector3(0, 0, 20)));
            bulletInfoList.Add(new BulletInfo(obj.transform));
            obj = GameObject.Instantiate(burstBulletConfig._bulletPrefab, posSpawn, Quaternion.Euler(new Vector3(0, 0, -20)));
            bulletInfoList.Add(new BulletInfo(obj.transform));
        }
        else
        {
            obj = GameObject.Instantiate(beamBulletConfig._bulletPrefab, posSpawn, Quaternion.identity);
            bulletInfoList.Add(new BulletInfo(obj.transform));
        }

        
    }
}
