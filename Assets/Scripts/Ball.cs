using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]Vector3 forcedirection = new Vector3(0f,0f,5f);
    [SerializeField]float power = 8f;
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
            Vector3 velocity = rb.linearVelocity;
            velocity.x = mouseX * mousespeed;
            rb.linearVelocity = velocity;
        }
    }

    //Hopping Machanism code using Collision
    private void OnCollisionEnter(Collision collision) {

        if (collision != null && collision.gameObject.name != "Start_Panel") {
            // Always hop in the direction of the collision normal
            Vector3 hopNormal = Vector3.zero;
            foreach (ContactPoint contact in collision.contacts) {
                hopNormal += contact.normal;
            }
            hopNormal.Normalize();

            // Blend normal and forcedirection (weights can be adjusted)
            float normalWeight = 0.5f;
            float forwardWeight = 0.7f;
            Vector3 hopDirection = (hopNormal * normalWeight + forcedirection.normalized * forwardWeight).normalized;

            // Set linearVelocity for consistent hop
            rb.linearVelocity = hopDirection * power;

            // Set velocity directly for consistent hops
            //rb.linearVelocity = hopDirection * power;
        }


        //string collidedObjectname = collision.gameObject.name;

        //if (collision != null && collidedObjectname != "Start_Panel") {
        //    rb.linearVelocity = Vector3.zero;
        //    rb.AddForce(forcedirection * power , ForceMode.Force);
        //}
    }
}
