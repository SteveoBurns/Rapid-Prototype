using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Transform firePoint;
    public Transform firePoint1;
    public Transform firePoint2;
    public float fireRate = .1f;
    public bool extraBullet = false;
    public bool canShoot2 = false;
    public bool canShoot3 = false;
    [SerializeField] private int totalHealth = 100;
    public int currentHealth;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Shoot),fireRate,fireRate);
        currentHealth = totalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(extraBullet && !canShoot2)
        {
            CancelInvoke(nameof(Shoot));
            extraBullet = false;
            canShoot2 = true;
            InvokeRepeating(nameof(Shoot2),fireRate, fireRate);
        }

        if(extraBullet && canShoot2)
        {
            CancelInvoke(nameof(Shoot2));
            InvokeRepeating(nameof(Shoot),fireRate,fireRate);
            InvokeRepeating(nameof(Shoot2),fireRate, fireRate);
            extraBullet = false;
            canShoot3 = true;
        }

    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Asteroid"))
        {
            currentHealth -= 25;
            GameManager.theManager.hitSFX.Play();
            
        }
    }

    /// <summary>
    /// Controls shooting one bullet
    /// </summary>
    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab,firePoint.position, transform.rotation);
        bullet.Project(transform.up);
        GameManager.theManager.lazerSFX.Play();
        Debug.Log("shoot");
    }
    /// <summary>
    /// Controls shooting two bullets
    /// </summary>
    private void Shoot2()
    {
        Bullet bullet1 = Instantiate(bulletPrefab,firePoint1.position, transform.rotation);
        Bullet bullet2 = Instantiate(bulletPrefab,firePoint2.position, transform.rotation);
        bullet1.Project(transform.up);
        bullet2.Project(transform.up);
        GameManager.theManager.lazerSFX.Play();
        Debug.Log("shoot2");
    }

    
}
