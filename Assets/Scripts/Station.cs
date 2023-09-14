using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public Color colorRoof;
    public Color colorHouse;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameManager.instance;

        colorRoof = Random.ColorHSV();
        colorHouse = Random.ColorHSV();

        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colorRoof;
        transform.GetChild(1).GetComponent<SpriteRenderer>().color = colorHouse;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(train.transform.GetChild(0).GetComponent<Renderer>().material.color == colorHouse 
            && train.transform.GetChild(1).GetComponent<Renderer>().material.color == colorRoof)
        {
            train.gameObject.SetActive(false);
            gameManager.correctScore++;
            gameManager.CheckGameStatus();
        }
        else
        {
            gameManager.failScore++;
            train.gameObject.SetActive(false);
            gameManager.CheckGameStatus();
        }
    }
}
