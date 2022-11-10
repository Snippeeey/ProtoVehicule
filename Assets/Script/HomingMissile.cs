using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [Header("Setup")]
    public Transform RocketTarget;
    public Rigidbody RocketRgb;

    public float turnSpeed = 1f;
    public float rocketFlySpeed = 10f;

    private Transform rocketLocalTrans;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        WaitBoom();
        if (!RocketTarget)
            Debug.Log("Please set the Rocket Target");

        rocketLocalTrans = GetComponent<Transform>();
    }


    private void FixedUpdate()
    {
        if (!RocketRgb) //If we have not set the Rigidbody, do nothing..
            return;

        RocketRgb.velocity = rocketLocalTrans.forward * rocketFlySpeed;

        //Now Turn the Rocket towards the Target
        var rocketTargetRot = Quaternion.LookRotation(RocketTarget.position - rocketLocalTrans.position);

        RocketRgb.MoveRotation(Quaternion.RotateTowards(rocketLocalTrans.rotation, rocketTargetRot, turnSpeed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ennemie"))
        {
            Rigidbody plRgb = collision.gameObject.GetComponent<Rigidbody>();
            if (plRgb)
                plRgb.AddForceAtPosition(Vector3.up * 1000f, plRgb.position);

            //Deactivate Rocket..
            Instantiate(explosion,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
    IEnumerator WaitBoom()
    {
        yield return new WaitForSeconds(7f);
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}

