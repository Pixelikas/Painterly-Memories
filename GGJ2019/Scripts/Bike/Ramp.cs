using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2019.Bike
{
    [RequireComponent(typeof(Collider2D))]
    public class Ramp : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<Movable>().Jump();
            }
        }
    }
}