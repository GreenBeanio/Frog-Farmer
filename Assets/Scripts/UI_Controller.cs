using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    public TMP_Text healthText;
    public TMP_Text snailsText;
    public TMP_Text shellsText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Getting health from the player
        float health = gameObject.GetComponent<Player_Controller>().health;
        float snails = gameObject.GetComponent<Player_Controller>().snail_kills;
        float shells = gameObject.GetComponent<Player_Controller>().snail_shells;
        healthText.text = "Health: " + health;
        snailsText.text = "Snails Killed: " + snails;
        shellsText.text = "Shells Collected: " + shells;
    }
}
