using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;



    [SerializeField] private AudioSource musicSource, effectsSource;


    public AudioClip coinVoice;

    //singleton kontrol
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {

            Destroy(gameObject);
        }
    }

    
    public void CoinEffect()
    {
        
        effectsSource.PlayOneShot(coinVoice);
    }






}
