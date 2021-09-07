using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float speed = 5;
    [SerializeField] private bool extraBulletPickup = false;
    [SerializeField] private bool healthPickup = false;
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            GameManager.theManager.pickupSFX.Play();
            Destroy(this.gameObject);
            if(extraBulletPickup)
            {
                player.extraBullet = true;
            }

            if(healthPickup)
            {
                player.currentHealth += 25;
            }
        }
    }
}
