using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllManager : MonoBehaviour
{
    public static AllManager _instance { get; private set; }

    public BulletManager bulletManager;
    public MeteorManager meteorManager;
    public ItemManager itemManager;
    [SerializeField] private BulletConfig bulletConfig;
    [SerializeField] private MeteorConfig meteorConfig;
    public ItemConfig itemBeamConfig, itemBurstConfig,itemHealConfig;
    public BulletConfig burstConfig;
    public BulletConfig beamConfig;
    public GameObject vfxHit;

    public static AllManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindAnyObjectByType<AllManager>();
        }
        return _instance;
    }

    private void Start()
    {
        bulletManager = new BulletManager();
        bulletManager.bulletConfig = bulletConfig;
        bulletManager.beamBulletConfig = beamConfig;
        bulletManager.burstBulletConfig = burstConfig;
        meteorManager = new MeteorManager();
        itemManager = new ItemManager();
        itemManager.itemBeamConfig = itemBeamConfig;
        itemManager.itemBurstConfig = itemBurstConfig;
        itemManager.itemHealConfig = itemHealConfig;
        
        meteorManager.Setup(meteorConfig._meteorPrefab.transform,meteorConfig.enemyPrefab.transform);

        StartCoroutine(Cor_SpawnMeteo());
        StartCoroutine(Cor_SpawnEnemy());
        StartCoroutine(Cor_SpawnItem());
    }

    private void Update()
    {
        bulletManager.MyUpdate();
        meteorManager.MyUpdate();
        itemManager.MyUpdate();
        // bulletManager.LateUpdate();
    }

    void LateUpdate()
    {
        bulletManager.LateUpdate();
        meteorManager.LateUpdate();
        itemManager.LateUpdate();
    }
    IEnumerator Cor_SpawnMeteo()
    {
        yield return new WaitForSeconds(1);
        meteorManager.SawpnMeteo();
        StartCoroutine(Cor_SpawnMeteo());
    }
    IEnumerator Cor_SpawnEnemy()
    {
        yield return new WaitForSeconds(1);
        meteorManager.SpawnEnemy();
        StartCoroutine(Cor_SpawnEnemy());
    }
    
    IEnumerator Cor_SpawnItem()
    {
        yield return new WaitForSeconds(1);
        itemManager.SpawnItem();
        StartCoroutine(Cor_SpawnItem());
    }
}
