using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false) return;
        
        int id = gameObject.GetInstanceID();
        AllManager.GetInstance().itemManager.OnLootItem(id);
    }
}
