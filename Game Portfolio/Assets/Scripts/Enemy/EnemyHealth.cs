using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyInfo))]
public class EnemyHealth : MonoBehaviour
{
    EnemyInfo info;

    private void Start()
    {
        info = GetComponent<EnemyInfo>();
    }

    public void TakeDamage(int damage)
    {
        info.currentHp -= damage;

        if (isDead())
            Die();
    }

    private void Die()
    {
        GameobjectDestroyer.Instance.DestroyGO(gameObject);
    }

    private bool isDead() => info.currentHp <= 0;
}
