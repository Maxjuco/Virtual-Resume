using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public bool isInvicible = false;
    public float invicibilityFlashDelay;
    public float invicibilityTimeAfterHit;
    public SpriteRenderer graphics;

    public healthBar HealthBar;

    public AudioClip hitSound;

    public static PlayerHealth instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Two instance of PlayerHealth are in the scene !");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
    }

    // health bar test : 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }


    /*the player take damage*/
    public void TakeDamage(int damage)
    {
        if (!isInvicible)
        {
            //play the sound of damage :
            AudioManager.instance.playClipAt(hitSound, transform.position);

            currentHealth -= damage;
            HealthBar.SetHealth(currentHealth);

            //check if the player still alive : 
            if(currentHealth <= 0)
            {
                Die();
                return;
            }

            //invicible frame : 
            isInvicible = true;
            //launch the co routine : 
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());
        }
        

    }

    public void Die()
    {
        
        //block player movement : 
        MovePlayer.instance.enabled = false;

        //play death animation :
        MovePlayer.instance.animator.SetTrigger("isDead");

        //block other physicals interactions with environment 
        MovePlayer.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        MovePlayer.instance.capsuleCollider.enabled = false;

        //reset the force apply to the player : 
        MovePlayer.instance.rb.velocity = Vector3.zero; 
        

        //to display the gameover menu : 
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        //re enable the player movement : 
        MovePlayer.instance.enabled = true;

        //come back to inital animations :
        MovePlayer.instance.animator.SetTrigger("Respawn");

        //re enable other physicals interactions with environment 
        MovePlayer.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        MovePlayer.instance.capsuleCollider.enabled = true;

        //give back health to the player : 
        currentHealth = maxHealth;
        HealthBar.SetHealth(currentHealth);
    }

    /*the player HEALLLLOOO*/
    public void HealPlayer(int health)
    {
        //check if the heal don't pass over the max health
        if(currentHealth + health > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += health;
           
        }

        HealthBar.SetHealth(currentHealth);

    }

    /*Co routine qui permet d'être exécuté tout en laissant le reste du code s'éxecuter en même temps */
    public IEnumerator InvicibilityFlash()
    {
        while (isInvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            //we put a delays of 1sec
            yield return new WaitForSeconds(invicibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invicibilityFlashDelay);

        }


    }

    public IEnumerator HandleInvicibilityDelay()
    {
        yield return new WaitForSeconds(invicibilityTimeAfterHit);
        isInvicible = false;
    }
}
