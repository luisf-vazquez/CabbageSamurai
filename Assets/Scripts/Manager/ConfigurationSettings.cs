using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationSettings : MonoBehaviour
{


    public static ConfigurationSettings instance;
    

    private string[] language = { "English" ,"Español"};
    private int selectedLanguage=0;
    private float dificulty;

    /// <summary>
    /// Devuelve el valor float asociado a la dificultad elegida, cuanto más alto, más tardan en lanzarse lechugas.
    /// </summary>
    public float getDificulty()
    {
        float value=1;
        switch (dificulty)
        {
            case 1:
                value = 1.0f;
                break;
                
            case 2:
                value = 0.8f;
                break;

            case 3:
                value = 0.65f;
                break;
        }
        return value;
    }
    /// <summary>
    /// Setea el valor de la dificultad elegida, guardándolo en local para mantener las preferencias del usuario.
    /// </summary>
    /// <param name="value">El valor entre 1 y 3 del slider de dificultad elegido</param>
    public void setDificulty(float value)
    {
        dificulty = value;
        PlayerPrefs.SetFloat("dificulty", dificulty);
    }
    /// <summary>
    /// Devuelve el literal del idioma elegido
    /// </summary>
    public string getLanguage()
    {
        
        return language[selectedLanguage];
    }
    /// <summary>
    /// Setea el valor del índice del idioma elegido, guardándolo en local para mantener las preferencias del usuario.
    /// </summary>
    /// <param name="index">El valor entre 0 y 1 del dropdown de idioma elegido</param>

    public void setLanguage(int index)
    {
        selectedLanguage = index;
        PlayerPrefs.SetInt("language", index);
    }
    void Awake()
    {
        //Comprobamos si la instancia existe
        if (instance == null)
            //Si no, la seteamos al valor actual
            instance = this;
        //Si la instancia existe pero no es el valor actual:
        else if (instance != this)
            //Destruimos la instancia, forzando el patrón singleton.
            Destroy(gameObject);
        //Marcamos el objeto como no destructible
        DontDestroyOnLoad(gameObject);
        
    }


    /// <summary>
    /// Se le llama antes de actualizar el primer frame.
    /// </summary>
    void Start()
    {
        dificulty = PlayerPrefs.GetFloat("dificulty");
        selectedLanguage = PlayerPrefs.GetInt("language");
    }

    /// <summary>
    /// Se le llama una vez por actualización de frame.
    /// </summary>
    void Update()
    {
        
    }
}
