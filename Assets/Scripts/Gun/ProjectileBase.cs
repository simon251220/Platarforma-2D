using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector2 direction;
    public float timeToDestroy = 2;
    public float side = 1;
    public int damageAmount = 1;
    void Awake()
    {
        Destroy(this.gameObject, timeToDestroy);   
    }
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * side);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.transform.GetComponent<EnemyBase>();

        if(enemy != null)
        {
            enemy.Damage(damageAmount);
            Destroy(this.gameObject);
        }
    }
}
