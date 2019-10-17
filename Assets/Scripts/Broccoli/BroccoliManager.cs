using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliManager : MonoBehaviour
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
    public int points = 0;

    // Start is called before the first frame update
    private void Start()
    {

    }

    public void LaunchBroccoli(float ySpeed, float xSpeed, float xStart)
    {
        isActive = true;
        verticalSpeed = ySpeed;
        horizontalSpeed = xSpeed;
        transform.position = new Vector3(xStart, 0, 0);
        rotationSpeed = Random.Range(-180, 180);
       
    }

    public void Update()
    {
        if (!isActive)
        {
            isCutted = false;
            return;
        }
        verticalSpeed -= gravity * Time.deltaTime;
        transform.position += new Vector3(horizontalSpeed, verticalSpeed, 0) * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);

        if (isCutted)
        {
            
        }

    }

    public void CutBroccoli()
    {
        Gamemanager.game.GameOver();
    }
}
