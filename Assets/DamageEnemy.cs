using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("ENEMY"))
        {

            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }
}
