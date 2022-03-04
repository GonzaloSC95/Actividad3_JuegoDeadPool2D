using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPlayerController : MonoBehaviour
{
    //-----------------------
    public GameObject explosionMuerteDelEnemy;
    private GameObject ref_explosionMuerteDelEnemy;
    private EnemyCreatorController enemyCreatorController_Script;
    private PlayerController playerControllerr_Script;
    //------------------------
    private void Start()
    {
        enemyCreatorController_Script = FindObjectOfType<EnemyCreatorController>();
        playerControllerr_Script = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Enemy") {
            //-----Ahora si destruimos/matamos al Enemy e instanciamos el sistema de particulas correspondiente---------------
            ref_explosionMuerteDelEnemy=Instantiate(explosionMuerteDelEnemy, collision.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
            Destroy(collision.gameObject);
            playerControllerr_Script.celebracionPlayerMataEnemigo();
            enemyCreatorController_Script.crearEnemy_dondeSeDestruyo_ElUltimo(collision.transform.position);
            Destroy(ref_explosionMuerteDelEnemy.gameObject,3f);
        }
    }
}
