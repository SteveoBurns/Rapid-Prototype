using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;

using UnityEngine;

using Random = UnityEngine.Random;

namespace AsteroidHell
{
    /// <summary>
    /// Handles functionality of spawning asteroids and powerups.
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject asteroidPrefab;
        public GameObject extraBulletPrefab = null;
        public GameObject healthPrefab = null;
        
        [Header("Asteroid Spawn Rate in seconds")]public float spawnRate = 2;

        [SerializeField, Tooltip("Can this spawner spawn power ups?")] private bool canSpawnPowerUps = false;
        
        private float timeTillPickup = 5;
        private float timeTillSecondPickup = 20;
        private bool firstPickUp = true;
        private bool secondPickUp = true;
        private float newTime = 0;
    
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating(nameof(Spawn),spawnRate,spawnRate);
            newTime = 0;
            if(canSpawnPowerUps)
                InvokeRepeating(nameof(HealthPickup), 15,20);

        }

        private void Update()
        {
            // Handles the timing of spawning the extra bullet powerups.
            newTime += Time.deltaTime;
            if(newTime > timeTillPickup && firstPickUp && canSpawnPowerUps)
            {
                if(extraBulletPrefab != null)
                {
                    SpawnPowerUp(extraBulletPrefab); 
                    firstPickUp = false;
                }
            }
            if(newTime > timeTillSecondPickup && secondPickUp && canSpawnPowerUps)
            {
                if(extraBulletPrefab != null)
                {
                    SpawnPowerUp(extraBulletPrefab); 
                    secondPickUp = false;
                }
            }
        }

        /// <summary>
        /// Handles spawning the health pickups. Gets invoked repeating in start.
        /// </summary>
        private void HealthPickup()
        {
            Debug.Log("Spawned Health Pickup");
            SpawnPowerUp(healthPrefab);
        }

        /// <summary>
        /// Used to spawn a powerup from a passed prefab.
        /// </summary>
        /// <param name="_powerUp">The Prefab to spawn</param>
        private void SpawnPowerUp(GameObject _powerUp)
        {
            GameObject powerUp = Instantiate(_powerUp, transform.position, quaternion.identity);
            Destroy(powerUp, 30);
        }

        /// <summary>
        /// Spawns an asteroid and destroys it afterwards.
        /// </summary>
        public void Spawn()
        {
            GameObject _asteroid = null;
            float spawnPointX = Random.Range(-8, 8);
            Vector3 spawnPoint = new Vector3(transform.position.x + spawnPointX, transform.position.y, transform.position.z);
            _asteroid = Instantiate(asteroidPrefab, spawnPoint,quaternion.identity);
            GameManager.theManager.activeAsteroids.Add((_asteroid));
            Destroy(_asteroid,5);
        }

        }
}