using UnityEngine;

public class Health : MonoBehaviour
{
    // Health variables
    [SerializeField]
    float currentHealth, minHealth = 0, maxHealth = 100;
    bool invulnerable = false;
    float timeStamp, vulnerableTime;

    // Object references for sounds when damaged
    [SerializeField] AudioSource hitSounds;
    [SerializeField] AudioClip cannonHit;

    // Colors for the different player objects.
    Color[] tankColors = { Color.red, Color.blue, Color.green, Color.yellow };

    // Start is called before the first frame update
    void Start()
    {
        // Set the current health
        currentHealth = maxHealth;

        // Get the audio source
        hitSounds = GetComponent<AudioSource>();
        vulnerableTime = 3.0f;
    }

    private void Update()
    {
        if (Time.time > timeStamp + vulnerableTime && invulnerable)
        {
            invulnerable = false;
            GetComponentInChildren<MeshRenderer>().material.color = tankColors[GetComponent<TankController>().GetPlayerID()];
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

                GetComponentInChildren<MeshRenderer>().material.color = Color.white;
                invulnerable = true;
                timeStamp = Time.time;
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
