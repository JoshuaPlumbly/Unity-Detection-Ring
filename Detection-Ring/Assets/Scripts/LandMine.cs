using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : Explosive
{
    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }
}