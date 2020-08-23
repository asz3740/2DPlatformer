using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private AudioSource myAudio;

    public AudioClip sndPlayerJump;
    public AudioClip sndPlayerAttack;
    public AudioClip sndPlayerThrow;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void PlayerJumpSound()
    {
        myAudio.PlayOneShot(sndPlayerJump);
    }

    public void PlayerAttackSound()
    {
        myAudio.PlayOneShot(sndPlayerAttack);
    }

    public void PlayerThrowSound()
    {
        Invoke("Play", 0.2f);      
    }

    private void Play()
    {
        myAudio.PlayOneShot(sndPlayerThrow);
    }



}
