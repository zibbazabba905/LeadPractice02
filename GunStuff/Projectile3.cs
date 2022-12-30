using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile3 : MonoBehaviour
{
    public GameObject owner;
    public float LifeTime;

    private Rigidbody rb;

    private float _bulletVelocity;
    public float BulletVelocity 
    {
        get { return _bulletVelocity; }
        set { _bulletVelocity = value; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Invoke("DestroyBullet", LifeTime);
        LaunchBullet();
    }
    private void FixedUpdate()
    {
        RayCastPath();
    }

    private void LaunchBullet()
    {
        rb.AddForce(transform.forward * BulletVelocity, ForceMode.Impulse);
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    /* 
     * Collison for dynamic/speculative Continuous Detection
     * Raycasting for Trigger Detection
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out IShootable shootableObject))
        {
            shootableObject.GetShot(transform.forward * BulletVelocity, collision.GetContact(0).point);
            DestroyBullet();
        }
    }

    private void RayCastPath()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        float Distance = BulletVelocity * Time.fixedDeltaTime;
        int layermask = LayerMask.GetMask("ShotSpotter");

        if (Physics.Raycast(ray, out var hit, Distance, layermask))
        {
            hit.transform.GetComponent<ShotSpotter>().MarkShot(hit.point);
        }
    }
}
