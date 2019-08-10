﻿using UnityEngine;
using System.Collections;

public class gunShooter : MonoBehaviour {
    [SerializeField]
    float FiringRate;
    [SerializeField]
    float inaccuracy;
	float CurrentFireTime;
    float currentOverheat;

    [SerializeField]
    int ProjectileCount;
    [SerializeField]
    int damage;
	bool canShootAgain = true;
	[SerializeField]
	Transform FireTransform;
    [SerializeField]
    Transform MuzzleFlashTransform;

    public LineRenderer laser;
    [SerializeField]
	GameObject MuzzleFlashPrefab;
    [SerializeField]
    GameObject ProjectilePrefab;
    bool firing = false;
    
	void Start()
	{
        laser.gameObject.SetActive(true);
    }

    void Update()
    {
        FireRate();
    }

	public bool Fire()
	{
		if (!canShootAgain)
			return false;

		if (GetComponentInParent<handScript>().currentWeapon.ProjectilePrefab == null)
			return false;

        firing = true;
        


        // when firing a single projectile per discharge (pistols, machineguns)
        // this loop will only run once. if firing several projectiles per
        // round (shotguns) the loop will iterate several times. the fire seed
        // is the same for every iteration, but is multiplied with the number
        // of iterations to get a unique, deterministic seed for each projectile.
        for (int v = 0; v < ProjectileCount; v++)
		{
            if (GetComponent<GunAmmo>().currentAmmo > 0)
            {
                GameObject p = null;
                p = (GameObject)Instantiate(ProjectilePrefab, FireTransform.position, FireTransform.rotation);
                if (TransformDeepChildExtension.FindDeepChild(transform, "FirePosition") != null)
                {
                    //for the shotgun the bulletspawn rotation should change for each bullet based on spread
                    if (p.GetComponent<g_ProjectileBullet>())
                    {
                        p.GetComponent<g_ProjectileBullet>().damage = damage;
                        p.GetComponent<g_ProjectileBullet>().bulletSpawnTransform = TransformDeepChildExtension.FindDeepChild(transform, "FirePosition");
                    }
                    else
                    {
                        p.transform.position = TransformDeepChildExtension.FindDeepChild(transform, "FirePosition").position;
                        p.GetComponent<g_HitscanBullet>().Damage = damage;
                    }
                }
                GetComponent<GunAmmo>().currentAmmo--;
            }
            else
            {
                return false;
            }
		}
        if (GetComponentInParent<handScript>().currentWeapon.MuzzleFlashPrefab != null)
            ShowMuzzleFlash();
        canShootAgain = false;
		return true;

	}

    public void StandBy()
    {
        firing = false;
        currentOverheat = 0;
    }


	void FireRate()
	{
		if (!canShootAgain)
		{

            CurrentFireTime += Time.deltaTime;
		    if (CurrentFireTime >= FiringRate)
		    {
                CurrentFireTime = 0;
		    	canShootAgain = true;
		    }
		}

        //set overheat value
        //if overheated, slow down fire rate
	}

    void OverHeat()
    {

        if (firing)
            currentOverheat += Time.deltaTime;
        if (currentOverheat > GetComponentInParent<handScript>().currentWeapon.overHeat)
        {
            currentOverheat = GetComponentInParent<handScript>().currentWeapon.overHeat;
            FiringRate = GetComponentInParent<handScript>().currentWeapon.overHeatFiringRate;
        }
        else
        {
            if (currentOverheat < 0)
                currentOverheat = 0;
            FiringRate = GetComponentInParent<handScript>().currentWeapon.fireRate;
        }
    }


    void ShowMuzzleFlash()
	{

		//MuzzleFlash = (GameObject)Instantiate(GetComponentInParent<handScript>().currentWeapon.MuzzleFlashPrefab, MuzzleFlashTransform.position, MuzzleFlashTransform.rotation);
		//MuzzleFlash.transform.parent = MuzzleFlashTransform;
	}

    
}
