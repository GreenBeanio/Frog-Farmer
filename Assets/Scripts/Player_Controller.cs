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
    Vector2 playerMousePosition;
    public Camera playerCam;
    public float rotationSpeed = 2.5f;
    //Audio variables
    public AudioSource playerAudio;
    public AudioClip hurtSound;
    public AudioClip attackSound;
    public AudioClip deathSound;
    //attack variables
    public bool attacked = false;
    private float attack_time = 0;
    public float attack_reload_time = 0.5f;
    public float attack_range = 2;
    public GameObject attack_point;
    public float attack_damge = 10;
    //score variables
    public int snail_shells;
    public int snail_kills;
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
        playerMousePosition = playerCam.ScreenToWorldPoint(Input.mousePosition);
        //playerMousePosition = playerCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }
    //Updates at a FixedUpdate
    private void FixedUpdate()
    {
        //Attack
        if (Input.GetMouseButton(0))
        {
            if(attacked == false)
            {
                attacked = true;
                attack();
            }
        }
        //Moving the body
            //moves in fixed direcitons
        //Body.MovePosition(Body.position + (movement * movement_speed * Time.fixedDeltaTime));
            //moves where pointing
        Vector3 testdir = new Vector3(movement.x, movement.y, 0);
        transform.Translate(testdir.normalized * Time.deltaTime * movement_speed);
        
        //Checking damage status
        if (damaged==true)
        {
            damaged_time = damaged_time + Time.fixedDeltaTime;
            if(damaged_time >= damage_invincibility_time)
            {
                damaged = false;
                damaged_time = 0;
            }
        }
        //check attack
        if(attacked==true)
        {
            attack_time = attack_time + Time.fixedDeltaTime;
            if (attack_time >= attack_reload_time)
            {
                attacked = false;
                attack_time = 0;
            }
        }
        //Point towards the pointer (this shit busted if you move the camera with the player)
        Vector2 lookDirection = playerMousePosition - Body.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        Body.rotation = lookAngle;

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
                playerAudio.PlayOneShot(hurtSound, 1);
            }
            else
            {
                health = 0;
                playerAudio.PlayOneShot(deathSound, 1);
                //You are dead
            }
        }
    }
    //Heal
    public void healing(float heal_amount)
    {
        float new_health = health + heal_amount;
        
        //Check health
        if (new_health > 100)
        {
            health = 100;
        }
        else
        {
            health = new_health;
        }
    }
    //attack
    public void attack()
    {
        playerAudio.PlayOneShot(attackSound, 1);
        RaycastHit2D raycast_hit = Physics2D.Raycast(attack_point.transform.position, attack_point.transform.up, attack_range);
        if(raycast_hit.collider)
        {
            if (raycast_hit.transform.name == "Actual Shell")
            {
                healing(10);
                snail_shells += 1;
                GameObject hit_object = raycast_hit.transform.parent.gameObject;
                hit_object.GetComponent<Shell_Script>().destroy_shell();
                Debug.Log(raycast_hit.transform.name);
            }
            if (raycast_hit.transform.name == "Actual Snail")
            {
                GameObject hit_object = raycast_hit.transform.parent.gameObject;
                hit_object.GetComponent<Snail_Script>().take_damage(attack_damge);
            }
        }
    }
}
