using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //---------------------------------
    private Transform playerPosition;
    private float speed = 2f;
    private SpriteRenderer flipEnemy;
    private Animator animatorEnemy;
    private PlayerController playerController_Script;
    private ColeccionableController coleccionableController_Script;
    //----------------------------------
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        flipEnemy = GetComponent<SpriteRenderer>();
        animatorEnemy = GetComponent<Animator>();
        playerController_Script = FindObjectOfType<PlayerController>();
        coleccionableController_Script = FindObjectOfType<ColeccionableController>();
    }

    // Update is called once per frame
    void Update()
    {
        //-------------------------------------------------------------------------
        float distanciaX_player_enemy = Mathf.Abs(playerPosition.position.x - transform.position.x);
        float distanciaY_player_enemy = Mathf.Abs(playerPosition.position.y - transform.position.y);
        //----------------------------------------------------------------------
        if (distanciaX_player_enemy < 30 
            && distanciaY_player_enemy < 2 
            && playerController_Script.playerIsDead==false
            &&coleccionableController_Script.playerIsWinner==false)
        {
            animatorEnemy.SetBool("run_playerVisualizado",true);
            Debug.Log("La distancia entre el enemigo y el player es menor que 30");
            //Hacemos que el enemigo se desplace constantemente desde su posición hacia la posción en la que se encuentra el Player
            transform.position = Vector3.MoveTowards(transform.position, playerPosition.position, speed * Time.deltaTime);
            //Si la posición del enemigo en el eje x, es menor que la del player, cambiamos el flip x a true;
            if (transform.position.x < playerPosition.position.x)
            {
                flipEnemy.flipX = true;
            }
            else
            {
                flipEnemy.flipX = false;
            }
        }
        else {
            animatorEnemy.SetBool("run_playerVisualizado", false);
        }
    }
}
