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
    public float health = 100;
    Vector2 movement;
    //Animation Variables
    public Animator player_animator;
    //Damange Status
    public bool damaged = false;
    private float damaged_time = 0;
    public float damage_invincibility_time = 0.5f;
    //Pointing variables
    Vector2 mousePosition;
    public Camera playerCam;

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
        //Getting mouse position
        mousePosition = playerCam.ScreenToWorldPoint(Input.mousePosition);
    }
    //Updates at a FixedUpdate
    private void FixedUpdate()
    {
        //Moving the body
        Body.MovePosition(Body.position + (movement * movement_speed * Time.fixedDeltaTime));
        //Checking damage status
        if(damaged==true)
        {
            damaged_time = damaged_time + Time.fixedDeltaTime;
            if(damaged_time >= damage_invincibility_time)
            {
                damaged = false;
                damaged_time = 0;
            }
        }
        //Point towards the pointer
        Vector2 lookDirection = mousePosition - Body.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Body.rotation = lookAngle;

        //Attack
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            //attack in front of frog (raycast)
            //make a froggy tounge come out
        }
    }
    //If take damage
    public void take_damage(float damage)
    {
        //Only get damaged if you haven't recently been damaged
        if(damaged==false)
        {
            damaged = true;
            player_animator.SetTrigger("Hit");
            float new_health = health - damage;
            //Check health
            if (new_health > 0)
            {
                health = new_health;
            }
            else
            {
                health = 0;
                //You are dead
            }
        }
    }
    //Heal
    public void healing(float heal_amount)
    {
        float new_health = health + heal_amount;
        //Check health
        if(new_health > 100)
        {
            health = 100;
        }
        else
        {
            health = new_health;
        }
    }
}
