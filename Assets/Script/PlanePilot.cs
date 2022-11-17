using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanePilot : MonoBehaviour
{
    public float rotationValue;
    public float speed;
    public float accéleration;
    public float decélerationpower;
    public float camBias = 0.96f;
    private RaycastHit hitF;
    public float maxDistanceF = 300f;
    public Camera cam;
    private Vector3 target;
    public RectTransform Crosshair;
    public RectTransform targetCanvas;
    public GameObject Cursor, pivot;
    public CinemachineRecomposer rc3P,rc1P;
    private Transform ennemieTransform;
    private GameObject targetedennemie;
    public GameObject homingMissile;
    public Transform missilePoint1,missilePoint2;
    private Animator _CMVCamStateDrivenCamera;
    private bool switchCMVc;

    // Start is called before the first frame update
    void Start()
    {
        _CMVCamStateDrivenCamera = GameObject.FindGameObjectWithTag("StateCam").GetComponent<Animator>();
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
        EnnemiDetection();
        FireMissile();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("YYYYY");
            if(switchCMVc)
            {
                SwitchCam("CMVcam2");
                switchCMVc = false;
            }
            else
            {
                switchCMVc = true;
                SwitchCam("CMVcam1");
            }
        }

    }
    private void Rotation()
    {

        // transform.Rotate(Input.GetAxis("Vertical")/2, 0.0f, -Input.GetAxis("Horizontal")/2);
        transform.Rotate(-Input.GetAxis("Vertical") /1.5f, rotationValue, -Input.GetAxis("Horizontal")/1.3f );
        rc3P.m_Dutch = transform.localEulerAngles.z;
        rc1P.m_Dutch = transform.localEulerAngles.z;

    }
    private void Movement()
    {
        transform.position += transform.forward * Time.deltaTime * (speed + accéleration);
        speed -= transform.forward.y * Time.deltaTime * decélerationpower;
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
            //Debug.Log("boom");
        }

    }
    private void Acceleration()
    {
        //speed = speed + accéleration;
        float InputValue = Input.GetAxis("Fire1");
        accéleration = Mathf.Lerp(0, InputValue, 0.5f) * 150;


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

        //Debug.Log(target);
        if (isHitF)
        {


            
            //stopmove();
            /*Gizmos.color = Color.red;
            Gizmos.DrawRay(from: transform.position, direction: transform.forward * hitF.distance);*/
            //Gizmos.DrawWireCube(center: transform.position + transform.forward * hitF.distance, size: transform.localScale);






        }
        else if (!isHitF)
        {


            /*Gizmos.color = Color.green;
            Gizmos.DrawRay(from: transform.position, direction: transform.forward * maxDistanceF);*/




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
        //Debug.Log(target);

        Crosshair.anchoredPosition = WorldObject_ScreenPosition;



        /*elRect.width = Crosshair.GetComponent<Rect>().height;
        labelRect.height = Crosshair.GetComponent<Rect>().width;*/

        //GUI.DrawTexture(labelRect, Crosshair);

    }
    public void EnnemiDetection()
    {


        if (hitF.transform.gameObject.GetComponent<Ennemie>() == true )
        {
            
            ennemieTransform = hitF.transform.gameObject.transform;
              
           
        }
       
    }
    private void FireMissile()
    {
        //Debug.Log("fire");
        if(Input.GetButtonDown("fire1") && ennemieTransform != null)
        {
            Instantiate(homingMissile,missilePoint1.position,missilePoint1.rotation);
            homingMissile.GetComponent<HomingMissile>().RocketTarget=ennemieTransform;

        }
    }

    IEnumerator AutoRecover()
    {
        yield return new WaitForSeconds(0);
    }

    public void SwitchCam(string cameraToSwitch)
    {
        _CMVCamStateDrivenCamera.Play(cameraToSwitch);
    }
}




/*if (ennemieTransform != null)
{
    for (int i = 0; i > ennemieTransform.Count; i++)
    {
        if (hitF.transform != ennemieTransform[i])
        {
            locked = false;

        }
        else if (hitF.transform == ennemieTransform[i])
        {
            locked = true;
        }

    }
    if (!locked)
    {
        ennemieTransform.Add(hitF.transform);
        Debug.Log(locked);
        locked = true;
    }

}
if (ennemieTransform == null)

{
    Debug.Log("Get");
    Debug.Log(ennemieTransform[0]);
    ennemieTransform.Add(hitF.transform);
}*/


