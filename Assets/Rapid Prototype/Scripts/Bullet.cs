using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidHell
{
    /// <summary>
    /// Handles the bullet functionality
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float speed = 500f;
        private float maxLifetime = 10f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Sets the trajectory of the bullet
        /// </summary>
        /// <param name="_direction">Direction of travel for the bullet</param>
        public void Project(Vector2 _direction)
        {
            rb.AddForce(_direction * this.speed);
            
            Destroy(this.gameObject, maxLifetime);
        }

        /// <summary>
        /// When the bullet hits an object, destroy the bullet.
        /// </summary>
        private void OnCollisionEnter2D()
        {
            Destroy(this.gameObject);
        }
    }
}