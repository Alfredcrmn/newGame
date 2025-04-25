/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; // 🚨 Agregado: para cargar la escena GameOVer

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;

    Animator animator;


    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;

            if(Health < 0)
            {
                IsAlive = false;
            }
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

            //If health drops below 0, character is no longer alive
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
        }
    }

    //The velocity should not be changed while this is true but needs to be respected by other physics
    //components like the player controller
    public bool LockVelocity {
    get
    {
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
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                //Remove invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }

        // 🚨 Agregado: si el personaje está muerto, cambia a la escena GameOVer
        if (!IsAlive)
        {
            SceneManager.LoadScene("GameOVer");
        }
    }

    public float recoveryTime = 0.4f; //Do not touch this
    //Returns whether the damageable took damage or not

    // *****************************************************
    public bool Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
           Health -= damage;
           isInvincible = true;

           animator.SetBool(AnimationStrings.canMove, false); //Do not touch this
           animator.SetBool(AnimationStrings.lockVelocity, true); //Do not touch this
           Debug.Log("Hit: " + gameObject.name + " for " + damage + " damage. Health: " + Health);
           animator.SetTrigger(AnimationStrings.hitTrigger);
           damageableHit?.Invoke(damage, knockback);

           StartCoroutine(RecoverFromHit()); //Do not touch this

           return true;
        }
        
        //Unable to be hit
        return false;
    }
    // *****************************************************

    private IEnumerator RecoverFromHit() //Do not touch me
    {
        yield return new WaitForSeconds(recoveryTime); //Do not touch this

        if(IsAlive) //Do not touch this
        {
            print(
                "Recovering from hit: " + gameObject.name + " for " + Health + " health. IsAlive: " + IsAlive
            );
            animator.SetBool(AnimationStrings.canMove, true); //Do not touch this
            animator.SetBool(AnimationStrings.lockVelocity, false); //Do not touch this
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; // Para cargar la escena de GameOver

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;

    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;

            if (Health < 0)
            {
                IsAlive = false;
            }
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

            // Si la vida cae a 0 o menos, el personaje muere
            if (_health <= 0)
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

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private bool hasTriggeredGameOver = false; // Para evitar múltiples llamadas

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                // Quitar invencibilidad
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }

        // Si murió y no se ha activado el cambio de escena
        if (!IsAlive && !hasTriggeredGameOver)
        {
            hasTriggeredGameOver = true;
            StartCoroutine(DelayedGameOver()); // Espera 2 segundos antes de cargar la escena
        }
    }

    public float recoveryTime = 0.4f; // No tocar esto

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetBool(AnimationStrings.canMove, false); // No tocar esto
            animator.SetBool(AnimationStrings.lockVelocity, true); // No tocar esto
            Debug.Log("Hit: " + gameObject.name + " for " + damage + " damage. Health: " + Health);
            animator.SetTrigger(AnimationStrings.hitTrigger);
            damageableHit?.Invoke(damage, knockback);

            StartCoroutine(RecoverFromHit()); // No tocar esto

            return true;
        }

        // No se pudo hacer daño
        return false;
    }

    private IEnumerator RecoverFromHit() // No tocar
    {
        yield return new WaitForSeconds(recoveryTime);

        if (IsAlive)
        {
            print("Recovering from hit: " + gameObject.name + " for " + Health + " health. IsAlive: " + IsAlive);
            animator.SetBool(AnimationStrings.canMove, true);
            animator.SetBool(AnimationStrings.lockVelocity, false);
        }
    }

    private IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(2f); // Espera 2 segundos
        SceneManager.LoadScene("GameOVer");  // Carga la escena de Game Over
    }
}
