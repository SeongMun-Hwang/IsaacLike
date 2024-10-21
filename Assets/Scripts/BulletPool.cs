using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int initialBulletNumber = 30;

    List<GameObject> bulletPool;

    private void Start()
    {
        bulletPool = new List<GameObject>();
        for(int i=0;i<initialBulletNumber; i++)
        {
            GameObject go=Instantiate(bulletPrefab, transform);
            go.SetActive(false);
            bulletPool.Add(go);
        }
    }
    public GameObject GetObject()
    {
        Debug.Log("GetObject()");
        foreach(GameObject go in bulletPool)
        {
            if (!go.activeSelf)
            {
                Debug.Log("Use existing bulelt");
                go.SetActive(true);
                return go;
            }
        }
        Debug.Log("Create New Bullet");
        GameObject bullet = Instantiate(bulletPrefab, transform);
        bulletPool.Add(bullet);
        return bullet;
    }
}
