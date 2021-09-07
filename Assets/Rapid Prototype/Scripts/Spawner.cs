using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;

using UnityEngine;

using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject extraBulletPrefab = null;
    public GameObject healthPrefab = null;
    [Header("Spawn Rate in seconds")]public float spawnRate = 2;

    [SerializeField] private bool canSpawnPowerUps = false;
    public float timeTillPickup = 5;
    public float timeTillSecondPickup = 20;
    private bool firstPickUp = true;
    private bool secondPickUp = true;
    private float newTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn),spawnRate,spawnRate);
        newTime = 0;
        if(canSpawnPowerUps)
            InvokeRepeating(nameof(HealthPickup), 40,40);

    }

    private void Update()
    {
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

    private void HealthPickup()
    {
        Debug.Log("Spawned Health Pickup");
        SpawnPowerUp(healthPrefab);
    }

    private void SpawnPowerUp(GameObject _powerUp)
    {
        GameObject powerUp = Instantiate(_powerUp, transform.position, quaternion.identity);
        Destroy(powerUp, 30);
    }

    public void Spawn()
    {
        GameObject _asteroid = null;
        float spawnPointX = Random.Range(-8, 8);
        Vector3 spawnPoint = new Vector3(transform.position.x + spawnPointX, transform.position.y, transform.position.z);
        _asteroid = Instantiate(asteroidPrefab, spawnPoint,quaternion.identity);
        GameManager.theManager.activeAsteroids.Add((_asteroid));
        Destroy(_asteroid,5);
    }

    private void RemoveAndDestroyAsteroid(GameObject _gameObject)
    {
        GameManager.theManager.activeAsteroids.Remove(_gameObject);
        Destroy(_gameObject);
    }
}
