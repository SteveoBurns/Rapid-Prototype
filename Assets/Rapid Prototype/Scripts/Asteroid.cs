using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

namespace AsteroidHell
{
    /// <summary>
    /// Handles Asteroid Object functionality
    /// </summary>
    public class Asteroid : MonoBehaviour
    {
        public float speed = 7;
        public float rotate;
    
        // Start is called before the first frame update
        void Start()
        {
            rotate = Random.Range(310, 400);
            transform.Rotate(Vector3.back,rotate);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down*speed*Time.deltaTime);

        }

        /// <summary>
        /// If the asteroid collides with a bullet, destroy the asteroid and call the AsteroidDestroy function from the Game Manager.
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.collider.CompareTag("Bullet"))
            {
                GameManager.theManager.AsteroidDestroy(this);
                Destroy(this.gameObject);
            }
        }
    }
}