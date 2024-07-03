using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    private float inputH;

    [Header("Para el movimiento")]
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private float fuerzaSalto = 5f;
    
    //Para identificar el suelo bajo los pies
    [SerializeField] private LayerMask queEsSaltable;
    [SerializeField] private Transform puntoPies;
    

    [Header("Sistema de combate")]
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float danhoAtaque;

    // Layer para detectar los triggers afectables
    [SerializeField] private LayerMask queEsDanhable;

    private bool deathAnimIsPlayed;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (TryGetComponent(out HealthSystem health))
            health.delIsDead += MuerteJugador;

        deathAnimIsPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Menu Pausa
        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.instance.PauseGame();

        Movimiento();

        //Para el salto
        Salto();

        LanzarAtaque();
    }

    private void LanzarAtaque()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
    }

    // Para esconderse detrás de un escondite
    

    //Se llama desde el evento en la animación del jugador
    private void Ataque()
    {
        //Lanzar Trigger instantaneo que recoge los colliders pillados
        Collider2D [] collidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanhable);
        foreach(Collider2D collider in collidersTocados)
        {
            if(collider.gameObject != gameObject 
               && collider.gameObject.TryGetComponent(out IHitableElement hitableElement))
            {
                hitableElement.GetDamage(danhoAtaque);
            }
        }
    }

    private void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstoyEnSuelo())
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
    }

    private bool EstoyEnSuelo()
    {
        //Para comprobar las variables, dibujar una linea que dure 0.3 segundos
        //Debug.DrawRay(puntoPies.position, Vector3.down, Color.red, 0.3f);

        return Physics2D.Raycast(puntoPies.position, Vector2.down, 0.15f, queEsSaltable);
    }
    
    private void Movimiento()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        
        // En vez de utilizar las fuerzas del rigidbody, usaremos el velocity
        // para el movimiento horizontal
        // Hay que mantener la velocidad vertical para no cargarnos la gravedad
        rb.velocity = new Vector2(inputH * velocidadMovimiento, rb.velocity.y);
        if (inputH != 0)
        {
            anim.SetBool("running", true);
            if (inputH > 0)
                transform.eulerAngles = Vector3.zero;
            else
                transform.eulerAngles = new Vector3(0, 180, 0);

        }
        else
            anim.SetBool("running", false);
    }

    public void MuerteJugador()
    {
        if (!deathAnimIsPlayed)
        {
            deathAnimIsPlayed = true;
            anim.SetTrigger("muerte");
        }
    }
    public void DeathAnimationIsFinished()
    {
        //Debug.Log("Se murio el jugador");
        GameManager.instance.PlayerIsDead();
        Destroy(gameObject);
    }


    /*private void OnDrawGizmos()
    {
        //Para dibujar la esfera del overlap en el ataque
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }
    */

}
