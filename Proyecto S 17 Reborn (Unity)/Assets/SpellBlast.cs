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
                    Debug.Log("Enemy");
                    Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(50f);
                }
            }
        }

        Destroy(this.gameObject, 3f);
    }
}
