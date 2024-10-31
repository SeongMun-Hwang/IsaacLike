using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public List<SpriteRenderer> renderers;
    public List<GameObject> enemies;

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
}
