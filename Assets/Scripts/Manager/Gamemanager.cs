using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Cabbage> cabbages = new List<Cabbage>();

    public GameObject cabbagePrefab;
    private float lastSpawn;
    private float deltaSpawn = 1.0f;
    public Transform trail;
    private Collider2D[] collsCabbages;
    private Vector3 lastMousePos;
    private int substractPoints;
    private int incrementPoints;
    private bool isPaused;
   
    public static Gamemanager game { get; set; }

    private const float minimum_cut_Speed = 400.0f;
    private int score, bestScore, lifePoints;

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

    private void Awake()
    {
        game = this;
    }
    public void NewGame()
    {
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1;
        score = 0;
        lifePoints = 500;
        substractPoints = 100;
        incrementPoints = 10;
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
        NewGame();
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
            lastSpawn = Time.time;
        }

        if (Input.GetMouseButton(0))
        {

            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = -1;
            trail.position = position;

            Collider2D[] thisFrameCabbages = Physics2D.OverlapPointAll(new Vector2(position.x, position.y), LayerMask.GetMask("Cabbage"));

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
                        }
                    }
                }
            }
            lastMousePos = Input.mousePosition;
            collsCabbages = thisFrameCabbages;


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

    public void incrementScore()
    {
        score += incrementPoints;
        setScorePointsText();
        if (score > bestScore)
        {
            bestScore = score;
            setBestScorePointsText();
            PlayerPrefs.SetInt("MaxScore", bestScore);
        }
    }

    public void GameOver()
    {
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
