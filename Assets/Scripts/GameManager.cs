using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField]private Rigidbody ballrb;
    [SerializeField] private GameObject panel;
    public static bool startflag = false;
    public Vector3 forcedirection;
    public float power;
    void Start()
    {
        #region PlatformSpwaner
        float forpos = 0;
        for (int i = 0; i < 20; i++) {

            //Randomly Generates the platform in the Game

            float horpos = Random.Range(-2.6f, 2.6f);
            Vector3 pos = new Vector3(horpos,0,forpos + 6.1f);
            Instantiate(panel, pos, Quaternion.identity);   
            forpos += 6.1f;
        }

        #endregion
    }


    void Update()
    {
        //Press 'R' to Restart the Game
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
            startflag = false;
        }
        //Click 'Left Mouse Button' to start the Hop Of the Ball
        if (!startflag) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                ballrb.AddForce(forcedirection * power, ForceMode.Force);
                startflag = true;
            }
        }
    }
}
