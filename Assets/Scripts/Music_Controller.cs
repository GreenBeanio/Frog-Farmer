using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Controller : MonoBehaviour
{
    //Audio controller variable
    public AudioSource music_player;
    //Music variables
    public AudioClip background_music;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if(!music_player.isPlaying)
        {
            MusicMan();
        }
    }
    //When Awake
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    //Music Man
    public void MusicMan()
    {
        //Select the music clip to play
        music_player.clip = background_music;
        //Play the music
        music_player.Play();
    }
    public void changeVolume(float volume)
    {
        music_player.volume = volume;
    }
}
