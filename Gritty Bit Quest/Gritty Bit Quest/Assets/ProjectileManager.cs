using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField]
    Vector3 MinXYZ;
    [SerializeField]
    Vector3 MaxXYZ;
    [SerializeField]
    List<GameObject> Projectiles = new List<GameObject>();
    [SerializeField]
    List<GameObject> projectilePrefabs = new List<GameObject>();
    [SerializeField]
    Vector2 projectileLimitRange;
    [SerializeField]
    int projectileLimit;
    [SerializeField]
    float pullForce;


    void Update()
    {
        Projectiles = GameManager.OrderList(Projectiles);
    }

    public void PullTowardsBoss()
    {
        for (int i = 0; i < Projectiles.Count; i++)
        {
            Vector3 direction = (transform.root.position - Projectiles[i].transform.position).normalized;
            Projectiles[i].GetComponent<Rigidbody>().AddForce(direction * pullForce);
        }
    }

    public void AddProjectileToList(GameObject p)
    {
        Projectiles.Add(p);
    }

    public void RemoveProjectileFromList(GameObject p)
    {
        Projectiles.Remove(p);
    }

    public GameObject GetRandomProjectile()
    {
        if (Projectiles.Count > 0)
            return Projectiles[Random.Range(0, Projectiles.Count - 1)];
        else
            return null;
    }

    public void ResetCubes()
    {
        projectileLimit = (int)Random.Range(projectileLimitRange.x, projectileLimitRange.y);

        for (int i = 0; i < projectileLimit; i++)
        {
            SpawnProjectile();
        }

        for (int i = 0; i < Projectiles.Count; i++)
        {
            RandomizeCubes();
        }
    }

    public void SpawnProjectile()
    {
        GameObject temp = (GameObject)Instantiate(projectilePrefabs[Random.Range(0,projectilePrefabs.Count)]);
        temp.GetComponent<CubeBehaviour>().SetBoss(GameManager.Boss);
        temp.GetComponent<CubeBehaviour>().SetCubeState(ObjectState.ObjectStates.HeldByBoss);
        temp.transform.parent = transform;
        AddProjectileToList(temp);
    }

    public void RandomizeCubes()
    {
        for (int i = 0; i < Projectiles.Count; i++)
        {
            Projectiles[i].transform.localPosition = new Vector3(Random.Range(MinXYZ.x, MaxXYZ.x), Random.Range(MinXYZ.y, MaxXYZ.y), Random.Range(MinXYZ.z, MaxXYZ.z));
        }
    }

    public int GetProjectileLimit()
    {
        return projectileLimit;
    }
}
