using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class healthScript : MonoBehaviour
{

    public static healthScript instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount == 0)
        {
            //game over menu
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Death");
        }
    }

    public void removeHealth()
    {
        

        Destroy(transform.GetChild(transform.childCount - 1).gameObject);


        

        //Destroy(transform.GetChild(transform.childCount - 1).GetComponent<RawImage>());
        
    }
}
