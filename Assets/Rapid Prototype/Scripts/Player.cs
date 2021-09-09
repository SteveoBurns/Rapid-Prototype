using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidHell.Player
{
    /// <summary>
    /// Handles the stats and interactions of the player object.
    /// </summary>
    public class Player : MonoBehaviour
    {
        [Header("Bullet Variables")]public Bullet bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Transform firePoint1;
        [SerializeField] private Transform firePoint2;
        [SerializeField] private float fireRate = .1f;
        
        [NonSerialized] public bool extraBullet = false;
        [NonSerialized] public bool canShoot2 = false;
        
        [Header("Player Health")]
        [SerializeField] private int totalHealth = 100;
        [NonSerialized] public int currentHealth;
    
    
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating(nameof(Shoot),fireRate,fireRate);
            currentHealth = totalHealth;
        }

        // Update is called once per frame
        void Update()
        {
            // Handles how many bullets can shoot after getting the pickups.
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
                
            }

        }

        /// <summary>
        /// Handles collisions with the asteroids. Removes health and plays the hit sound.
        /// </summary>
        /// <param name="other">The collider touching this object.</param>
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
}