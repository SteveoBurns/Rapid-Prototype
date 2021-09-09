using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidHell.Player
{
    /// <summary>
    /// Handles player object movement.
    /// </summary>
    public class CharacterMotor : MonoBehaviour
    {
        private Vector3 position;
        private Vector2 dir;
        [SerializeField] private float moveSpeed = 2;
        [SerializeField, Tooltip("Offset position of the player object to the location of the pointer/finger")] private float posOffset = 1;

        public static bool fullspeed = false;
        public static bool halfspeed = false;
        public static bool paused = false;
    
        private Rigidbody2D rb;

        void Start()
        {
            paused = false;
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            // Handles player object position in relation to input and sets bools for game timescale.
            if(Input.GetMouseButton(0) && !paused)
            {
                position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position = new Vector3(position.x, position.y + posOffset, position.z);
                dir = (position - transform.position).normalized;
                rb.velocity = new Vector2(dir.x * moveSpeed, dir.y * moveSpeed);
                fullspeed = true;
                halfspeed = false;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                fullspeed = false;
                halfspeed = true;
                rb.velocity = Vector2.zero;
            }
            
            // Handles timescale for the game when pausing and unpausing.
            if(fullspeed && !halfspeed)
            {
                Time.timeScale = 1;
            }
            if(halfspeed && !fullspeed)
            {
                Time.timeScale = 0.2f;
            }

            if(paused)
            {
                Time.timeScale = 0;
            }
        }

    }
}