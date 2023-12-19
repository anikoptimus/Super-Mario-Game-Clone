using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrideEgg : MonoBehaviour
{
    void OnCollisionEnter2D (Collision2D target)
    {
        if(target.gameObject.tag == MyTag.PLAYER_TAG)
        {
            // player death
        }
        gameObject.SetActive(false) ;
    }
}
