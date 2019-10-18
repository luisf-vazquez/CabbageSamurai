using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliManager : MonoBehaviour
{
    public bool isActive { get; set; }
    public int points = 0;
    public SpriteRenderer Srenderer;

    private float verticalSpeed;
    private float horizontalSpeed;
    private float gravity = 2.0f;
    private float rotationSpeed;


    /// <summary>
    /// Se le llama antes de actualizar el primer frame.
    /// </summary>
    private void Start()
    {

    }
    /// <summary>
    /// Evento que lanza un objeto 'Brócoli'
    /// </summary>
    /// <param name="xSpeed">Velocidad en el eje x que tomará el brocoli</param>
    /// <param name="xStart">Punto del eje x desde donde se lanza el brocoli</param>
    /// <param name="ySpeed">Velocidad en el eje y que tomará el brocoli</param>
    public void LaunchBroccoli(float ySpeed, float xSpeed, float xStart)
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
       
    }
    /// <summary>
    /// Se le llama una vez por actualización de frame.
    /// </summary>
    public void Update()
    {
        //Si el objeto ya no está activo (no se encuentra en juego) seteamos la variable de corte a false, para que se pueda reutilizar.
        if (!isActive)
        {
            return;
        }
        //Conseguimos el efecto de caída tras un cierto tiempo.
        verticalSpeed -= gravity * Time.deltaTime;
        transform.position += new Vector3(horizontalSpeed, verticalSpeed, 0) * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);


    }
    /// <summary>
    /// Evento que se lanza cuando cortamos un brocoli, automáticamente se pierde la partida.
    /// </summary>
    public void CutBroccoli()
    {
        Gamemanager.game.GameOver();
    }
}
