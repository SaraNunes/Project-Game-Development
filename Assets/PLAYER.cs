using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PLAYER : MonoBehaviour
{
    public float life = 100;
    public string scene;
    public float speed = 0.3f;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        transform.tag = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        float movHor = Input.GetAxis("Horizontal");
        float movVer = Input.GetAxis("Vertical");
        transform.position += new Vector3(movHor * 0.1f, 0, movVer * speed);
        if (Input.GetKeyDown(KeyCode.Space))
        {

            transform.position += new Vector3(0, 5, 0);

        }
        if(life <=50)
        {
            speed = 0.6f;
            Debug.Log("Alert state");
        }
            if (life <= 0)
            {
                Death();
            }
        }

        void Death()
        {
            SceneManager.LoadScene(scene);
        }
    }
    
