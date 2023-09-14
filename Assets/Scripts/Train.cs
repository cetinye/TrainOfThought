using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Train : MonoBehaviour
{
    [SerializeField] private float moveDuration = 3f;
    [SerializeField] private GameObject currentPlatform;
    [SerializeField] private Transform platforms;
    [SerializeField] private Vector3[] platformArr = new Vector3[1];
    [SerializeField] private float duration;
    [SerializeField] private PathType pathType;
    [SerializeField] private PathMode pathMode;
    [SerializeField] private float raycastDistance;
    [SerializeField] private GameObject previousPlatform;
    [SerializeField] private GameObject tempPlatform;

    //public bool rayDistFlag = true;
    //private float tempDist;

    // Start is called before the first frame update
    void Start()
    {
        //tempDist = raycastDistance;

        FindPlatform();

    }

    // Update is called once per frame
    void Update()
    {
        //to detect front platform on spawn change ray distance
        //raycastDistance = (rayDistFlag) ? 0.5f : tempDist;
        //rayDistFlag = false;

        //cast ray in front
        RaycastHit2D hitFront = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), raycastDistance);
        //draw ray in front
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * raycastDistance, Color.red, 5f);
        
        if (hitFront.collider != null && 
                hitFront.collider.gameObject.TryGetComponent<IPlatform>(out IPlatform iPlatform))
                    currentPlatform = hitFront.collider.gameObject;

        DetectRoute();

        //make train follow the path
        //transform.DOPath(platformArr, 5f, pathType, pathMode).SetLookAt(0.0001f);

        transform.DOMove(platformArr[0], moveDuration, false);
        transform.right = platformArr[0] - transform.position;
    }

    void DetectRoute()
    {
        //check platforms sprite regarding with trains coming direction and assign required platform to route array
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
            if (tempPlatform.transform.position.y -
                currentPlatform.GetComponent<Platform>().transform.position.y == 0)
                    platformArr[0] = currentPlatform.GetComponent<Platform>().upPlatform.transform.position;
            else
                platformArr[0] = currentPlatform.GetComponent<Platform>().rightPlatform.transform.position;
        }

        if (currentPlatform != tempPlatform)
        {
            previousPlatform = tempPlatform;
            tempPlatform = currentPlatform;
        }
    }

    void FindPlatform()
    {
        previousPlatform = GameObject.FindGameObjectWithTag("Tunnel");
        tempPlatform = GameObject.FindGameObjectWithTag("Tunnel");

        //cast raycast in all directions
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1f);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f);

        if (hitUp.collider != null &&
                hitUp.collider.gameObject.TryGetComponent<IPlatform>(out IPlatform iUPlatform))
                    currentPlatform = hitUp.collider.gameObject;

        else if (hitDown.collider != null &&
                    hitDown.collider.gameObject.TryGetComponent<IPlatform>(out IPlatform iDPlatform))
                        currentPlatform = hitDown.collider.gameObject;

        else if (hitRight.collider != null &&
                    hitRight.collider.gameObject.TryGetComponent<IPlatform>(out IPlatform iRPlatform))
                        currentPlatform = hitRight.collider.gameObject;

        else if (hitLeft.collider != null &&
                    hitLeft.collider.gameObject.TryGetComponent<IPlatform>(out IPlatform iLPlatform))
                        currentPlatform = hitLeft.collider.gameObject;

    }
}
