using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingPlane : MonoBehaviour
{
    public GameObject Plane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Plane.transform);
    }
}
