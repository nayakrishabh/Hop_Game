using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 forcedirection = new Vector3(0f,6f,5f);
    float power = 50f;
    private Rigidbody rb;
    private float mousespeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        //Ball Control Code
        if (gameManager.startflag) {
            float mouseX = Input.GetAxis("Mouse X");
            Debug.Log(mouseX);
            Vector3 newposition = transform.position + new Vector3(mouseX * mousespeed * Time.deltaTime, 0, 0);
            transform.position = newposition;
        }
    }

    //Hopping Machanism code using Collision
    private void OnCollisionEnter(Collision collision) {

        string collidedObjectname = collision.gameObject.name;

        if (collision != null && collidedObjectname != "Start_Panel") {
            rb.velocity = Vector3.zero;
            rb.AddForce(forcedirection * power , ForceMode.Force);
        }
    }
}
