using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail_Script : MonoBehaviour
{
    //Audio variables
    public AudioSource snailAudio;
    public AudioClip hitSound;
    public AudioClip attackSound;
    //Health variables
    public float snail_Health = 20;
    //movement variables
    public float movement_speed = 1;
    //animator
    public Animator snailAnimator;
    //shell object
    public GameObject shell;
    //attack variables
    public float attack_damage = 5;
    //collider gameobject
    public Collider2D snailBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Frog-Player")
        {
            collision.gameObject.GetComponent<Player_Controller>().take_damage(attack_damage);
        }
    }

    public void take_damage(float damage)
    {
        snailAnimator.SetTrigger("Hurt");
        snailAudio.PlayOneShot(hitSound, 1);
        float new_health = snail_Health - damage;
        if(new_health > 0)
        {
            snail_Health = new_health;
        }
        else
        {
            die();
        }
    }
    void die()
    {
        //give player a snail point
        GameObject player = GameObject.Find("Frog-Player");
        player.GetComponent<Player_Controller>().snail_kills += 1;
        //drop a snail shell
        Instantiate(shell,transform.position,transform.rotation,null);
        //disable snail and destroy it
        snailBox.enabled = false; //temporary until I find a better way proboably. I'd rather not have the collider on this object.
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(gameObject, 1);
    }
}
