using System.Numerics;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public int attackDamage = 10;
    public UnityEngine.Vector2 knockback = UnityEngine.Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //See if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable != null)
        {

            UnityEngine.Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new UnityEngine.Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(attackDamage, knockback);

            if(gotHit)
            {
                Debug.Log(collision.name + "hit for" + attackDamage);
            }
        }
    }
}
