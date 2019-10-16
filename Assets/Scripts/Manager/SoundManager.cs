using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }
    public AudioSource source;
    public AudioClip[] AllSounds;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("TitleScene");
    }

    public void PLaySound( int soundIndex)
    {
        AudioSource.PlayClipAtPoint(AllSounds[soundIndex], transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
