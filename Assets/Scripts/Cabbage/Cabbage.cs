using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cabbage : MonoBehaviour
{

    public bool isActive { get; set; }
    private float verticalSpeed;
    private float horizontalSpeed;
    private float gravity = 2.0f;
    private bool isCutted = false;
    public SpriteRenderer Srenderer;
    private float lastSpriteUpdate;
    private float spriteUpdateDelta = 0.22f;
    private float rotationSpeed;
    public int points = 1;


    public Sprite[] sprites;
    public int spriteIndex;

    // Start is called before the first frame update
    private void Start()
    {
       
    }

    public void LaunchCabbage(float ySpeed,float xSpeed,float xStart)
    {
        isActive = true;
        verticalSpeed = ySpeed;
        horizontalSpeed = xSpeed;
        transform.position = new Vector3(xStart, 0, 0);
        rotationSpeed = Random.Range(-180, 180);
        spriteIndex = 0;
        Srenderer.sprite = sprites[spriteIndex];
    }

    public void Update()
    {
        if (!isActive)
        {
            isCutted = false;
            return;
        }
        verticalSpeed -= gravity * Time.deltaTime;
        transform.position += new Vector3(horizontalSpeed, verticalSpeed, 0)* Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, rotationSpeed)*Time.deltaTime);

        if (isCutted)
        {
            if(spriteIndex != sprites.Length-1 && Time.time- lastSpriteUpdate > spriteUpdateDelta)
            {
                lastSpriteUpdate = Time.time;
                spriteIndex++;
                Srenderer.sprite = sprites[spriteIndex];
            }
        }

        if(transform.position.y < -1)
        {
            isActive = false;
            if (!isCutted)
            {
                Gamemanager.game.LoseLife();
            }
        }
    }

    public void CutCabbage()
    {
        if (!isCutted)
        {
            if (verticalSpeed < 0.5f)
            {
                verticalSpeed = 0.5f;
            }
            horizontalSpeed = horizontalSpeed / 2;

            isCutted = true;
            Gamemanager.game.incrementScore(points);
            SoundManager.Instance.PLaySound(0);
        }
        
    }
}
