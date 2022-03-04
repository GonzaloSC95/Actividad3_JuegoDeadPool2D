using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicioController : MonoBehaviour
{
    public Text puntuaciones;
    public GameObject panelpuntuaciones;
    private ButtonSoundsManager buttonSoundsManager;
    public AudioClip startGame_mp3,exitGame_mp3,showPoints_mp3,borrarPuntos_mp3;

    private void Start()
    {
        buttonSoundsManager = FindObjectOfType<ButtonSoundsManager>();
    }
    //------------------------------------------------------------------------------------
    public void startGame()
    {
        buttonSoundsManager.PlayButtonSound(startGame_mp3);
        Invoke("loadSceneGame",2f);
        //SceneManager.LoadScene("Game");
    }

    public void exitGame() {
        buttonSoundsManager.PlayButtonSound(exitGame_mp3);
        Invoke("exitAplication", 3f);
    }

    public void showPoints() {
        puntuaciones.text = ControllerPersistentData.loadPoints();
        buttonSoundsManager.PlayButtonSound(showPoints_mp3);
        panelpuntuaciones.SetActive(true);
    
    }

    public void backMenu()
    {
        panelpuntuaciones.SetActive(false);
    }

    public void borrarPuntuaciones() {
        buttonSoundsManager.PlayButtonSound(borrarPuntos_mp3);
        puntuaciones.text = "No hay puntuaciones guardadas.";
        ControllerPersistentData.resetPoints();
    }
    //-------------------------------------------------------------------------------
    private void loadSceneGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void exitAplication() {
        Application.Quit();
    }

}
