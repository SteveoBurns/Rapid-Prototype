using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager theManager;
    [Header("Player Object")]
    public Player player;
    [SerializeField] private GameObject explosion;
    private SpriteRenderer rend;

    [Header("Score Variables")]
    public int score = 0;
    public int scorePerAsteroid = 25;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Slider healthSlider;
    
    [Header("Pop Up Elements")]
    [SerializeField] public GameObject PopUp;
    [SerializeField] private TMP_Text popupText;
    [SerializeField, TextArea] private string diedText;
    [SerializeField, TextArea] private string winText;

    [Header("Audio Elements")] 
    public AudioSource explosionSFX;
    public AudioSource pickupSFX;
    public AudioSource deathSFX;
    public AudioSource lazerSFX;
    public AudioSource hitSFX;

    private bool isPlayerDead = false;


    public List<GameObject> activeAsteroids = new List<GameObject>();

    
    private void Awake()
    {
        if(theManager != null)
            Destroy(this);
        else
            theManager = this;

        rend = player.GetComponentInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// Sets the pop-up active and sets the text to the passed string and pauses time.
    /// </summary>
    /// <param name="_text">The string that will appear in the text field</param>
    public void PopUpPanel(string _text)
    {
        PopUp.SetActive(true);
        popupText.text = _text;
        CharacterMotor.paused = true;
        
        
    }

    /// <summary>
    /// Destroys all active asteroids
    /// </summary>
    private void DestroyActiveAsteroids()
    {
        foreach(GameObject _asteroid in activeAsteroids)
        {
            Destroy(_asteroid);
        }
    }

    

    /// <summary>
    /// Called from the asteroid class when it is destroyed by a bullet
    /// </summary>
    /// <param name="_asteroid">The asteroid that was hit</param>
    public void AsteroidDestroy(Asteroid _asteroid)
    {
        score += scorePerAsteroid;
        Explosion(_asteroid.transform);
        
    }

    /// <summary>
    /// Sets the explosion to the passed position and automatically hides it after 0.3s
    /// </summary>
    /// <param name="_transform">Passed transform for explosion location</param>
    private void Explosion(Transform _transform)
    {
        explosion.transform.position = _transform.position;
        explosion.SetActive(true);
        //Play Explosion SFX
        explosionSFX.Play();
        Invoke(nameof(HideExplosion),0.3f);
    }
    /// <summary>
    /// Called from Explosion method. Used to hide the explosion after the animation.
    /// </summary>
    private void HideExplosion()
    {
        explosion.SetActive(false);
    }
    
    /// <summary>
    /// Called when the player dies
    /// </summary>
    public void PlayerDied()
    {
        // Play Died SFX
        if(!isPlayerDead)
        {
            deathSFX.Play();
            isPlayerDead = true;
            Explosion(player.transform);
            rend.enabled = false;
        }
        PopUpPanel(diedText);
        Debug.Log("Player Died");
    }

    
    /// <summary>
    /// Runs when the win condition is true
    /// </summary>
    private void PlayerWin()
    {
        DestroyActiveAsteroids();
        PopUpPanel(winText);
    }

    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = player.currentHealth;
        scoreText.text = $"Score: {score.ToString()}";
        if(player.currentHealth<= 0)
        {
            PlayerDied();
        }

        if(score >= 5000)
        {
            PlayerWin();
        }
    }
}
