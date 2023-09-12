using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTap();
    }

    void CheckTap()
    {
        //Checking if user is tapping anywhere on the scene
        if (Input.GetMouseButtonDown(0))
        {
            //if yes, get the position
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var touchPos = new Vector2(worldPoint.x, worldPoint.y);

            //checking if user tapped on a platform
            if (Physics2D.OverlapPoint(touchPos) != null &&
                Physics2D.OverlapPoint(touchPos).TryGetComponent<IPlatform>(out IPlatform iPlatform))
            {
                Debug.Log("Tapped");
                iPlatform.Tapped();
            }
        }
    }
}
