using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieMarc.EnemyVision
{
    /// <summary>
    /// Editor only object (invisible at runtime)
    /// </summary>
    
    public class Invisible : MonoBehaviour
    {
        [SerializeField] private SphereCollider _sphere;

        private void Awake()
        {
            MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
            _sphere = GetComponent<SphereCollider>();
            if (mesh != null)
                mesh.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy") && _sphere != null)
            {
                _sphere.enabled = true;
            }

        }

    }
}
