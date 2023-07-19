using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    /*timeoffSet permet un décalage au niveau du suivi de la caméra ... + val grad + caméra mettra longtemps à se recentré sur le perso*/
    public Vector3 posOffset;
    /*posOffset permet un décalage de la cam (horizontal verticale ou en zoom 
     * prend -10 en valeur Z pour prendre du recule sinon on voit rien ... */

    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
    }
}
