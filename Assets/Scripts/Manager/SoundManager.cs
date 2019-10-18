using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }
    public AudioSource source;
    public AudioClip[] AllSounds;
    /// <summary>
    /// Se le llama antes de actualizar el primer frame.
    /// </summary>
    void Start()
    {
        // Instanciamos el objeto, lo marcamos como no destructible y cargamos la primera escena
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("TitleScene");

        // La música de fondo empieza a sonar en cuanto se lanza el juego, es una configuración visual de Unity
    }
    /// <summary>
    /// Función que hace que se lance un sonido
    /// </summary>
    /// <param name="soundIndex">Índice del archivo en concreto que va a sonar, del array de sonidos que seteamos de forma visual en Unity.</param>
    public void PLaySound( int soundIndex)
    {
        AudioSource.PlayClipAtPoint(AllSounds[soundIndex], transform.position);
    }

    /// <summary>
    /// Se le llama una vez por actualización de frame.
    /// </summary>
    void Update()
    {
        
    }
}
