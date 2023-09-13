using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class Train : MonoBehaviour
{
    public GameObject currentPlatform;
    public Transform platforms;
    public Vector3[] platformArr = new Vector3[1];
    public float duration;
    public PathType pathType;
    public PathMode pathMode;
    public float raycastDistance = 0.5f;
    public GameObject previousPlatform;
    public GameObject temp;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3[] platformArr = new Vector3[platforms.childCount];
        //for (int i = 0; i < platformArr.Length; i++)
        //{
        //    platformArr[i] = platforms.GetChild(i).position;
        //}
        //transform.DOPath(platformArr, duration, pathType, pathMode).SetLookAt(0.0001f);
        previousPlatform = GameObject.FindGameObjectWithTag("Tunnel");
        temp = GameObject.FindGameObjectWithTag("Tunnel");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //cast ray in front
        RaycastHit2D hitFront = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), raycastDistance);
        //draw ray in front
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * raycastDistance, Color.red, 5f);
        
        currentPlatform = hitFront.collider.gameObject;

        DetectRoute();
        
        //platformArr[0] = currentPlatform.transform.position;


        transform.DOPath(platformArr, 5f, pathType, pathMode).SetLookAt(0.0001f);

    }

    void DetectRoute()
    {
        if (currentPlatform.GetComponent<SpriteRenderer>().sprite == 
            currentPlatform.GetComponent<Platform>().listAllSprites[0]) 
        {
            if (previousPlatform.transform.position.x < 
                currentPlatform.GetComponent<Platform>().transform.position.x)
                    platformArr[0] = currentPlatform.GetComponent<Platform>().rightPlatform.transform.position;
            else
                platformArr[0] = currentPlatform.GetComponent<Platform>().leftPlatform.transform.position;
        }

        if (currentPlatform.GetComponent<SpriteRenderer>().sprite ==
            currentPlatform.GetComponent<Platform>().listAllSprites[1])
        {
            if (previousPlatform.transform.position.y - 
                currentPlatform.GetComponent<Platform>().transform.position.y > 0)
                    platformArr[0] = currentPlatform.GetComponent<Platform>().downPlatform.transform.position;
            else
                platformArr[0] = currentPlatform.GetComponent<Platform>().upPlatform.transform.position;
        }

        if (currentPlatform.GetComponent<SpriteRenderer>().sprite ==
            currentPlatform.GetComponent<Platform>().listAllSprites[2])
        {
            if (previousPlatform.transform.position.y !=
                currentPlatform.GetComponent<Platform>().transform.position.y)
                    platformArr[0] = currentPlatform.GetComponent<Platform>().leftPlatform.transform.position;
            else
                platformArr[0] = currentPlatform.GetComponent<Platform>().downPlatform.transform.position;
        }

        if (currentPlatform.GetComponent<SpriteRenderer>().sprite ==
            currentPlatform.GetComponent<Platform>().listAllSprites[3])
        {
            if (previousPlatform.transform.position.x -
                currentPlatform.GetComponent<Platform>().transform.position.x > 0)
                    platformArr[0] = currentPlatform.GetComponent<Platform>().downPlatform.transform.position;
            else
                platformArr[0] = currentPlatform.GetComponent<Platform>().rightPlatform.transform.position;
        }

        if (currentPlatform.GetComponent<SpriteRenderer>().sprite ==
            currentPlatform.GetComponent<Platform>().listAllSprites[4])
        {
            if (previousPlatform.transform.position.y -
                currentPlatform.GetComponent<Platform>().transform.position.y > 0)
                    platformArr[0] = currentPlatform.GetComponent<Platform>().leftPlatform.transform.position;
            else
                platformArr[0] = currentPlatform.GetComponent<Platform>().upPlatform.transform.position;
        }

        if (currentPlatform.GetComponent<SpriteRenderer>().sprite ==
            currentPlatform.GetComponent<Platform>().listAllSprites[5])
        {
            if (temp.transform.position.y -
                currentPlatform.GetComponent<Platform>().transform.position.y == 0)
                    platformArr[0] = currentPlatform.GetComponent<Platform>().upPlatform.transform.position;
            else
                platformArr[0] = currentPlatform.GetComponent<Platform>().rightPlatform.transform.position;
        }

        if (currentPlatform != temp)
        {
            previousPlatform = temp;
            temp = currentPlatform;
        }
    }
}
