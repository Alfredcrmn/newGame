using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{

    public UnityEvent<int, Vector2> damageableHit;

    Animator animator;


    [SerializeField]
    private int _maxHealth;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
        private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set" + value);
        }
    }

    //The velocity should not be changed while this is true but needs to be respected by other physics componenents
    //like the player controller
    public bool LockVelocity { get {
        return animator.GetBool(AnimationStrings.lockVelocity);
    }
    set
    {
        animator.SetBool(AnimationStrings.lockVelocity, value);
    }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                //Remove invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    //Returns whether the damageable took damage or not
    public bool Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
           Health -= damage;
           isInvincible = true;

           //Modify other suscribed components that the damageable was hit to handle the knockbak and such
           animator.SetTrigger(AnimationStrings.hitTrigger);
           LockVelocity = true;
           damageableHit?.Invoke(damage, knockback);

           return true;
        }

        return false;
    }
}
