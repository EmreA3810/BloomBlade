using System.Collections.Generic;
using UnityEngine;

public class SpringAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public int attackDamage = 25;
    public int lastHitCount;
    public PlayerRunStats runStats;

    void Awake()
    {
        if (runStats == null) runStats = GetComponent<PlayerRunStats>();

        if (attackPoint == null)
        {
            Transform found = transform.Find("AttackPoint");
            if (found != null)
            {
                attackPoint = found;
            }
            else
            {
                GameObject p = new GameObject("AttackPoint");
                p.transform.SetParent(transform);
                p.transform.localPosition = new Vector3(0.8f, 0f, 0f);
                attackPoint = p.transform;
            }
        }
    }

    public void AttackWithStats(int damage, float range)
    {
        if (attackPoint == null) return;

        int finalDamage = damage + (runStats != null ? runStats.bonusDamage : 0);
        attackDamage = finalDamage;
        attackRange = range;

        Collider2D[] hits = enemyLayer.value == 0
            ? Physics2D.OverlapCircleAll(attackPoint.position, attackRange)
            : Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        if (hits.Length == 0)
            hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        HashSet<WeedEnemy> targets = new HashSet<WeedEnemy>();
        foreach (Collider2D hit in hits)
        {
            WeedEnemy enemy = hit.GetComponent<WeedEnemy>();
            if (enemy == null) enemy = hit.GetComponentInParent<WeedEnemy>();
            if (enemy == null) enemy = hit.GetComponentInChildren<WeedEnemy>();
            if (enemy != null) targets.Add(enemy);
        }

        if (targets.Count == 0)
        {
            WeedEnemy[] allEnemies = FindObjectsByType<WeedEnemy>();
            foreach (WeedEnemy enemy in allEnemies)
            {
                if (Vector2.Distance(attackPoint.position, enemy.transform.position) <= attackRange)
                    targets.Add(enemy);
            }
        }

        foreach (WeedEnemy enemy in targets)
            enemy.TakeDamage(finalDamage);

        lastHitCount = targets.Count;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
