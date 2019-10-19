using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    
    public List<GreenCabbage> greenCabbages = new List<GreenCabbage>();
    public List<RedCabbage> redCabbages = new List<RedCabbage>();
    public List<BlueCabbage> blueCabbages = new List<BlueCabbage>();
    public List<BroccoliManager> broccolies = new List<BroccoliManager>();

    private int[] ScoreTable = new int[6];

    public GameObject cabbagePrefab;
    public GameObject redCabbagePrefab;
    public GameObject blueCabbagePrefab;
    public GameObject broccoliPrefab;

    private float lastSpawn;
    private float deltaSpawn = 0.75f;
    
    private Collider2D[] collsGreenCabbage, collsRedCabbage, collsBlueCabbage, collsBroccoli;
    private Vector3 lastMousePos;
    private int substractPoints;
    private bool isPaused;

    public Transform trail;
    public static Gamemanager game { get; set; }

    private const float minimum_cut_Speed = 400.0f;
    private int score, bestScore, lifePoints;
    private int randomCabbage;
    //UI Objects
    public Text scorePointsText;
    public Text bestPointsText;
    public Text LifePointsText;
    public Text PausedGameText;
    public Text ResumeButtonText;
    public Text RetryGOButtonText;
    public Text GOText;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    /// <summary>
    /// Buscamos objetos 'Lechuga' que no estén activos, para poder volver a lanzarlos.
    /// Si no hay ningunoi disponible, creamos uno y lo añadimos a la lista.
    /// De esta forma evitamos que se agrande innecesariamente el número de objetos en memoria.
    /// </summary>
    private GreenCabbage GetGreenCabbage()
    {
        GreenCabbage c = greenCabbages.Find(x => !x.isActive);
        if (c == null)
        {
            c = Instantiate(cabbagePrefab).GetComponent<GreenCabbage>();
            greenCabbages.Add(c);
        }

        return c;
    }
    /// <summary>
    /// Buscamos objetos 'Lechuga Roja' que no estén activos, para poder volver a lanzarlos.
    /// Si no hay ningunoi disponible, creamos uno y lo añadimos a la lista.
    /// De esta forma evitamos que se agrande innecesariamente el número de objetos en memoria.
    /// </summary>
    private RedCabbage GetRedCabbage()
    {
        RedCabbage rc = redCabbages.Find(x => !x.isActive);
        if (rc == null)
        {
            rc = Instantiate(redCabbagePrefab).GetComponent<RedCabbage>();
            redCabbages.Add(rc);
        }

        return rc;
    }
    /// <summary>
    /// Buscamos objetos 'Lechuga Azul' que no estén activos, para poder volver a lanzarlos.
    /// Si no hay ningunoi disponible, creamos uno y lo añadimos a la lista.
    /// De esta forma evitamos que se agrande innecesariamente el número de objetos en memoria.
    /// </summary>
    private BlueCabbage GetBlueCabbage()
    {
        BlueCabbage bc = blueCabbages.Find(x => !x.isActive);
        if (bc == null)
        {
            bc = Instantiate(blueCabbagePrefab).GetComponent<BlueCabbage>();
            blueCabbages.Add(bc);
        }

        return bc;
    }
    /// <summary>
    /// Buscamos objetos 'Brocoli' que no estén activos, para poder volver a lanzarlos.
    /// Si no hay ningunoi disponible, creamos uno y lo añadimos a la lista.
    /// De esta forma evitamos que se agrande innecesariamente el número de objetos en memoria.
    /// </summary>
    private BroccoliManager GetBroccoli()
    {
        BroccoliManager bro = broccolies.Find(x => !x.isActive);
        if (bro == null)
        {
            bro = Instantiate(broccoliPrefab).GetComponent<BroccoliManager>();
            broccolies.Add(bro);
        }

        return bro;
    }

    private void Awake()
    {
        game = this;
        //Recogemos las puntuaciones máximas previas e iniciamos el último valor con la puntuación actual para poder ordenarlo.
        ScoreTable[0] = PlayerPrefs.GetInt("MaxScore");
        ScoreTable[1] = PlayerPrefs.GetInt("2MaxScore");
        ScoreTable[2] = PlayerPrefs.GetInt("3MaxScore");
        ScoreTable[3] = PlayerPrefs.GetInt("4MaxScore");
        ScoreTable[4] = PlayerPrefs.GetInt("5MaxScore");
        ScoreTable[5] = 0;
        //Cambiamos los textos según el idioma actual
        deltaSpawn = ConfigurationSettings.instance.getDificulty();
        PausedGameText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "JUEGO PAUSADO" : "PAUSED GAME");
        ResumeButtonText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "CONTINUAR" : "RESUME");
        RetryGOButtonText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "REINTENTAR" : "RETRY");
        GOText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "JUEGO TERMINADO" : "GAME OVER");
    }
    /// <summary>
    /// Se usa el conocido método burbuja para ordenar las puntuaciones guardadas.
    /// Si la nueva puntuación es más alta que alguna de las guardadas, ocupa su lugar, dejando la que no interesa en la última posición.
    /// </summary>
    public void MetodoBurbuja()
    {
        int t;
        for (int a = 1; a < ScoreTable.Length; a++)
            for (int b = ScoreTable.Length - 1; b >= a; b--)
            {
                if (ScoreTable[b - 1] < ScoreTable[b])
                {
                    t = ScoreTable[b];
                    ScoreTable[b] = ScoreTable[b - 1];
                    ScoreTable[b - 1] = t;
                }
            }
    }
    /// <summary>
    /// Se setean las variables de inicio de partida.
    /// </summary>
    public void NewGame()
    {
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        deltaSpawn = 0.75f;
        Time.timeScale = 1;
        score = 0;
        lifePoints = 5;
        substractPoints = 1;
        bestScore = PlayerPrefs.GetInt("MaxScore");
        isPaused = false;
        setScorePointsText();
        setLifePointsText();
        setBestScorePointsText();
        

        foreach (GreenCabbage c in greenCabbages)
        {
            Destroy(c.gameObject);
        }
        greenCabbages.Clear();

        foreach (RedCabbage rc in redCabbages)
        {
            Destroy(rc.gameObject);
        }
        redCabbages.Clear();

        foreach (BlueCabbage bc in blueCabbages)
        {
            Destroy(bc.gameObject);
        }
        blueCabbages.Clear();

        foreach (BroccoliManager bro in broccolies)
        {
            Destroy(bro.gameObject);
        }
        broccolies.Clear();

    }

    void setLifePointsText()
    {
        LifePointsText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "Puntos de Vida: " : "Life Points: ") + lifePoints.ToString();
    }
    void setScorePointsText()
    {
        scorePointsText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "Puntos: " : "Points: ") + score.ToString();
    }
    void setBestScorePointsText()
    {
        bestPointsText.text = (ConfigurationSettings.instance.getLanguage() == "Español" ? "Mejor: " : "Best: ") + bestScore.ToString();
    }
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        collsGreenCabbage = new Collider2D[0];
        collsBlueCabbage = new Collider2D[0];
        collsRedCabbage = new Collider2D[0];
        collsBroccoli = new Collider2D[0];
        NewGame();
    }
    /// <summary>
    /// Según el tipo de lechuga cortada, aumentamos la velocidad de generación, para hacer más dinámico el juego.
    /// </summary>
    private void minusDeltaSpawn(int type)
    {
        switch (type)
        {
            case 1:
                deltaSpawn = deltaSpawn - 0.0005f;
                break;
            case 2:
                deltaSpawn = deltaSpawn - 0.00075f;
                break;
            case 3:
                deltaSpawn = deltaSpawn - 0.001f;
                break;
        }
    }

   
    void Update()
    {
        //Si el juego está pausado, no ejecutamos el update, de forma que no movemos los objetos ni dejamos al jugador interactuar con ellos.
        if (isPaused)
        {
            return;
        }
        // Si el momento actual menos la última vez que lanzamos un objeto es mayor que la velocidad de generación, podemos generar otro objeto.
        if (Time.time - lastSpawn > deltaSpawn)
        {

            GreenCabbage cabbage = GetGreenCabbage();
            float randomX = Random.Range(-1.55f, 1.55f);
            float randomY = Random.Range(1.75f, 2.75f);
            cabbage.LaunchCabbage(randomY, randomX, -randomX);
            // Usamos una variable random para lanzar de vez en cuando lechugas rojas, azules o incluso brocolis.
            randomCabbage = Random.Range(0, 100);
            if (randomCabbage > 75 && randomCabbage < 85)
            {
                RedCabbage RrCabbage = GetRedCabbage();
                randomX = Random.Range(-1.55f, 1.55f);
                randomY = Random.Range(1.75f, 2.75f);
                RrCabbage.LaunchCabbage(randomY, randomX, -randomX);

            }
            else if (randomCabbage > 90)
            {
                BlueCabbage bCabbage = GetBlueCabbage();
                randomX = Random.Range(-1.55f, 1.55f);
                randomY = Random.Range(1.75f, 2.75f);
                bCabbage.LaunchCabbage(randomY, randomX, -randomX);

            }
            else if (randomCabbage == 66 || randomCabbage == 33 || randomCabbage == 99)
            {
                BroccoliManager bro = GetBroccoli();
                randomX = Random.Range(-1.55f, 1.55f);
                randomY = Random.Range(1.75f, 2.75f);
                bro.LaunchBroccoli(randomY, randomX, -randomX);
            }
            //Seteamos él último lanzamiento al momento actual.
            lastSpawn = Time.time;
        }

        
        if (Input.GetMouseButton(0))
        {
            //Recogemos el punto donde se ha pulsado la pantalla
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Marcamos la posición delante de los demás objetos, para poder ver el trazo.
            position.z = -1;
            trail.position = position;
            //Recogemos las áreas de colisión de todos los objetos en juego
            Collider2D[] thisFrameGreenCabbages = Physics2D.OverlapPointAll(new Vector2(position.x, position.y), LayerMask.GetMask("GreenCabbage"));
            Collider2D[] thisFrameRedCabbages = Physics2D.OverlapPointAll(new Vector2(position.x, position.y), LayerMask.GetMask("RedCabbage"));
            Collider2D[] thisFrameBlueCabbages = Physics2D.OverlapPointAll(new Vector2(position.x, position.y), LayerMask.GetMask("BlueCabbage"));
            Collider2D[] thisFrameBroccolies = Physics2D.OverlapPointAll(new Vector2(position.x, position.y), LayerMask.GetMask("Broccoli"));
            //Comprobamos que haya una cierta velocidad de movimiento en pantalla, para dar la sensación de corte y no permitir sumar puntos solo tocando los objetos.
            if ((Input.mousePosition - lastMousePos).sqrMagnitude >= minimum_cut_Speed)
            {
                //Comprobamos si el trazo dibujado por el jugador encuentra algún çarea de colisión, y de esta forma, cortamos el objeto
                foreach (Collider2D c2 in thisFrameGreenCabbages)
                {
                    for (int i = 0; i < collsGreenCabbage.Length; i++)
                    {
                        if (c2 == collsGreenCabbage[i])
                        {

                            c2.GetComponent<GreenCabbage>().CutCabbage();
                            minusDeltaSpawn(1);
                        }
                    }
                }
                foreach (Collider2D c2 in thisFrameRedCabbages)
                {
                    for (int i = 0; i < collsRedCabbage.Length; i++)
                    {
                        if (c2 == collsRedCabbage[i])
                        {

                            c2.GetComponent<RedCabbage>().CutCabbage();
                            minusDeltaSpawn(2);
                        }
                    }
                }
                foreach (Collider2D c2 in thisFrameBlueCabbages)
                {
                    for (int i = 0; i < collsBlueCabbage.Length; i++)
                    {
                        if (c2 == collsBlueCabbage[i])
                        {

                            c2.GetComponent<BlueCabbage>().CutCabbage();
                            minusDeltaSpawn(3);
                        }
                    }
                }
                foreach (Collider2D c2 in thisFrameBroccolies)
                {
                    for (int i = 0; i < collsBroccoli.Length; i++)
                    {
                        if (c2 == collsBroccoli[i])
                        {

                            c2.GetComponent<BroccoliManager>().CutBroccoli();
                        }
                    }
                }
            }
            collsGreenCabbage = thisFrameGreenCabbages;

            collsRedCabbage = thisFrameRedCabbages;

            collsBlueCabbage = thisFrameBlueCabbages;

            collsBroccoli = thisFrameBroccolies;

            lastMousePos = Input.mousePosition;



        }
    }
    /// <summary>
    /// Decrementamos los puntos de vida, y en caso de no quedar ninguno, perdemos la partida.
    /// </summary>
    public void LoseLife()
    {
        lifePoints -= substractPoints;
        if (lifePoints >= 0)
        {
            setLifePointsText();
        }
        if (lifePoints <= 0)
        {
            GameOver();
        }
    }
    /// <summary>
    /// Aumentamos los puntos obtenidos, y si suman más que la mejor puntuación, lo marcamos de forma visual.
    /// </summary>
    public void incrementScore(int incrementPoints)
    {
        score += incrementPoints;
        setScorePointsText();
        if (score > bestScore)
        {
            bestScore = score;
            setBestScorePointsText();
        }
        ScoreTable[5] = score;

    }
    /// <summary>
    /// Finaliza el juego.
    /// </summary>
    public void GameOver()
    {
        //Ordena las puntuaciones y las guarda en local.
        MetodoBurbuja();
        PlayerPrefs.SetInt("MaxScore", ScoreTable[0]);
        PlayerPrefs.SetInt("2MaxScore", ScoreTable[1]);
        PlayerPrefs.SetInt("3MaxScore", ScoreTable[2]);
        PlayerPrefs.SetInt("4MaxScore", ScoreTable[3]);
        PlayerPrefs.SetInt("5MaxScore", ScoreTable[4]);
        isPaused = true;
        gameOverMenu.SetActive(true);
    }
    /// <summary>
    /// Pausa el juego.
    /// </summary>
    public void PauseGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        isPaused = pauseMenu.activeSelf;
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
