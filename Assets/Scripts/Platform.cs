using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour, IPlatform
{
    public GameObject upPlatform;
    public GameObject downPlatform;
    public GameObject rightPlatform;
    public GameObject leftPlatform;

    public float raycastDistance = 1;

    public List<Sprite> listAllSprites = new List<Sprite>();
    public List<Sprite> possibleSprites = new List<Sprite>();

    public int tapCount = 1;

    private void Start()
    {
        Debug.Log("New platform created");

        DetectNearbyPlatforms();
        CheckPossibleSprites();
    }

    public void Tapped()
    {
        tapCount++;
        Debug.Log("Tapped on a platform");

        this.GetComponent<SpriteRenderer>().sprite = possibleSprites[tapCount % possibleSprites.Count];
    }

    void DetectNearbyPlatforms()
    {
        //cast raycast in all directions
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, raycastDistance);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, raycastDistance);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, raycastDistance);

        //show the ray
        Debug.DrawRay(transform.position, Vector2.up * raycastDistance, Color.red, 5f);
        Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red, 5f);
        Debug.DrawRay(transform.position, Vector2.right * raycastDistance, Color.red, 5f);
        Debug.DrawRay(transform.position, Vector2.left * raycastDistance, Color.red, 5f);

        //get the surrounding object - if hit something and its not itself 
        if (hitUp.collider != null && hitUp.collider.gameObject != this.gameObject)
            upPlatform = hitUp.collider.gameObject;

        if (hitDown.collider != null && hitDown.collider.gameObject != this.gameObject)
            downPlatform = hitDown.collider.gameObject;

        if (hitRight.collider != null && hitRight.collider.gameObject != this.gameObject)
            rightPlatform = hitRight.collider.gameObject;

        if (hitLeft.collider != null && hitLeft.collider.gameObject != this.gameObject)
            leftPlatform = hitLeft.collider.gameObject;
    }

    void CheckPossibleSprites()
    {
        //if there are right and left neighbours, add certain sprite to list
        if (rightPlatform != null && leftPlatform != null)
            possibleSprites.Add(listAllSprites[0]);

        if (upPlatform != null && downPlatform != null)
            possibleSprites.Add(listAllSprites[1]);

        if (leftPlatform != null && downPlatform != null)
            possibleSprites.Add(listAllSprites[2]);

        if (rightPlatform != null && downPlatform != null)
            possibleSprites.Add(listAllSprites[3]);

        if (upPlatform != null && leftPlatform != null)
            possibleSprites.Add(listAllSprites[4]);

        if (upPlatform != null && rightPlatform != null)
            possibleSprites.Add(listAllSprites[5]);
    }
}
