using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class Shootbullet : MonoBehaviour
{
    [SerializeField] private float weight;
    [SerializeField] private float forceShoot;
    [SerializeField] private float shootAngle;
    [SerializeField] private float fictionForce;

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.position=new Vector3(0,5,0);
        }
        
    }

    private void FixedUpdate()
    {
        if (gameObject.transform.position.y>=0)
        {
            if (shootAngle > 0)
            {
                Debug.Log("Up abit then Down");
            }
            else
            {
                float forceFly = forceShoot - fictionForce * Time.deltaTime;
                if(forceShoot>0) forceShoot -= fictionForce * Time.deltaTime;
                gameObject.transform.Translate((Vector3.right + Vector3.down * weight * 9.8f)/5 *
                                               (forceFly));
            
            }
        }

    }
}
