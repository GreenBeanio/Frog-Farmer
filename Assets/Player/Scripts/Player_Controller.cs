using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    //Public Components
    public Rigidbody2D Body;
    //Movement Variables
    public float movement_speed = 2.0f;
    //Health Variables
    public int health = 100;
    Vector2 movement;
    //Animation Variables
    public Animator player_animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Getting Input Variales
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //Setting animations
        player_animator.SetFloat("Horizontal", movement.x);
        player_animator.SetFloat("Vertical", movement.y);
        player_animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    //Updates at a FixedUpdate
    private void FixedUpdate()
    {
        //Moving the body
        Body.MovePosition(Body.position + (movement * movement_speed * Time.fixedDeltaTime));
    }
}
