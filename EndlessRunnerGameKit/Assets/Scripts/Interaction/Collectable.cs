using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Used for coins, health, inventory items, and even ammo if you want to create a gun shooting mechanic!*/

public class Collectable : MonoBehaviour
{

    enum ItemType { InventoryItem, Coin, Health, Ammo }; //Creates an ItemType category
    [SerializeField] ItemType itemType; //Allows us to select what type of item the gameObject is in the inspector
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bounceSound;
    [SerializeField] private AudioClip[] collectSounds;
    [SerializeField] private int itemAmount;
    [SerializeField] private string itemName; //If an inventory item, what is its name?
    [SerializeField] private Sprite UIImage; //What image will be displayed if we collect an inventory item?
    public Vector3 distanceFromPlayer;
    public Vector3 speed;
    public Vector3 speedEased;
    public float easing = 20;
    public float speedMultiplier = 10;
    public bool flyToPlayer; //Fly to player after .5 seconds
    private Rigidbody2D rigidbody;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            Collect();
        }

        //Collect me if I trigger with an object tagged "Death Zone", aka an area the player can fall to certain death
        if (col.gameObject.layer == 14)
        {
            Collect();
        }
    }

    public void Update()
    {
        StartCoroutine(FlyToPlayer());
    }

    public void Collect()
    {
        if (itemType == ItemType.InventoryItem)
        {
            if (itemName != "")
            {
                GameManager.Instance.GetInventoryItem(itemName, UIImage);
            }
        }
        else if (itemType == ItemType.Coin)
        {
            NewPlayer.Instance.coins += itemAmount;
        }
        else if (itemType == ItemType.Health)
        {
            if (NewPlayer.Instance.health < NewPlayer.Instance.maxHealth)
            {
                GameManager.Instance.hud.HealthBarHurt();
                NewPlayer.Instance.health += itemAmount;
            }
        }
        else if (itemType == ItemType.Ammo)
        {
            if (NewPlayer.Instance.ammo < NewPlayer.Instance.maxAmmo)
            {
                GameManager.Instance.hud.HealthBarHurt();
                NewPlayer.Instance.ammo += itemAmount;
            }
        }

        GameManager.Instance.audioSource.PlayOneShot(collectSounds[Random.Range(0, collectSounds.Length)], Random.Range(.6f, 1f));

        NewPlayer.Instance.FlashEffect();


        //If my parent has an Ejector script, it means that my parent is actually what needs to be destroyed, along with me, once collected
        if (transform.parent.GetComponent<Ejector>() != null)
        {
            //Destroy(transform.parent.gameObject);
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            //Destroy(gameObject);
            transform.gameObject.SetActive(false);
        }

    }

    private IEnumerator FlyToPlayer()
    {
        if (flyToPlayer)
        {
            yield return new WaitForSeconds(1);
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            distanceFromPlayer.x = (NewPlayer.Instance.transform.position.x) - transform.parent.transform.position.x;
            distanceFromPlayer.y = (NewPlayer.Instance.transform.position.y + 1) - transform.parent.transform.position.y;
            speed.x = (Mathf.Abs(distanceFromPlayer.x) / distanceFromPlayer.x) * speedMultiplier;
            speed.y = (Mathf.Abs(distanceFromPlayer.y) / distanceFromPlayer.y) * speedMultiplier;
            speedEased += (speed - speedEased) * Time.deltaTime * easing;
            transform.parent.transform.position += speedEased * Time.deltaTime;
            Debug.Log("flyToPlayer!");
        }
    }
}
