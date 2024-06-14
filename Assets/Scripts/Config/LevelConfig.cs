
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Config/LevelConfig")]
public class LevelConfig : ScriptableObject
{
   public List<levelSetting> levelSettings = new List<levelSetting>();
}

[System.Serializable]
public class levelSetting
{
   public int spawnRate;
   public int countEnemy;
   public int countMeteo;
   public int countItem;
}