using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreatorController : MonoBehaviour
{
    //-------------------------
    public GameObject enemyPrefab;
    private GameObject enemy;
    private static int num_enemies=7;
    //--------------------------------
    private static ArrayList enemies = new ArrayList();
    //------------------------------------------
    private Vector3 posicionDelEnemyDestruido;
    //-------------------------------------------
    void Start()
    {
        //------------------------------------------------------------------
        enemyCreator();
        //--------------------------------------------------------------------
        InvokeRepeating("enemyReCreator", 30,60);
       
    }

    private void enemyReCreator()
    {
        if (num_enemies < 7)
        {
            enemy = Instantiate(enemyPrefab, posicionDelEnemyDestruido, Quaternion.identity);
            enemies.Add(enemy);
            num_enemies++;
        }

    }

    private void enemyCreator()
    {
        enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemies.Add(enemy);
    }


    public void crearEnemy_dondeSeDestruyo_ElUltimo(Vector3 vector) {
        num_enemies = num_enemies - 1;
        posicionDelEnemyDestruido = vector;
    }

}