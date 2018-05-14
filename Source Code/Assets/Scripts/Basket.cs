using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour 
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("ball"))
        {
            SFX.instance.PlayOneTimeClip("Score");
            GameManager.instance.Score();
        }
    }
}
