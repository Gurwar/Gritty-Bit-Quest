using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunReloader : MonoBehaviour
{
    [SerializeField]
    float reloadTime;
    [SerializeField]
    Image reloadUI;
    float currentTime;
    bool reloading;
    GunAmmo gunAmmo;
    AudioSource audio;
    [SerializeField]
    AudioClip reloadClip;
    void Start()
    {
        gunAmmo = GetComponent<GunAmmo>();
        audio = GetComponent<AudioSource>();

    }

    public void Reload()
    {
        audio.PlayOneShot(reloadClip);

        reloading = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (reloading)
        {

            currentTime += Time.unscaledDeltaTime;
            reloadUI.fillAmount = currentTime / reloadTime;
            if (currentTime >= reloadTime)
            {
                currentTime = 0;
                reloading = false;
                GetComponent<GunAmmo>().RefillClip();
                reloadUI.fillAmount = 0;
            }
        }
    }
}
