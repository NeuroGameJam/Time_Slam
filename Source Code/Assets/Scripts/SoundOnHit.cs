using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SoundOnHit : MonoBehaviour 
{
    [SerializeField] private string soundName;
    [SerializeField] private bool useCollisionForceAsVolume = true;
    public float maxVelocity = 10;
    public float minVolume = .5f;


    private void OnCollisionEnter(Collision collision)
    {
        float volume = 1;
        if (useCollisionForceAsVolume)
        {
            volume = collision.relativeVelocity.magnitude / maxVelocity;
            volume = Mathf.Clamp(volume, minVolume, 1);
        }

        SFX.instance.PlayOneTimeClipWithRandomPitch(soundName, 1f, 1.2f, volume);
    }
}
