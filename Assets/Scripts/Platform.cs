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

    private void Start()
    {
        Debug.Log("New platform created");
        DetectNearbyPlatforms();
    }

    public void Tapped()
    {
        Debug.Log("Tapped on a platform");
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

        //get the surrounding object  
        if (hitUp.collider != null && hitUp.collider.gameObject != this.gameObject)
            upPlatform = hitUp.collider.gameObject;

        if (hitDown.collider != null && hitDown.collider.gameObject != this.gameObject)
            downPlatform = hitDown.collider.gameObject;

        if (hitRight.collider != null && hitRight.collider.gameObject != this.gameObject)
            rightPlatform = hitRight.collider.gameObject;

        if (hitLeft.collider != null && hitLeft.collider.gameObject != this.gameObject)
            leftPlatform = hitLeft.collider.gameObject;
    }
}
