using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColeccionableController : MonoBehaviour
{
    //----Al declarar la variable puntos como estatica,
    //hacemos que pertenezca a todos los diamantes-----------
    public static int points = 0;
    public bool playerIsWinner = false;
    //Guardamos el Script del player en una variable para que
    //podamos acceder a todos los metodos y variables publicas de dicho script;
    //ya que será el script del Player el que contenga el metodo encargado de reproducir
    //el sonido correspondiente al coger un diamante.
    //----------------------------------------
    private PlayerController playerScript;
    private EnemyCreatorController enemyCreatorController_Script;
    public GameObject panelWin;
    //---------------------------------------
    private void Start()
    {
        playerScript = FindObjectOfType<PlayerController>();
        enemyCreatorController_Script= FindObjectOfType<EnemyCreatorController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
        //----------------------------------------
            if (tag=="5Points") { 
            points = points + 5;
            Debug.Log("Puntos = " + points+"/100");


                    playerScript.pointsTxt.text = "POINTS: " + points.ToString() + "/100";
                    playerScript.diamondSoundPlayer();
                    Destroy(gameObject);
                
            }
        //-------------------------------------------
            if (tag == "10Points")
            {
                points = points + 10;
                Debug.Log("Puntos = " + points+"/100");


                playerScript.pointsTxt.text = "POINTS: " + points.ToString() + "/100";
                playerScript.diamondSoundPlayer();
                    Destroy(gameObject);
                
            }
            //-------------------------------------------
            if (tag == "20Points")
            {
                points = points + 20;
                Debug.Log("Puntos = " + points+"/100");


                playerScript.pointsTxt.text = "POINTS: " + points.ToString() + "/100";
                playerScript.diamondSoundPlayer();
                    Destroy(gameObject);
                
            }
            //----------EL PLAYER GANA-----------------------------
            playerWIN();
        }
            
     }
    
    //Resetear puntuación
    public void resetPoints() {
        points = 0;
    }

    private void playerWIN() {
        if (points==100) {
            playerScript.celebracionPlayerWin();
            playerIsWinner = true;
            //--------------------------------------
            playerScript.pointsResultadoFinalTxt_win.text = "POINTS: " + points.ToString() + "/100";
            panelWin.SetActive(true);
            playerScript.hud.SetActive(false);
            playerScript.androidControllers.SetActive(false);
            //---------------GUARDAMOS LA PUNTUACIÓN---------------------------
            ControllerPersistentData.savePoints(points,System.DateTime.Now);
            Debug.Log("Puntos guardados");

        }
    }
}
