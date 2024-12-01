using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Weapon : MonoBehaviour
{
[Header("Weapon Stats")]
[SerializeField] private float shootIntervalInSeconds = 3f;


[Header("Bullets")]
public Bullet bullet;
[SerializeField] private Transform bulletSpawnPoint;


[Header("Bullet Pool")]
private IObjectPool<Bullet> objectPool;


private readonly bool collectionCheck = false;
private readonly int defaultCapacity = 30;
private readonly int maxSize = 100;
private float timer;
public Transform parentTransform;
private void Awake()
    {
        // Initialize the object pool for bullets
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBullet.objectPool = objectPool;  // Set the pool reference for the bullet
        return newBullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0;
        }
    }

    public void Shoot()
    {
        Bullet newBullet = objectPool.Get();
        newBullet.transform.position = bulletSpawnPoint.position;
        newBullet.transform.rotation = bulletSpawnPoint.rotation;
    }

}

