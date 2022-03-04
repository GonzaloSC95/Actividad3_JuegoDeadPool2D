using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    //---------------------
    public Transform destino;
    private float speed=2f;
    private Vector3 start, end;
    //--------------------------
    //Metodo que se ejecuta al iniciar la aplicación
    void Start()
    {
        start = transform.position;
        end = destino.position;
    }

    //Metodo que se ejecuta por cada frame del juego
    void Update()
    {
        //Movemos la posición de la plataforma del origen al destino a la velocidad indicada (speed)
        transform.position = Vector3.MoveTowards(transform.position,destino.position,speed*Time.deltaTime);
        //----Si la plataforma ha legado a su destino---------
        if (transform.position==destino.position) {
            //----Si la plataforma esta arriba---------
            if (destino.position == end)
            {
                destino.position = start;
            }
            else {
                destino.position = end;
            }
        }

    }

    //Cuando el player y la plataforma entren en contacto,
    //el player pasará a ser hijo (jerarquia) de la plataforma.
    //De esta forma el movimiento de ambos estará sincronizado.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.parent = transform;
    }

    //Cuando el player y la plataforma dejen de estar en contacto,
    //el player dejará de ser hijo (jerarquia) de la plataforma.
    //De esta forma su movimiento volverá a ser independiente.
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
    }
}
