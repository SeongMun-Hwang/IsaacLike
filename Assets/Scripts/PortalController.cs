using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    //Portal
    public List<SpriteRenderer> renderers;

    //Enemy
    public List<GameObject> enemyPrefabs; //������ ���� ������ ����Ʈ
    public List<GameObject> enemies; //������ ���� ���� ����Ʈ
    int minEnemy = 4;
    int maxEnemy = 8;

    //Map
    public GameObject currentMap;
    public List<GameObject> maps;

    //stage
    public TextMeshProUGUI stageText;
    int stageRound = 0;
    
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
        stageText.text = stageRound+"";
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
        Debug.Log(collision.tag);
        if(collision.CompareTag("Player")&&Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(currentMap);
            int rand = Random.Range(0, 2);
            GameObject go = Instantiate(maps[rand]);
            currentMap = go;
            stageRound++;
            SpawnMonster();
        }
    }
}
