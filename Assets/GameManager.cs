using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.tag== "scissor")
                {
                    Debug.Log("click scissor");
                    SceneManager.LoadScene("SampleScene 2");
                }

                if (hit.collider.tag == "key")
                {
                    Debug.Log("click key");
                    SceneManager.LoadScene("SampleScene 1");
                }
            }
        }
        
    }
}
