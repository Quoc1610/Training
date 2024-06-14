using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/BurstBulletConfig")]
public class BurstConfig : BulletConfig
{

    public override void Move()
    {
        //ban 3 tia
        this._bulletPrefab.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

}