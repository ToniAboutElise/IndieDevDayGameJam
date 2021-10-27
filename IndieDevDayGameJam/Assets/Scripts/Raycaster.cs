using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("You selected the " + hit.transform.name);
                if(hit.transform.GetComponent<Chicken>() == true)
                {
                    hit.transform.GetComponent<Chicken>().ChickenHit();
                }
            }
        }
    }
}
