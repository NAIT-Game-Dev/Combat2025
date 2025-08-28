using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float damage = 10.0f;

    [SerializeField] int playerID = -1;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", 3.0f);
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
        }
        
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
