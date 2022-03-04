using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    //--------------
    private static int vidas=3;
    //-------SPEED-------
    private float androidSpeed = 5f;
    private float windowsSpeed=5f;
    //-----------------------------
    private float jumpForce = 8F;
    private float movement_X;
    //--------------------------
    private Rigidbody2D rigidBody;
    private SpriteRenderer flipPlayer;
    //--------------------------------
    private bool isGround=true;
    public bool playerIsDead = false;
    //---------------------------------
    private AudioSource audioManager;
    public AudioClip diamondSoundCollect, jumpSound,attack,ohShit,chaching,celebracionMatar,ohComeOn,celebracionWin;
    public GameObject cameraPlayer;
    private Animator animator;
    public GameObject explosionPlayer;
    //----------------------------------
    public GameObject panelGameOver;
    public GameObject hud;
    //--------------ANDROID--------------------
    public GameObject androidControllers;
    //----------------------------------------
    public Text vidasTxt;
    public Text pointsTxt;
    public Text pointsResultadoFinalTxt_win;
    public Text pointsResultadoFinalTxt_loose;
    //------------------------------------
    private EnemyCreatorController enemyCreatorController_Script;
    private ColeccionableController coleccionableController_Script;
    //--------------
    //Metodo que se ejecuta al iniciar la aplicación
    void Start()
    {
        //--------------------------
        flipPlayer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        audioManager = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        //--------------------------------------
        enemyCreatorController_Script = FindObjectOfType<EnemyCreatorController>();
        coleccionableController_Script = FindObjectOfType<ColeccionableController>();
        //----------------------------------
    }

    // Metodo que se ejecuta por cada frame del juego
    void Update()
    {
        //--------WIDOWS-----------------
        movimiento_horizontal();
        salto();
        attackMode();
        //------ANDROID------------------
        //movimiento_horizontalAndroid();
        //---------------------------
        muertePorCaida();
        //-------------------------
    }
    ///////////////////////////////////////////////////////////////WINDOWS///////////////////////////////////////////////////////////////////
    void movimiento_horizontal() {
        if (playerIsDead == false)
        {
            movement_X = Input.GetAxis("Horizontal");
            //-----CONTROLAMOS SI EL MOVIMIENTO EN EL EJE X ES > O < QUE 0 PARA DARLE LA VUELTA AL PERSONAJE-----------
            //--------------------------------------------------
            //Obtenemos la componente transform del personaje para poder moverlo or el nivel
            //Al no poner el objeto, los componentes a los que se accede son
            //los del GameObject asignado a este script (this.transform.position)
            transform.position += new Vector3(movement_X, 0, 0) * windowsSpeed * Time.deltaTime;
            //Multiplicar por Time.deltaTime nos asegurará que el movimiento será fluido
            //independientemenete de la velocidad del microprocesador
            //Movimiento horizontal (x,y=0,z=0)
            //En Edit/Proyect Settings/Input Manager podemos ver la configuración de controles de Unity y nuestro proyecto
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                animator.SetBool("playerWalk", false);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                animator.SetBool("playerWalk", true);
            }

            if (movement_X > 0)
            {
                flipPlayer.flipX = false;
            }
            if (movement_X < 0)
            {
                flipPlayer.flipX = true;
            }
        }
    }

    void salto() {
        //movement_Y = Input.GetAxis("Vertical");
        //transform.position = transform.position+ new Vector3(0,movement_Y,0)*speed*Time.deltaTime;
        //Para hacer el controlador de salto, vamos a hacerlo a través del componente RigidBody
        //Si la tecla "Flecha hacia arriba" es pulsada y el personaje esta en el suelo....
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGround==true && playerIsDead==false) {
            //Añadimos una fuerza de impulso en dirección Y (Vector2.up) , en modo impulso (ForceMode2D.Impulse)
            rigidBody.AddForce(Vector2.up* jumpForce,ForceMode2D.Impulse);
            //Reproducimos el sonido correspondiente al saltar
            animator.SetBool("playerJump",true);
            jumpSoundPlayer();
        }

        
    }

    void attackMode()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Activamos la animacion de ataque
            animator.SetTrigger("playerAttack");
            //Activamos el sonido de ataque
            audioManager.PlayOneShot(attack);
        }
    }

    //////////////////////////////////////////////////////////////////ANDROID CONTROLS//////////////////////////////////////////////////////////////////

    public void saltoaAndroid()
    {
        //transform.position = transform.position+ new Vector3(0,movement_Y,0)*speed*Time.deltaTime;
        //Para hacer el controlador de salto, vamos a hacerlo a través del componente RigidBody
        //Si la tecla "Flecha hacia arriba" es pulsada y el personaje esta en el suelo....
        if (isGround == true && playerIsDead == false)
        {
            //Añadimos una fuerza de impulso en dirección Y (Vector2.up) , en modo impulso (ForceMode2D.Impulse)
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //Reproducimos el sonido correspondiente al saltar
            animator.SetBool("playerJump", true);
            jumpSoundPlayer();
        }
    }

    public void attackModeAndroid()
    {
        //Activamos la animacion de ataque
        animator.SetTrigger("playerAttack");
        //Activamos el sonido de ataque
        audioManager.PlayOneShot(attack);

    }

    private void movimiento_horizontalAndroid()
    {
        if (playerIsDead == false)
        {
            movement_X = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            //-----CONTROLAMOS SI EL MOVIMIENTO EN EL EJE X ES > O < QUE 0 PARA DARLE LA VUELTA AL PERSONAJE-----------
            //--------------------------------------------------------------------------------------------------------------
            //rigidBody.velocity = new Vector2(movement_X * androidSpeed * Time.deltaTime, rigidBody.velocity.y);
            /*if (rigidBody.velocity.x > 0|| rigidBody.velocity.x < 0) { animator.SetBool("playerWalk", true); }
            if (rigidBody.velocity.x == 0) { animator.SetBool("playerWalk", false); }*/
            //--------------------------------------------------
            transform.position += new Vector3(movement_X, 0, 0) * androidSpeed * Time.deltaTime;

            if (movement_X > 0)
            {
                flipPlayer.flipX = false;
                animator.SetBool("playerWalk", true);
            }
            if (movement_X < 0)
            {
                flipPlayer.flipX = true;
                animator.SetBool("playerWalk", true);
            }

            if (movement_X == 0) { animator.SetBool("playerWalk", false); }
        }

    }

    //--------------------------------------------------COMPORTAMIENTO COMÚN---------------------------------------------------------------------------

    //Metodos para controlar las colisiones del Player con el resto de objetos del juego--------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el Player colisiona con el suelo.....
        if (collision.gameObject.tag=="Ground") {
            isGround = true;
            animator.SetBool("playerJump", false);
        }

        //Si el Player entra en contacto con un enemigo
        if (collision.gameObject.tag=="Enemy") {
            //-----------------
            vidas -= 1;
            vidasTxt.text = vidas+"/3";
            //----------------------------------
            playerHerido();
            //-----------------
            if (vidas == 0)
            {
                //-----------------------------------------
                Debug.Log("Muerte del Player - Fin del juego");
                //---------Antes de destruir al Player hay que separarlo de la Main camera---------------
                //Siempre tiene que haber minimo 1 camara e el videojuego
                cameraPlayer.transform.parent = null;
                //-----Ahora si destruimos/matamos al Player e instanciamos el sistema de particulas correspondiente---------------
                //Instantiate(explosionMuerteDelPlayer, transform.position+new Vector3(0,0.3f,0), Quaternion.identity);
                //----Activamos el panel de Fin del Juego y desactivamos el Hud-------
                hud.SetActive(false);
                androidControllers.SetActive(false);
                pointsResultadoFinalTxt_loose.text = "POINTS: " + ColeccionableController.points.ToString() + "/100";
                panelGameOver.SetActive(true);
                //---------------GUARDAMOS LA PUNTUACIÓN---------------------------
                ControllerPersistentData.savePoints(ColeccionableController.points, System.DateTime.Now);
                Debug.Log("Puntos guardados");
                //---Matamos al Player------------------
                playerMuerto();
            }

            if (collision.gameObject.tag == "Enemy" && isGround==false && vidas==0) { 
                animator.SetBool("playerJump", false); playerMuerto(); }
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Si el Player deja de colisionar con el suelo.....
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }

    public void diamondSoundPlayer() {
        audioManager.PlayOneShot(diamondSoundCollect);
        audioManager.PlayOneShot(chaching);
    }

    public void celebracionPlayerMataEnemigo()
    {
        audioManager.PlayOneShot(celebracionMatar);
    }

    public void celebracionPlayerWin()
    {
        audioManager.PlayOneShot(celebracionWin);
    }

    private void jumpSoundPlayer() {
        audioManager.PlayOneShot(jumpSound);
    }

  

    public void restVidas() {
        vidas = 3;
    }

    private void playerHerido() {
        if (vidas >= 1)
        {
            animator.SetTrigger("playerHurt");
            audioManager.PlayOneShot(ohShit);
        }
    }

    private void playerMuerto()
    {
        playerIsDead = true;
        animator.SetTrigger("playerDead");
        audioManager.PlayOneShot(ohComeOn);
    }

    public void muertePorCaida()
    {
        if (transform.position.y <= (-21.4))
        {
            try {
                cameraPlayer.transform.parent = null;
                //----Activamos el panel de Fin del Juego y desactivamos el Hud-------
                hud.SetActive(false);
                androidControllers.SetActive(false);
                pointsResultadoFinalTxt_loose.text = "POINTS: " + ColeccionableController.points.ToString() + "/100";
                panelGameOver.SetActive(true);
                //---------------GUARDAMOS LA PUNTUACIÓN---------------------------
                ControllerPersistentData.savePoints(ColeccionableController.points, System.DateTime.Now);
                Debug.Log("Puntos guardados");
                //-------------------------------------------------------------------
                Instantiate(explosionPlayer,transform.position,Quaternion.identity);
                Destroy(gameObject);
            }
            catch (Exception e) {
                Debug.Log(e.Message);
            }
            
        }
    }

   

}
