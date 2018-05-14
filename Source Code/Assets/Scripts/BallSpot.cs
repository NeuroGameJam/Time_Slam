using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpot : MonoBehaviour 
{
    public GameObject ballPrefab;
    private GameObject lastBall;

    private void Start()
    {
        InstantiateNewBall();
    }

    private void InstantiateNewBall()
    {
        lastBall = Instantiate(ballPrefab, transform.position, Quaternion.identity);
    }

    private void ResetBall()
    {
        lastBall.transform.position = transform.position;
        Rigidbody rb = lastBall.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == lastBall)
        {
            ResetBall();
        }
    }
}
