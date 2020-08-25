using System.Collections;
using UnityEngine;
using Random = System.Random;


[System.Serializable]
public struct CrosshairData
{
    public Sprite crosshairSprite;
    public int crosshairSize;
    public Color crosshairColor;
}

public enum ShootType
{
    manual,
    automatic
}

[RequireComponent(typeof(WeaponGraphics))]
[RequireComponent(typeof(BulletManager))]
public class WeaponController : MonoBehaviour
{

    [Header("Shoot Parameters")]
    public ShootType shootType;

    public int bulletDamage = 10;
    public int bulletPerShot = 1;
    public float bulletSpreadAngle = 0f;
    public float delayBetweenShots = 0.2f;
    public float horizontalRecoilForce = 0f;
    public float verticalRecoilForce = 0f;
    
    
    [Header("Ammo Parameters")]
    public int maxAmmo = 90;
    public int ammoPerClip = 30;

    [Header("Others")]
    public float reloadTime = 1f;

 
    private int curAmmo; //当前弹夹的弹药数
    private int leftAmmo; //剩余的备用弹药
    private float lastShootTime = Mathf.NegativeInfinity; //上一次的射击时间
    private bool isReloading = false;
    private WeaponGraphics weaponGraphics;
    private BulletManager bulletManager;
    private RaycastHit hit;
    private Ray ray;
    private MouseLook mouseLook;

    
    
    private void Awake()
    {
        //装上一个弹夹
        curAmmo = ammoPerClip;
        leftAmmo = maxAmmo - ammoPerClip;
        weaponGraphics = GetComponent<WeaponGraphics>();
        bulletManager = GetComponent<BulletManager>();
        mouseLook = Camera.main.GetComponent<MouseLook>();

    }

    private void Update()
    {
        if (isReloading)
        {
            return;
        }
        
        switch (shootType)
        {
            case ShootType.manual:
                if (Input.GetButtonDown("Fire1") && lastShootTime + delayBetweenShots < Time.time)
                    TryShoot();
                break;
            case ShootType.automatic:
                if(Input.GetAxis("Fire1") > 0 && lastShootTime + delayBetweenShots < Time.time)
                    TryShoot();
                break;
        }
        
        
    }

    void TryShoot()
    {
        //检测是否需要换弹
        if (curAmmo <= 0)
        {
            if(leftAmmo > 0)
                StartCoroutine(Reload());
            else
                Debug.Log("弹药全部用完了！");
            return;
        }

        float shootInterval = Time.time - lastShootTime;
        lastShootTime = Time.time;
        curAmmo--;

        //播放射击动画

        
        //播放枪口特效
        weaponGraphics.PlayMuzzleFlash();
        
        //发射子弹
        bulletManager.FireBullet();
        
        //施加后座力
        float coefficient = Mathf.Lerp(1, 0, (shootInterval - delayBetweenShots) /(5 * delayBetweenShots));
        float horizontalRecoil = UnityEngine.Random.Range(-horizontalRecoilForce, horizontalRecoilForce);
        
        StartCoroutine(mouseLook.ApplyScreenShake(horizontalRecoil * coefficient, verticalRecoilForce * coefficient));
        
        //播放射击音效
        
    }
    

    IEnumerator Reload()
    {
        Debug.Log("开始换弹");
        //播放换弹动画***
        isReloading = true;
        
        yield return new WaitForSeconds(reloadTime);
        
        //更新弹药数
        int ammoSupplied = ammoPerClip - curAmmo;
        curAmmo = ammoPerClip;
        leftAmmo -= ammoSupplied;
        isReloading = false;

    }
    
    private void OnGUI()
    {
        GUI.Box(new Rect(10,10,100,90),$"{curAmmo} / {leftAmmo}" );
    }




}