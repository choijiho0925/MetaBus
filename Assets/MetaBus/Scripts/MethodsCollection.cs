using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MethodsCollection : MonoBehaviour
{
    [SerializeField] private GameObject gameDescriptionPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        FlapPlaneManager.IsStart = true;
        //DontDestroyOnLoad();
    }

    public void JumpMap()
    {
        SceneManager.LoadScene(2);
    }
}
