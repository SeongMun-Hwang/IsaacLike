using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    //Portal
    public List<SpriteRenderer> renderers;
    public bool portalActive = true;
    public bool playerOnPortal = false;
    //Enemy
    public List<GameObject> enemyPrefabs; //몬스터 프레팹 리스트
    public List<GameObject> enemies; //복사한 몬스터 저장 리스트
    int minEnemy = 4;
    int maxEnemy = 8;

    //Map
    public GameObject currentMap;
    public List<GameObject> maps;

    //stage
    public TextMeshProUGUI stageText;
    int stageRound = 0;

    //Boss
    public GameObject bossPrefab;
    public GameObject bossStage;
    bool isBossStage = false;

    //Canvas
    public GameObject victoryCanvas;

    void Update()
    {
        CheckExistingEnemy();
        if (enemies.Count == 0)
        {
            ControlAlpha(1f);
            portalActive = true;
            if (isBossStage)
            {
                victoryCanvas.SetActive(true);
            }
        }
        else
        {
            ControlAlpha(0f);
            portalActive = false;
        }
        stageText.text = "Stage" + stageRound;
        if (Input.GetKeyDown(KeyCode.Space) && portalActive && playerOnPortal)
        {
            if (stageRound % 4 == 0 && stageRound != 0)
            {
                MoveToBossStage();
            }
            else
            {
                MoveStage();
            }
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
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
            }
        }
    }
    private void MoveStage()
    {
        Destroy(currentMap);
        int rand = Random.Range(0, maps.Count);
        GameObject go = Instantiate(maps[rand]);
        currentMap = go;
        stageRound++;
        SpawnMonster();
    }
    private void SpawnMonster()
    {
        int rand = Random.Range(minEnemy, maxEnemy);
        int rand1 = Random.Range(0, rand);
        Vector3 instantiatePosition = gameObject.transform.position + new Vector3(0, 7.5f, 0);

        for (int i = 0; i < rand1; i++)
        {
            GameObject go = Instantiate(enemyPrefabs[0], instantiatePosition, Quaternion.identity);
            enemies.Add(go);
        }
        for (int i = 0; i < rand - rand1; i++)
        {
            GameObject go = Instantiate(enemyPrefabs[1], instantiatePosition, Quaternion.identity);
            enemies.Add(go);
        }
    }
    private void MoveToBossStage()
    {
        Destroy(currentMap);
        GameObject go = Instantiate(bossStage);
        currentMap = go;
        GameObject boss = Instantiate(bossPrefab, new Vector3(0, 5, 0), bossPrefab.transform.rotation);
        enemies.Add(boss);
        isBossStage = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPortal = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPortal = false;
        }
    }
}