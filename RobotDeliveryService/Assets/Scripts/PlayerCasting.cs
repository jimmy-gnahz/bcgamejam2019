using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    public static float Distancefromtarget;
    public float Totarget;
    // Update is called once per frame
    void Update()
    {
        RaycastHit Hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out Hit))
        {
            Totarget = Hit.distance;
            Distancefromtarget = Totarget;
        }
    }
}
