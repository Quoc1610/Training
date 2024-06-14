using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class Player : MonoBehaviour
{
    public static Player _instance { get; private set; }
    [SerializeField] private float speed;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float HP=3f;
    [SerializeField] private Slider sliderHP;
    [SerializeField] private GameObject vfxHit;
    [SerializeField] private ParticleSystem vfxShoot;
    public int bulletId;
    private bool isDead=false;
    private float lastFireTime;
    [SerializeField]Animator charAnimator;
    public static Player GetInstance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindAnyObjectByType<Player>();
        }
        return _instance;
    }

    private void Start()
    {
        sliderHP.maxValue = 3;
        bulletId = -1;
        charAnimator.SetFloat("walkForce",.5f);
        UIManager.GetInstance().uiMainHud.SetMaxValue(this, (int)HP);
    }

    private void Update()
    {
        Move();
    }
    private void LateUpdate() {
        if(isDead){
           Destroy(gameObject);
        }
    }
    private void Move()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        
        charAnimator.SetFloat("walkForce",xAxis/2+.5f);
        if (Input.GetButton("Fire1"))
        {
            FireWeapon();
        }

        transform.Translate(new Vector2(xAxis, yAxis) * speed * Time.deltaTime);
       
        
        
    }

    private void FireWeapon()
    {
        float currentTime = Time.time;

        if (currentTime - lastFireTime >= fireRate)
        {
            lastFireTime = currentTime;
            Debug.Log("V");
            AllManager.GetInstance().bulletManager.SpawnBullet(transform.position + Vector3.up * 1.2f,bulletId);
            Debug.Log("Fire");
            vfxShoot.Play( );
        }
        charAnimator.SetTrigger("isShoot");
    }
    public void OnHit()
    {
        UIManager.GetInstance().uiMainHud.SetValue(this, -1);
        if (HP <= 0)
        {
            isDead = true;
            Debug.Log("Game Over");
        }
        Instantiate(vfxHit, this.transform.position, quaternion.identity);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) return;

        if (other.CompareTag("Item"))
        {
            Debug.Log("Interact item");
            int id = other.gameObject.GetInstanceID();
            AllManager.GetInstance().itemManager.OnLootItem(id);
        }
        else
        {
            int id = gameObject.GetInstanceID();
            Debug.Log("Player Collision");
            //AllManager.GetInstance().meteorManager.ProcessCollision(id,1);
            AllManager.GetInstance().meteorManager.ProcessCollision(id);
        }
        
    }

    // public void OnCollisionExit(Collision other)
    // {
    //     int id = gameObject.GetInstanceID();
    //     Debug.Log("Player Collision");
    //     AllManager.GetInstance().meteorManager.ProcessCollision(id,2);
    // }

    public void LootItem(ItemInfo itemInfo)
    {
        Debug.Log("loot "+itemInfo.typeId);
        if (itemInfo.typeId == 2)
        {
            UIManager.GetInstance().uiMainHud.SetValue(this, 1);

        }
        bulletId = itemInfo.typeId;
    }
}