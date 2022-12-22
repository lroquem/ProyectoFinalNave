using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlnave : MonoBehaviour
{
    // Start is called before the first frame update
    float velocity_x = 0F;
    float velocity_y = 0.70F;
    const float CTETRUEPCT = 15000F;
    const float CTEOIL = 5000F;
    public const int CTELIFE = 5;
    float truepercent = CTETRUEPCT;
    float addoil = CTEOIL;
    bool flag=false;
    public static int life = CTELIFE;
    Rigidbody rigidbody;
    Transform transform;
    AudioSource audiosource;
    Scene sm_Scene;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (truepercent>=0)
            print("Combustible : "+ (int)(truepercent/(CTETRUEPCT/100)) + " Vidas : "+life);
        else
            print("Combustible : "+ 0 + " Vidas : "+life);
        ProcesarInput();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) {
            case "Colision segura":
                {
                sm_Scene = SceneManager.GetActiveScene();
                    switch(sm_Scene.name){
                        case "nivel 1":
                        SceneManager.LoadScene("nivel 2");
                        break;
                        case "nivel 2":
                        SceneManager.LoadScene("nivel 3");
                        break;
                        case "nivel 3":
                        SceneManager.LoadScene("nivel 4");
                        break;
                        case "nivel 4":
                        SceneManager.LoadScene("nivel 5");
                        break;
                        case "nivel 5":
                        SceneManager.LoadScene("menu");
                        break;
                    }
                }
                break;
            case "Colision peligrosa":
                {
                if (flag == false)
                    life = life -1;
                flag = true;
                sm_Scene = SceneManager.GetActiveScene();
                if (life <= 0){
                    SceneManager.LoadScene("menu");
                    break;
                }
                
                    switch(sm_Scene.name){
                        case "nivel 1":
                        SceneManager.LoadScene("nivel 1");
                        break;
                        case "nivel 2":
                        SceneManager.LoadScene("nivel 2");
                        break;
                        case "nivel 3":
                        SceneManager.LoadScene("nivel 3");
                        break;
                        case "nivel 4":
                        SceneManager.LoadScene("nivel 4");
                        break;
                        case "nivel 5":
                        SceneManager.LoadScene("nivel 5");
                        break;
                    }
                }
                break;
            case "Recarga":
                {
                    if (flag == false)
                        truepercent=truepercent+addoil;
                    flag = true;
                    Destroy(collision.gameObject);
                }
                break;
            case "Colision despegue":
                {
                if (truepercent<=0){
                    if (flag == false)
                        life = life -1;
                    flag = true;
                    sm_Scene = SceneManager.GetActiveScene();
                    if (life <= 0){
                        SceneManager.LoadScene("menu");
                        break;
                        }
                    SceneManager.LoadScene(sm_Scene.name);
                    }
                }
                break;
        }
    }

    private void ProcesarInput()
    {

        Propulsion();
        Rotacion();
    }

    private void Propulsion()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            truepercent=truepercent - 20F;
            flag=false;
            rigidbody.freezeRotation = true;
            //print("propulsor....");
            if (Input.GetKey(KeyCode.D) && (truepercent>0))
                velocity_x=velocity_x+0.02F;
            if (Input.GetKey(KeyCode.A) && (truepercent>0))
                velocity_x=velocity_x-0.02F;
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                velocity_x=0F;
            if (truepercent>0){
                rigidbody.AddRelativeForce(velocity_x,velocity_y,0);
                }
            else
                audiosource.Stop();
            if (!audiosource.isPlaying && truepercent>0)
            {
                audiosource.Play();
            }

        }
        else
        {
            audiosource.Stop();
        }
        //rigidbody.freezeRotation = false;
    }

    private void Rotacion()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //print("rotar derecha....");
            // transform.Rotate(-Vector3.forward);
            var rotarDerecha = transform.rotation;
            rotarDerecha.z -= Time.deltaTime * 0.24f;
            transform.rotation = rotarDerecha;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            //print("rotar izquierda....");
            //transform.Rotate(Vector3.forward);
            var rotarIzquierda = transform.rotation;
            rotarIzquierda.z += Time.deltaTime * 0.24f;
            transform.rotation = rotarIzquierda;

        }
    }
}
