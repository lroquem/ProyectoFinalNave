using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlmenu : MonoBehaviour
{
    void Start(){}
    void Update(){}

    public void empezar(){
        SceneManager.LoadScene("nivel 1");
    }

    public void salir(){
        Application.Quit();
    }
}
