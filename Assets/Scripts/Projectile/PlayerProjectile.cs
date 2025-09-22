using System;
using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    private Vector3 direction;
    [SerializeField] private float projectileSpeed = 1f;
    private int projectileDamage;
    private void FixedUpdate() {
        transform.Translate(direction * (projectileSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Health>()) {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth.isPlayer) {
                return;
            }
            enemyHealth.TakeDamage(projectileDamage);
        } 
        Destroy(gameObject);
    }

    public void Initialize(Vector3 dir, int gunDamage) {
        direction = dir;
        projectileDamage = gunDamage;
    }
}
