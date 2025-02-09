using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows object to break after depleting its "health".

[RequireComponent(typeof(RecoveryCounter))]

public class Breakable : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Sprite brokenSprite; //If destroyAfterDeath is false, a broken sprite will appear instead
    [SerializeField] public GameObject deathParticles;
    [SerializeField] private bool destroyAfterDeath = true; //If false, a broken sprite will appear instead of complete destruction
    public int health;
    [SerializeField] private Instantiator instantiator;
    [SerializeField] private AudioClip hitSound;
    private bool recovered;
    [SerializeField] private RecoveryCounter recoveryCounter;
    [SerializeField] private bool requireDownAttack;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private bool launchPlayerWhenBroken = false;
    // Use this for initialization
    void Start()
    {
        recoveryCounter = GetComponent<RecoveryCounter>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void GetHurt(int hitPower)
    {
        //If breakable object health is above zero, it's not recovering from a recent hit, get hit!
        if (health > 0 && !recoveryCounter.recovering)
        {
            if (!requireDownAttack || (requireDownAttack && NewPlayer.Instance.velocity.y < -2))
            {
                
                if (hitSound != null)
                {
                    GameManager.Instance.audioSource.PlayOneShot(hitSound);
                }
               
                //Ensure the player can't hit the box multiple times in one hit
                recoveryCounter.counter = 0;

                StartCoroutine(NewPlayer.Instance.FreezeFrameEffect());

                health -= 1;
                animator.SetTrigger("hit");

                if (health <= 0)
                {
                    Die();
                }
                
            }
        }
    }

    public void Die()
    {
        if (launchPlayerWhenBroken)
        {
            NewPlayer.Instance.velocity.y = 10;
        }

        //Ensure timeScale is forced to 1 after breaking
        Time.timeScale = 1;

        //Activate deathParticles & unparent from this so they aren't destroyed!
        deathParticles.SetActive(true);
        deathParticles.transform.parent = null;

        if (instantiator != null)
        {
            instantiator.InstantiateObjects();
        }

        //Destroy me, or set my sprite to the brokenSprite
        if (destroyAfterDeath)
        {
            //Destroy(gameObject);
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = brokenSprite;
        }

    }
}
