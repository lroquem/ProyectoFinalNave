using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlmenu : MonoBehaviour
{
    void Start(){
    controlnave.life=controlnave.CTELIFE;
    }
    void Update(){}

    public void empezar(){
        SceneManager.LoadScene("nivel 1");
    }

    public void salir(){
        Application.Quit();
    }
}
