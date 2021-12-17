using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieMarc.EnemyVision;

public class SpellBlast : MonoBehaviour
{
    [SerializeField] private SphereCollider _sphereCollider;

    public void Start()
    {
        Collider[] touching = Physics.OverlapSphere(_sphereCollider.bounds.center, 3);

        foreach (var collider in touching)
        {
            if (collider.gameObject != _sphereCollider)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(50f);
                    }

                    BossTurret boss = collider.gameObject.GetComponent<BossTurret>();
                    if (boss != null)
                    {
                        boss.TakeDamage(50f);
                    }
                }
            }
        }

        Destroy(this.gameObject, 3f);
    }
}
