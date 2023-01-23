using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�˽ű���������������
public class BackMusicController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip mainpage;
    public AudioClip gamepage1;
    public AudioSource player;
    void Start()
    {
        player= GetComponent<AudioSource>();
        player.clip = mainpage;
        player.loop = true;
        player.volume = 0.25f;
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
