using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damege = 10;
    public Animator animator;
    public string triggerAttack = "Attack";
    public string triggerKill = "Kill";
    public HealthBase healthBase;
    public float timeToDestroy = 1f;

    void Awake()
    {
        if(healthBase != null)
        {
            healthBase.onKill += OnEnemyKill;
        }
    }

    void OnEnemyKill()
    {
        healthBase.onKill -= OnEnemyKill;
        PlayKillAnimation();
        Destroy(this.gameObject, timeToDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();

        if (health != null)
        {
            health.Damage(damege);
            PlayAttackAnimation();
        }
    }

    private void PlayAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }

    private void PlayKillAnimation()
    {
        animator.SetTrigger(triggerKill);
    }

    public void Damage(int amount)
    {
        healthBase.Damage(amount);
    }
}
