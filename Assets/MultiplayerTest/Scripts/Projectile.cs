using UnityEngine;

public class Projectile : MonoBehaviour
{
    float damage = 10.0f;

    [SerializeField] int playerID = -1;
    
    [SerializeField] GameObject sparksObject;
    ParticleSystem sparks;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", 3.0f);
        GameObject instantiatedObject = Instantiate(sparksObject);
        sparks = instantiatedObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerID(int ID)
    {
        playerID = ID;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health healthscript = collision.gameObject.GetComponent<Health>();

        if (healthscript != null )
        {
            healthscript.ApplyDamage(damage, playerID);
            sparks.transform.position = collision.GetContact(0).point - (transform.forward * 0.3f);
            sparks.transform.rotation = Quaternion.LookRotation(-transform.forward);
            sparks.Play();
            //Instantiate(sparks, collision.GetContact(0).point - (transform.forward * 0.3f), Quaternion.LookRotation(-transform.forward));
        }
        
        
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        gameObject.SetActive(false);
    }
}
