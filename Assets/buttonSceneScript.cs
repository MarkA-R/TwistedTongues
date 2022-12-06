using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonSceneScript : MonoBehaviour
{
    public string sceneToSwitch;
    public void onClick()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(sceneToSwitch);
    }

}
