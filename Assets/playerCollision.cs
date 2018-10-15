﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollision : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            FighterController.instance.react();
            Debug.Log("Enemy HIT");
        }
    }
}
