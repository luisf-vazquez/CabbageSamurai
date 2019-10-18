using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cabbage : MonoBehaviour
{

    public bool isActive { get; set; }
    public SpriteRenderer Srenderer;
    public Sprite[] sprites;
    public int spriteIndex;

    private float verticalSpeed;
    private float horizontalSpeed;
    private float gravity = 2.0f;
    private bool isCutted = false;
    private float lastSpriteUpdate;
    private float spriteUpdateDelta = 0.22f;
    private float rotationSpeed;
    private int points = 1;

    /// <summary>
    /// Se le llama antes de actualizar el primer frame.
    /// </summary>
    private void Start()
    {
       
    }
    /// <summary>
    /// Evento que lanza un objeto 'Lechuga'
    /// </summary>
    /// <param name="xSpeed">Velocidad en el eje x que tomará la lechuga</param>
    /// <param name="xStart">Punto del eje x desde donde se lanza la lechuga</param>
    /// <param name="ySpeed">Velocidad en el eje y que tomará el la lechuga</param>
    public void LaunchCabbage(float ySpeed,float xSpeed,float xStart)
    {
        //Lo marcamos como activo
        isActive = true;
        //Seteamos las variables de velocidad globales
        verticalSpeed = ySpeed;
        horizontalSpeed = xSpeed;
        //Marcamos el punto de lanzamiento del brocoli
        transform.position = new Vector3(xStart, 0, 0);
        //El ángulo y velocidad de rotación que tendrá el brocoli
        rotationSpeed = Random.Range(-180, 180);
       //Marcamos el primer sprite de la lista como inicial y lo pintamos en pantalla
        spriteIndex = 0;
        Srenderer.sprite = sprites[spriteIndex];
    }
    /// <summary>
    /// Se le llama una vez por actualización de frame.
    /// </summary>
    public void Update()
    {
        //Si el objeto ya no está activo (no se encuentra en juego) seteamos la variable de corte a false, para que se pueda reutilizar.
        if (!isActive)
        {
            isCutted = false;
            return;
        }
        //Conseguimos el efecto de caída tras un cierto tiempo.
        verticalSpeed -= gravity * Time.deltaTime;
        transform.position += new Vector3(horizontalSpeed, verticalSpeed, 0)* Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, rotationSpeed)*Time.deltaTime);

        // Si se ha cortado el objeto ( colisionado con el jugador ) movemos el sprite y lo pintamos para dar la sensación de movimiento de corte.
        if (isCutted)
        {
            if(spriteIndex != sprites.Length-1 && Time.time- lastSpriteUpdate > spriteUpdateDelta)
            {
                lastSpriteUpdate = Time.time;
                spriteIndex++;
                Srenderer.sprite = sprites[spriteIndex];
            }
        }
        //Si el objeto cae fuera de la pantalla sin ser cortado, perdemos una vida.
        if(transform.position.y < -1)
        {
            isActive = false;
            if (!isCutted)
            {
                Gamemanager.game.LoseLife();
            }
        }
    }
    /// <summary>
    /// Evento que se lanza cuando cortamos una lechuga
    /// </summary>
    public void CutCabbage()
    {
        // Comprobamos que no está ya cortada
        if (!isCutted)
        {
            //Si aún no está cortada, hacemos que se mueva más lento
            if (verticalSpeed < 0.5f)
            {
                verticalSpeed = 0.5f;
            }
            horizontalSpeed = horizontalSpeed / 2;
            //La marcamos como cortada
            isCutted = true;
            //Incrementamos los puntos
            Gamemanager.game.incrementScore(points);
            //Hacemos sonar el corte
            SoundManager.Instance.PLaySound(0);
        }
        
    }
}
