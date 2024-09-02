using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ENEMY"))
        {
            var child = collision.gameObject.transform.GetChild(1);
            collision.gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<NumberChecker>().GetShootObjectValue();
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(child.gameObject);
            Destroy(collision.gameObject, 1.2f);
            Destroy(gameObject);


        }
    }

}
