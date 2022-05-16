using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell_Script : MonoBehaviour
{

    //Audio variables
    public AudioSource shellAudio;
    public AudioClip shell_sound;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {   
    }
    //
    public void destroy_shell()
    {
        shellAudio.PlayOneShot(shell_sound, 1);
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(gameObject, 1);
    }
}
