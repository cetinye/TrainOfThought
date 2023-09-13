using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public Color colorRoof;
    public Color colorHouse;

    // Start is called before the first frame update
    void Awake()
    {
        colorRoof = Random.ColorHSV();
        colorHouse = Random.ColorHSV();

        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colorRoof;
        transform.GetChild(1).GetComponent<SpriteRenderer>().color = colorHouse;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
