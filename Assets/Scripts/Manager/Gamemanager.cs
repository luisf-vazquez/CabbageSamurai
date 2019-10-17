using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Cabbage> cabbages = new List<Cabbage>();
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
    public Transform trail;
    private Collider2D[] collsCabbages,collsRedCabbage,collsBlueCabbage,collsBroccoli;
    private Vector3 lastMousePos;
    private int substractPoints;
    //private int incrementPoints;
    private bool isPaused;
   
    public static Gamemanager game { get; set; }

    private const float minimum_cut_Speed = 400.0f;
    private int score, bestScore, lifePoints;
    private int randomCabbage;

    public Text scorePointsText;
    public Text bestPointsText;
    public Text LifePointsText;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    private Cabbage GetCabbage()
    {
        Cabbage c = cabbages.Find(x => !x.isActive);
        if (c == null)
        {
            c = Instantiate(cabbagePrefab).GetComponent<Cabbage>();
            cabbages.Add(c);
        }

        return c;
    }
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
        ScoreTable[0] = PlayerPrefs.GetInt("MaxScore");
        ScoreTable[1] = PlayerPrefs.GetInt("2MaxScore");
        ScoreTable[2] = PlayerPrefs.GetInt("3MaxScore");
        ScoreTable[3] = PlayerPrefs.GetInt("4MaxScore");
        ScoreTable[4] = PlayerPrefs.GetInt("5MaxScore");
        ScoreTable[5] = 0;
        deltaSpawn = ConfigurationSettings.instance.getDificulty();
    }

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

        foreach(Cabbage c in cabbages)
        {
            Destroy(c.gameObject);
        }
        cabbages.Clear();

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
        LifePointsText.text = "Life Points: " + lifePoints.ToString();
    }
    void setScorePointsText()
    {
        scorePointsText.text = "Points: " + score.ToString();
    }
    void setBestScorePointsText()
    {
        bestPointsText.text = "Best: " + bestScore.ToString();
    }
    void Start()
    {
        collsCabbages = new Collider2D[0];
        collsBlueCabbage = new Collider2D[0];
        collsRedCabbage = new Collider2D[0];
        collsBroccoli = new Collider2D[0];
        NewGame();
    }

    private void minusDeltaSpawn( int type)
    {
        switch (type){
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

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            return;
        }
        
        if (Time.time - lastSpawn > deltaSpawn)
        {

            Cabbage cabbage = GetCabbage();
            float randomX = Random.Range(-1.55f, 1.55f);
            float randomY = Random.Range(1.75f, 2.75f);
            cabbage.LaunchCabbage(randomY, randomX, -randomX);
            randomCabbage = Random.Range(0, 100);
            if (randomCabbage > 75 && randomCabbage < 85)
            {
                RedCabbage RrCabbage = GetRedCabbage();
                 randomX = Random.Range(-1.55f, 1.55f);
                 randomY = Random.Range(1.75f, 2.75f);
                RrCabbage.LaunchRedCabbage(randomY, randomX, -randomX);

            }
            else if (randomCabbage > 90)
            {
                BlueCabbage bCabbage = GetBlueCabbage();
                 randomX = Random.Range(-1.55f, 1.55f);
                 randomY = Random.Range(1.75f, 2.75f);
                bCabbage.LaunchBlueCabbage(randomY, randomX, -randomX);

            }
            else if(randomCabbage == 66 || randomCabbage==33 || randomCabbage== 99)
            {
                BroccoliManager bro = GetBroccoli();
                randomX = Random.Range(-1.55f, 1.55f);
                randomY = Random.Range(1.75f, 2.75f);
                bro.LaunchBroccoli(randomY, randomX, -randomX);
            }
            lastSpawn = Time.time;
        }
        

        if (Input.GetMouseButton(0))
        {

            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = -1;
            trail.position = position;

            Collider2D[] thisFrameCabbages = Physics2D.OverlapPointAll(new Vector2(position.x, position.y), LayerMask.GetMask("Cabbage"));
            Collider2D[] thisFrameRedCabbages = Physics2D.OverlapPointAll(new Vector2(position.x, position.y), LayerMask.GetMask("RedCabbage"));
            Collider2D[] thisFrameBlueCabbages = Physics2D.OverlapPointAll(new Vector2(position.x, position.y), LayerMask.GetMask("BlueCabbage"));
            Collider2D[] thisFrameBroccolies = Physics2D.OverlapPointAll(new Vector2(position.x, position.y), LayerMask.GetMask("Broccoli"));

            if ((Input.mousePosition - lastMousePos).sqrMagnitude >= minimum_cut_Speed)
            {
                foreach (Collider2D c2 in thisFrameCabbages)
                {
                    for (int i = 0; i < collsCabbages.Length; i++)
                    {
                        if (c2 == collsCabbages[i])
                        {
                            Debug.Log("Sliced " + c2.name);
                            c2.GetComponent<Cabbage>().CutCabbage();
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
                            Debug.Log("Sliced " + c2.name);
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
                            Debug.Log("Sliced " + c2.name);
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
                            Debug.Log("Sliced " + c2.name);
                            c2.GetComponent<BroccoliManager>().CutBroccoli();
                        }
                    }
                }
            }
            collsCabbages = thisFrameCabbages;

            collsRedCabbage = thisFrameRedCabbages;
        
            collsBlueCabbage = thisFrameBlueCabbages;

            collsBroccoli = thisFrameBroccolies;

            lastMousePos = Input.mousePosition;
           


        }
    }

    public void LoseLife()
    {
        lifePoints -= substractPoints;
        if( lifePoints >= 0)
        {
            setLifePointsText();
        }
        if (lifePoints <= 0)
        {
            GameOver();
        }
    }

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

    public void GameOver()
    {
        MetodoBurbuja();
        PlayerPrefs.SetInt("MaxScore", ScoreTable[0]);
        PlayerPrefs.SetInt("2MaxScore", ScoreTable[1]);
        PlayerPrefs.SetInt("3MaxScore", ScoreTable[2]);
        PlayerPrefs.SetInt("4MaxScore", ScoreTable[3]);
        PlayerPrefs.SetInt("5MaxScore", ScoreTable[4]);
        isPaused = true;
        gameOverMenu.SetActive(true);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        isPaused = pauseMenu.activeSelf;
        Time.timeScale= Time.timeScale==0?1:0;
    }
}
