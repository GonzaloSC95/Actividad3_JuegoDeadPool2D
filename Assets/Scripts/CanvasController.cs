using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    //---------------------
    private ColeccionableController coleccionableController_Script;
    private PlayerController playerController_Script;
    private ButtonSoundsManager buttonSoundsManager;
    private AudioSource audioManager;
    public AudioClip ohReally;
    //----------------------
    private void Start()
    {
        coleccionableController_Script = FindObjectOfType<ColeccionableController>();
        playerController_Script= FindObjectOfType<PlayerController>();
        audioManager = GetComponent<AudioSource>();
    }
    public void startGame()
    {
        audioManager.PlayOneShot(ohReally);
        //Resetamos la puntuación--------------
        coleccionableController_Script.resetPoints();
        playerController_Script.restVidas();
        //----------------------------------
        //SceneManager.LoadScene("Game");
        Invoke("loadSceneGame", 2f);
    }
    public void exitGame() {
        //Resetamos la puntuación--------------
        coleccionableController_Script.resetPoints();
        playerController_Script.restVidas();
        //----------------------------------
        SceneManager.LoadScene("Inicio");
        
    }

    private void loadSceneGame() {
        SceneManager.LoadScene("Game");
    }
}
