using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCleanup : MonoBehaviour
{
    private void Start() {
        Invoke(nameof(Cleanup), 10f);
    }

    private void Cleanup() {
        Destroy(gameObject);
    }
}
