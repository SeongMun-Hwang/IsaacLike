using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    //Portal
    public List<SpriteRenderer> renderers;

    //Enemy
    public List<GameObject> enemyPrefabs; //복사할 몬스터 프레팹 리스트
    public List<GameObject> enemies; //복사한 몬스터 저장 리스트
    int minEnemy = 4;
    int maxEnemy = 8;

    //Map
    public GameObject currentMap;
    public List<GameObject> maps;


    
    void Update()
    {
        CheckExistingEnemy();
        if (enemies.Count==0)
        {
            ControlAlpha(1f);
        }
        else
        {
            ControlAlpha(0f);
        }
    }
    void ControlAlpha(float alpha)
    {
        foreach (SpriteRenderer renderer in renderers)
        {
            Color color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }
    void CheckExistingEnemy()
    {
        for(int i=0;i<enemies.Count;i++)
        {
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
            }
        }
    }
    private void SpawnMonster()
    {
        int rand=Random.Range(minEnemy,maxEnemy);
        int rand1=Random.Range(0,rand);

        for(int i = 0; i < rand1; i++)
        {
            GameObject go = Instantiate(enemyPrefabs[0]);
            enemies.Add(go);
        }
        for (int i = 0; i < rand-rand1; i++)
        {
            GameObject go = Instantiate(enemyPrefabs[1]);
            enemies.Add(go);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&&Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(currentMap);
            int rand = Random.Range(0, 2);
            GameObject go = Instantiate(maps[rand]);
            currentMap = go;
            SpawnMonster();
        }
    }
}
