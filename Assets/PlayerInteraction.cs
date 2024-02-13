using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerNeeds playerNeeds;
    public float Hunger;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //get player currentHunger
        PlayerNeeds playerNeeds = GetComponent<PlayerNeeds>();
        Hunger = playerNeeds.currentHunger;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
        {
            
        }
        Debug.DrawRay(transform.position, transform.forward * 1f, Color.blue);
    }
}
