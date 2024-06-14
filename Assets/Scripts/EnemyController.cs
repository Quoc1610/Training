using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") == false) return;
        int idB = other.gameObject.GetInstanceID();
        AllManager.GetInstance().bulletManager.SetDelete(idB);
        int id = gameObject.GetInstanceID();
        AllManager.GetInstance().meteorManager.OnBulletHit(id);
    }
}
