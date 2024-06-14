using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MeteoInfo
{
    public Transform meteoObj;
    float speed;
    public bool isNeedDestroy;

    public MeteoInfo()
    {

    }
    public MeteoInfo(Transform obj)
    {
        this.meteoObj = obj;
        Setup();
    }
    public void Setup()
    {
        isNeedDestroy = false;
        //meteoObj.localScale = Vector3.one * Random.Range(1, 3);
        speed = Random.Range(3, 7);
    }
    public void Move()
    {
        meteoObj.Translate(Vector3.down * speed * Time.deltaTime);
        if (meteoObj.position.y < -10)
        {
            isNeedDestroy = true;
        }
    }
}
public class EnemyInfo
{
    public Transform enemyObj;
    float speed;
    public bool isNeedDestroy;
    bool isMoveLeft;

    public EnemyInfo()
    {

    }
    public EnemyInfo(Transform obj)
    {
        this.enemyObj = obj;
        Setup();
    }
    public void Setup()
    {
        isNeedDestroy = false;
        //enemyObj.localScale = Vector3.one * Random.Range(1, 3);
        speed = Random.Range(3, 7);
        isMoveLeft = Random.Range(0, 10) % 2 == 0;
    }
    public void Move()
    {

        if (enemyObj.position.y > 0)
        {
            enemyObj.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else
        {
            if (isMoveLeft)
            {
                enemyObj.Translate((Vector3.down + Vector3.left).normalized * speed * Time.deltaTime);
            }
            else
            {
                enemyObj.Translate((Vector3.down + Vector3.right).normalized * speed * Time.deltaTime);
            }
        }
        if (enemyObj.position.y < -10)
        {
            isNeedDestroy = true;
        }
    }

    public void ProcessDmg()
    {

    }
}

public class MeteorManager //: MonoBehaviour
{
    //public static MeteorManager _instance { get; private set; }
    Transform meteoObj, enemyObj;
    //[SerializeField] private Player player;
    [SerializeField] float xRangeRandom;
    public List<MeteoInfo> meteoInfoList = new List<MeteoInfo>();
    public Dictionary<int, EnemyInfo> enemyInfoDict = new Dictionary<int, EnemyInfo>();
    public List<EnemyInfo> enemyInfoList = new List<EnemyInfo>();

    // public static MeteorManager GetInstance()
    // {
    //     if (_instance == null)
    //     {
    //         _instance = GameObject.FindAnyObjectByType<MeteorManager>();
    //     }
    //     return _instance;
    // }

    public void Setup(Transform meteoObj, Transform enemyObj)
    {
        this.meteoObj = meteoObj;
        this.enemyObj = enemyObj;
        xRangeRandom = 10;
    }
    public void MyUpdate()
    {
        for (int i = 0; i < meteoInfoList.Count; i++)
        {
            meteoInfoList[i].Move();
        }

        EnemyInfo info;
        foreach (var pair in enemyInfoDict)
        {
            info = pair.Value;
            info.Move();
        }
    }

    public void LateUpdate()
    {
        for (int i = meteoInfoList.Count - 1; i >= 0; i--)
        {
            if (meteoInfoList[i].isNeedDestroy)
            {
                GameObject.Destroy(meteoInfoList[i].meteoObj.gameObject);
                meteoInfoList.RemoveAt(i);
            }
        }

        GameObject go;
        for (int i = enemyInfoList.Count - 1; i >= 0; i--)
        {
            if (enemyInfoList[i].isNeedDestroy)
            {
                go = enemyInfoList[i].enemyObj.gameObject;
                GameObject.Destroy(go);
                enemyInfoList.RemoveAt(i);
                enemyInfoDict.Remove(go.GetInstanceID());
            }
        }
    }

    public void SawpnMeteo()
    {
        Transform obj = GameObject.Instantiate(meteoObj, new Vector3(Random.Range(-xRangeRandom, xRangeRandom), 10, 0),
            Quaternion.identity);
        meteoInfoList.Add(new MeteoInfo(obj));
    }
    public void SpawnEnemy()
    {
        Transform obj = GameObject.Instantiate(enemyObj, new Vector3(Random.Range(-xRangeRandom, xRangeRandom), 10, 0), Quaternion.identity);
        EnemyInfo info = new EnemyInfo(obj);
        enemyInfoDict.Add(obj.gameObject.GetInstanceID(), info);
        enemyInfoList.Add(info);
    }

    

    public void ProcessDamageEnemy(EnemyInfo enemyInfo)
    {
        Debug.Log("process Dmg");
        Player.GetInstance().OnHit();
        
    }
    public void ProcessDamage()
    {
        Debug.Log("process Dmg");
        Player.GetInstance().OnHit();
    }
    public void ProcessCollision(int id)
    {
        EnemyInfo enemyInfo;
        
        if (enemyInfoDict.TryGetValue(id, out enemyInfo))
        {
            
            ProcessDamageEnemy(enemyInfo);
            enemyInfo.enemyObj.gameObject.GetComponent<BoxCollider>().gameObject.SetActive(false);
            enemyInfo.isNeedDestroy = true;
            Debug.Log("Add Score");
            
        }
        ProcessDamage();

    }

    public void OnBulletHit(int id)
    {
        EnemyInfo enemyInfo;
        if (enemyInfoDict.TryGetValue(id, out enemyInfo))
        {
            enemyInfo.isNeedDestroy = true;
            UIManager.GetInstance().uiMainHud.ChangeScore(this,10);
            GameObject.Instantiate(AllManager.GetInstance().vfxHit, enemyInfo.enemyObj.position, Quaternion.identity);
        }
    }
}
