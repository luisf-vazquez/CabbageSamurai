using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private const float  minimum_cut_Speed= 400.0f;
    private Cabbage GetCabbage()
    {
        Cabbage c = cabbages.Find(x => !x.isActive);
        if( c== null)
        {
            c = Instantiate(cabbagePrefab).GetComponent<Cabbage>();
            cabbages.Add(c);
        }

        return c;
    }
    void Start()
    {
        collsCabbages = new Collider2D[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSpawn > deltaSpawn)
        {

            Cabbage cabbage = GetCabbage();
            float randomX = Random.Range(-1.55f, 1.55f);
            float randomY = Random.Range(1.75f, 2.75f);
            cabbage.LaunchCabbage(randomY, randomX, -randomX);
            lastSpawn = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            
            Vector3 position= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = -1;
            trail.position = position;

            Collider2D[] thisFrameCabbages= Physics2D.OverlapPointAll(new Vector2(position.x, position.y), LayerMask.GetMask("Cabbage"));
            
            if((Input.mousePosition - lastMousePos).sqrMagnitude >= minimum_cut_Speed)
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
}
