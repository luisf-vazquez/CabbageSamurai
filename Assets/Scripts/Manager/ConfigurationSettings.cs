using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationSettings : MonoBehaviour
{


    public static ConfigurationSettings instance;

    private string[] language = { "English" ,"Español"};
    private int selectedLanguage=0;
    private float dificulty=1;

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
                value = 0.7f;
                break;
        }
        return value;
    }

    public void setDificulty(float value)
    {
        dificulty = value;
    }

    public string getLanguage()
    {
        
        return language[selectedLanguage];
    }

    public void setLanguage(int index)
    {
        selectedLanguage = index;
    }
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
