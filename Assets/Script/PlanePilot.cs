using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePilot : MonoBehaviour
{
    public float speed,minspeed;
    public float accéleration,maxacceleration;
    public float decélerationpower;
    public float camBias = 0.96f;
    public float TimeToAccelerate = 1f;
    private Rigidbody rg;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rotation();
        movement();
        CamMovement();
        groundcolliding();
        acceleration();
    }
    private void rotation()
    {
       
        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
    }
    private void movement()
    {
        rg.AddForce(transform.forward * speed);
        speed -= transform.forward.y*Time.deltaTime*decélerationpower;
        if (speed<=minspeed)
        {
            speed = minspeed;
        }
    }
    private void CamMovement()
    {
        Vector3 moveCamTo = transform.position - transform.forward* 50f + Vector3.up * 35f;
        Camera.main.transform.position = Camera.main.transform.position * camBias + moveCamTo*(1.0f - camBias); ;

        Camera.main.transform.LookAt(transform.position+ transform.forward*30f);
    }
    private void groundcolliding()
    {
        float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);
        if (terrainHeightWhereWeAre > transform.position.y)
        {

        }
        
    }
    private void acceleration()
    {
        //speed = speed + accéleration;
        float InputValue = Input.GetAxis("Fire1");
        accéleration = Mathf.Lerp(0, InputValue, TimeToAccelerate) * maxacceleration;
        rg.AddForce(transform.forward*accéleration);
        
    }

}
