using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllManager : MonoBehaviour
{
    public static AllManager _instance { get; private set; }
    public LevelConfig levelConfig;
    public BulletManager bulletManager;
    public MeteorManager meteorManager;
    public ItemManager itemManager;
    [SerializeField] private BulletConfig bulletConfig;
    [SerializeField] private MeteorConfig meteorConfig;
    public ItemConfig itemBeamConfig, itemBurstConfig,itemHealConfig;
    public BulletConfig burstConfig;
    public BulletConfig beamConfig;
    public GameObject vfxHit;
    public int levelCount;
    private int countEnemy=0, countMeteo=0, countItem=0;
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
        levelCount = 0;
        

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
        countMeteo++;
        if (countMeteo == levelConfig.levelSettings[levelCount].countMeteo) yield break;
        yield return new WaitForSeconds(levelConfig.levelSettings[levelCount].spawnRate);
        meteorManager.SawpnMeteo();
        
        StartCoroutine(Cor_SpawnMeteo());
    }
    IEnumerator Cor_SpawnEnemy()
    {
        countEnemy++;
        if (countEnemy == levelConfig.levelSettings[levelCount].countEnemy) yield break;
        yield return new WaitForSeconds(levelConfig.levelSettings[levelCount].spawnRate);
        meteorManager.SpawnEnemy();
        StartCoroutine(Cor_SpawnEnemy());
    }
    
    IEnumerator Cor_SpawnItem()
    {
        countItem++;
        if (countItem == levelConfig.levelSettings[levelCount].countItem) yield break;
        yield return new WaitForSeconds(levelConfig.levelSettings[levelCount].spawnRate);
        itemManager.SpawnItem();
        StartCoroutine(Cor_SpawnItem());
    }
}
