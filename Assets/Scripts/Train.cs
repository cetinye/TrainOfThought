using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Train : MonoBehaviour
{
    public Transform platforms;
    public PathType pathType;
    public PathMode pathMode;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] platformArr = new Vector3[platforms.childCount];
        for (int i = 0; i < platformArr.Length; i++)
        {
            platformArr[i] = platforms.GetChild(i).position;
        }

        transform.DOPath(platformArr, 5f, pathType, pathMode).SetLookAt(0.0001f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
