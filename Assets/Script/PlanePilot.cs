using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePilot : MonoBehaviour
{
    public float speed;
    public float acc�leration;
    public float dec�lerationpower;
    public float camBias = 0.96f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation();
        movement();
        CamMovement();
        groundcolliding();
    }
    private void rotation()
    {
       
        transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
    }
    private void movement()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        speed -= transform.forward.y*Time.deltaTime*dec�lerationpower;
        if (speed<=35)
        {
            speed = 35;
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

}
