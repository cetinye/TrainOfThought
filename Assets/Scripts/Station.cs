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
        //get random colors and paint station
        colorRoof = Random.ColorHSV();
        colorHouse = Random.ColorHSV();

        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colorRoof;
        transform.GetChild(1).GetComponent<SpriteRenderer>().color = colorHouse;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Train>(out Train train))
        {
            CheckColor(collision.gameObject);
        }   
    }

    private void CheckColor(GameObject train)
    {
        //check if train has the same color with the statino
        if(train.transform.GetChild(0).GetComponent<Renderer>().material.color == colorHouse 
            && train.transform.GetChild(1).GetComponent<Renderer>().material.color == colorRoof)
        {
            GameManager.instance.correctScore++;
            GameManager.instance.CheckGameStatus();
            train.gameObject.SetActive(false);
        }
        else
        {
            GameManager.instance.failScore++;
            GameManager.instance.CheckGameStatus();
            train.gameObject.SetActive(false);
        }
    }
}
