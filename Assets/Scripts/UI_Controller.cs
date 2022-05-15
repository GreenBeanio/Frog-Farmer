using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Controller : MonoBehaviour
{
    public TMP_Text healthText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Getting health from the player
        float health = gameObject.GetComponent<Player_Controller>().health;
        healthText.text = "Health: " + health;
    }
}
