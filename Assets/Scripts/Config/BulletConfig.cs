using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Config/BulletConfig")]
public class BulletConfig : ScriptableObject
{
    public GameObject _bulletPrefab;
    public float speed;

    public virtual void Move()
    {
       
    }

}
