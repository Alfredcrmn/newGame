using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            myRigidBody.linearVelocity = Vector2.up * 7;
        }
    }
}
