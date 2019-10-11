using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : MonoBehaviour
{

    public bool isActive { get; set; }
    private float verticalSpeed;
    private float horizontalSpeed;
    private float gravity = 2.0f;
    private bool isCutted = false;
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
    }

    public void Update()
    {
        if (!isActive)
        {
            return;
        }
        verticalSpeed -= gravity * Time.deltaTime;
        transform.position += new Vector3(horizontalSpeed, verticalSpeed, 0)* Time.deltaTime;

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
            Gamemanager.game.incrementScore();
        }
        
    }
}
