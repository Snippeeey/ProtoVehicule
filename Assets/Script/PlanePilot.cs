using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanePilot : MonoBehaviour
{
    public float rotationValue;
    public float speed;
    public float acc�leration;
    public float dec�lerationpower;
    public float camBias = 0.96f;
    private RaycastHit hitF;
    public float maxDistanceF = 300f;
    public Camera cam;
    private Vector3 target;
    public RectTransform Crosshair;
    public RectTransform targetCanvas;
    public GameObject Cursor,pivot;
    public CinemachineRecomposer rc;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        Movement();
        //CamMovement();
        Groundcolliding();
        Acceleration();
        RepositionnateCrossair();
        OnDrawGizmo();

    }
    private void Rotation()
    {

        // transform.Rotate(Input.GetAxis("Vertical")/2, 0.0f, -Input.GetAxis("Horizontal")/2);
        transform.Rotate(-Input.GetAxis("Vertical") / 2, rotationValue, -Input.GetAxis("Horizontal") /2);
        rc.m_Dutch = transform.localEulerAngles.z;

    }
    private void Movement()
    {
        transform.position += transform.forward * Time.deltaTime * (speed + acc�leration);
        speed -= transform.forward.y * Time.deltaTime * dec�lerationpower;
        if (speed <= 35)
        {
            speed = 35;
        }
        if (Input.GetButton("RB"))
        {
            rotationValue = 0.2f;
        }
        else if (Input.GetButton("LB"))
        {
            rotationValue = -0.2f;
        }
        else
            rotationValue = 0;

    }
    private void CamMovement()
    {
        Vector3 moveCamTo = transform.position - transform.forward * 50f + Vector3.up * 35f;
        Camera.main.transform.position = Camera.main.transform.position * camBias + moveCamTo * (1.0f - camBias); ;

        Camera.main.transform.LookAt(transform.position + transform.forward * 30f);
        Camera.main.transform.Rotate(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, transform.rotation.z);
    }
    private void Groundcolliding()
    {
        float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);
        if (terrainHeightWhereWeAre > transform.position.y)
        {
            Debug.Log("boom");
        }

    }
    private void Acceleration()
    {
        //speed = speed + acc�leration;
        float InputValue = Input.GetAxis("Fire1");
        acc�leration = Mathf.Lerp(0, InputValue, 0.5f) * 150;


    }
   /* public void Cursor()
    {
       /* Vector3 screenPos = cam.WorldToScreenPoint(target.position);
        Crosshair.(screenPos);
    }*/



     private void OnDrawGizmo()
    {



        bool isHitF = Physics.BoxCast(transform.position, transform.localScale, direction: transform.forward, out hitF,
        transform.rotation, Mathf.Infinity);

        target = hitF.point;

        Debug.Log(target);
        if (isHitF)
        {



            //stopmove();
            Gizmos.color = Color.red;
            Gizmos.DrawRay(from: transform.position, direction: transform.forward * hitF.distance);
            //Gizmos.DrawWireCube(center: transform.position + transform.forward * hitF.distance, size: transform.localScale);






        }
        else if (!isHitF)
        {


            Gizmos.color = Color.green;
            Gizmos.DrawRay(from: transform.position, direction: transform.forward * maxDistanceF);




        }


    }
    public void SetHealthBarData(Vector3 targetTransform, RectTransform healthBarPanel)
    {
        this.targetCanvas = healthBarPanel;
        Crosshair = GetComponent<RectTransform>();
        target = targetTransform;
        RepositionnateCrossair();
        Crosshair.gameObject.SetActive(true);
    }
    public void RepositionnateCrossair()
    {
        Crosshair.gameObject.transform.SetParent(targetCanvas, false);
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(target);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * targetCanvas.sizeDelta.x) - (targetCanvas.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * targetCanvas.sizeDelta.y) - (targetCanvas.sizeDelta.y * 0.5f)));
        Debug.Log(target);

        Crosshair.anchoredPosition = WorldObject_ScreenPosition;



        /*elRect.width = Crosshair.GetComponent<Rect>().height;
        labelRect.height = Crosshair.GetComponent<Rect>().width;*/

        //GUI.DrawTexture(labelRect, Crosshair);

    }

    IEnumerator AutoRecover()
    {
        yield return new WaitForSeconds(0);
    }
}

    
       
    

   

      