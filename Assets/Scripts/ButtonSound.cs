using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�˽ű��������������ʱ������
public class ButtonSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip HighlightMusic;
    public AudioClip PressMusic;
    public AudioSource player;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        player.volume=0.25f;
        player.PlayOneShot(HighlightMusic);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            player.PlayOneShot(PressMusic);
        }
    }
}
