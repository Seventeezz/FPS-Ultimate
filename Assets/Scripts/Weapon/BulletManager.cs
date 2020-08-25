using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 可用对象池优化
/// </summary>
public class BulletManager : MonoBehaviour
{
    
    [Header("References")]
    public TrailRenderer bulletTrailPrefab;
    public ParticleSystem hitEffect;
    public Transform muzzle;


    [Header("Bullet Info")]
    public float bulletSpeed = 1000f;
    public float bulletDrop = 0f;
    public float maxLifeTime = 2f;

    private RaycastHit hit;
    private Ray ray;
    private Camera cam;
    private readonly List<Bullet> bullets = new List<Bullet>();


    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        //更新所有子弹的状态
        if (bullets.Count > 0)
        {
            UpdateBullets(Time.deltaTime);
        }
        
    }

    public void FireBullet()
    {
        // EditorApplication.isPaused = true;
        Vector3 origin = muzzle.position;
        Vector3 destination = GetCrosshairTargetPosition();
        Vector3 velocity = (destination - origin).normalized * bulletSpeed;
        var bullet = CreateBullet(origin, velocity);
        bullets.Add(bullet);

    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initPosition = position;
        bullet.initVelocity = velocity;
        bullet.time = 0f;
        bullet.tracer = Instantiate(bulletTrailPrefab, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }

    public void UpdateBullets(float deltaTime)
    {
        //更新所有子弹的位置
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
        //将超过生命周期的子弹清除
        bullets.RemoveAll(bullet => bullet.time >= maxLifeTime);
    }
    
    
    Vector3 GetPosition(Bullet bullet)
    {
        // p + v*t + 0.5*g*t*t
        Vector3 gravity = Vector3.down * bulletDrop;
        return (bullet.initPosition) + (bullet.initVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }

    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        ray.origin = start;
        ray.direction = direction;
        
        if (Physics.Raycast(ray, out hit, direction.magnitude))
        {
            //Play Hit Effect
            ShowHitEffect(hit);
            //更新trail
            bullet.tracer.transform.position = hit.point;
            //击中了，则子弹生命周期结束
            bullet.time = maxLifeTime;
        }
        else
        {
            bullet.tracer.transform.position = end;
        }
    }
    
    
    Vector3 GetCrosshairTargetPosition()
    {
        ray =  cam.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit) ? hit.point : ray.GetPoint(100f);
    }
    
    
    void ShowHitEffect(RaycastHit hit)
    {
        var transform1 = hitEffect.transform;
        transform1.position = hit.point + hit.normal * 0.00001f;
        transform1.forward = hit.normal;
        hitEffect.Play();
    }
    
    
    
}

class Bullet
{
    public Vector3 initPosition;
    public Vector3 initVelocity;
    public float time;
    public TrailRenderer tracer;
}