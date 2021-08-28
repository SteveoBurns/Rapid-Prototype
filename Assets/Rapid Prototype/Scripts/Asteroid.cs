using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Bullet"))
        {
            GameManager.theManager.AsteroidDestroy(this);
            Destroy(this.gameObject);
        }
    }
}
