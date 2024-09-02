using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberChecker : MonoBehaviour
{
    [SerializeField] public int value;
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private GameObject healthCounter;
    public void Awake()
    {
       
        valueText.text = value.ToString();
    }
    public void GetShootObjectValue()
    {

        if (value % 2 == 0)
        {

            Debug.Log("even");
            
        }
        else
        {
            Debug.Log("Odd");
            ReduceHealth();
            
        }


    }

    public void ReduceHealth()
    {
       
        if(healthCounter.transform.childCount > 0)
        {
            Destroy(healthCounter.transform.GetChild(0).gameObject);

        }

       


    }

   


}
