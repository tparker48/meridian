using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSounds : MonoBehaviour
{
    // shoot'n
    public AudioClip cock;
    public AudioClip fire;
    public AudioClip dryFire;

    // load'n
    public AudioClip openChamber;
    public AudioClip loadBullet;
    public AudioClip closeChamber;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnCock()
    {
        audioSource.PlayOneShot(cock);
    }
    public void OnFire()
    {
        audioSource.PlayOneShot(fire);
    }
    public void OnDryFire()
    {
        audioSource.PlayOneShot(dryFire);
    }
    public void OnReloadStart()
    {
        audioSource.PlayOneShot(openChamber);
    }
    public void OnLoadBullet()
    {
        audioSource.PlayOneShot(loadBullet);
    }
    public void OnReloadEnd()
    {
        audioSource.PlayOneShot(closeChamber);
    }
    
}
