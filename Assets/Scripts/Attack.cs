using UnityEngine;

public class Attack : MonoBehaviour
{

    public int attackDamage = 10;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //see if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            //hit the target
            damageable.Hit(attackDamage);
            Debug.Log(collision.name + "hit for " + attackDamage);
        }
    }
}
