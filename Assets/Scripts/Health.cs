using UnityEngine;

public class Health : MonoBehaviour
{
    // Health variables
    [SerializeField]
    float currentHealth, minHealth = 0, maxHealth = 100;
    bool invulnerable = false;
    float invulnerableStamp, vulnerableTime, flashStamp, flashTime;

    // Object references for sounds when damaged
    [SerializeField] AudioSource hitSounds;
    [SerializeField] AudioClip cannonHit;

    Material myMat;

    // Start is called before the first frame update
    void Start()
    {
        // Set the current health
        currentHealth = maxHealth;

        // Get the audio source
        hitSounds = GetComponent<AudioSource>();
        vulnerableTime = 4.0f;
        flashTime = 0.2f;

        myMat = GetComponentInChildren<MeshRenderer>().material;
    }

    private void Update()
    {
        if (Time.time > invulnerableStamp + vulnerableTime && invulnerable)
        {
            invulnerable = false;

            myMat.color = new Color(myMat.color.r, myMat.color.g, myMat.color.b, 1f);
        }
        else if (invulnerable && Time.time > flashStamp + flashTime)
        {
            flashStamp = Time.time;
            if (myMat.color.a == 1)
            {
                myMat.color = new Color(myMat.color.r, myMat.color.g, myMat.color.b, 0.3f);
            }
            else
            {
                myMat.color = new Color(myMat.color.r, myMat.color.g, myMat.color.b, 1f);
            }
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    { 
        return maxHealth; 
    }

    public void ApplyDamage(float damage, int ID)
    {
        if (!invulnerable)
        {
            // Reduce the health by the damage
            currentHealth -= damage;
            hitSounds.PlayOneShot(cannonHit);

            // If the current health goes below the minimum health then set it to minimum
            if (currentHealth < minHealth)
            {
                currentHealth = minHealth;
            }

            // When the current health is at the minimum then add to the attacking player's score and respawn the hit player.
            if (currentHealth == minHealth)
            {
                MyEvents.AddScore.Invoke(ID);
                MyEvents.RespawnPlayer.Invoke(GetComponent<TankController>().GetPlayerID());
                
                myMat.color = new Color(myMat.color.r, myMat.color.g, myMat.color.b, 0.3f);

                invulnerable = true;
                invulnerableStamp = Time.time;
            }
        }        
    }

    public void HealDamage(float damage)
    {
        // Increase the health by the damage amount
        currentHealth += damage;

        // If the current health is more than the maximum health then set it to the maximum health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
