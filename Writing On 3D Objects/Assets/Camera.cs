using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    GameObject go;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = (Quaternion.EulerAngles(Input.mousePosition));
    }
}
