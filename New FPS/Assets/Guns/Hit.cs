﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public float damage = 10f;
    public float knockback = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 15);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy" ) {
            Target enemy = other.GetComponent<Target>();
            Enemy_Chase en = other.GetComponent<Enemy_Chase>();
            if (enemy != null) {
                // en.isStunned = true;
                enemy.TakeDamage(damage);
                Rigidbody enemybody = other.gameObject.GetComponent<Rigidbody>();
                enemybody.AddForce(this.transform.forward * knockback);
            }
        }
        
        Destroy(gameObject);
    }
}
