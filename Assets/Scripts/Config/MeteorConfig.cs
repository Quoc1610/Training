using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/MeteorConfig")]
public class MeteorConfig : ScriptableObject
{
    public GameObject _meteorPrefab, enemyPrefab;
}