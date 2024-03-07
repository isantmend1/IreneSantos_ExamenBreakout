using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    /*
    Los límites definidos con bound nos hacen falta debido a que el jugador se puede salir de la pantalla
    debido a que su rigidbody es quinemático, por lo que no se ve afectado por la gravedad ni puede colisionar
    con objetos estáticos.
    */
    [SerializeField] private float bound = 2.5f; // x axis bound 

    private Vector2 startPos; // Posición inicial del jugador
    private float sizeX;
    private float sizeY;
    private float sizeZ;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; // Guardamos la posición inicial del jugador
        sizeX = gameObject.transform.localScale.x;
        sizeY = gameObject.transform.localScale.y;
        sizeZ = gameObject.transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
       PlayerMovement();
    }

    void PlayerMovement()
    {
         float moveInput = Input.GetAxisRaw("Horizontal");
        // Controlaríamo el movimiento de la siguiente forma de no ser el rigidbody quinemático
        // transform.position += new Vector3(moveInput * speed * Time.deltaTime, 0f, 0f);

        Vector2 playerPosition = transform.position;
        // Mathf.Clamp nos permite limitar un valor entre un mínimo y un máximo
        playerPosition.x = Mathf.Clamp(playerPosition.x + moveInput * speed * Time.deltaTime, -bound, bound);
        transform.position = playerPosition;
    }

    public void ResetPlayer()
    {
        transform.position = startPos; // Posición inicial del jugador
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("powerUp")) // Si colisionamos con un powerUp
        {
            Destroy(collision.gameObject); // Lo destruimos
            GameManager.Instance.AddLife(); // Añadimos una vida
        }
        else if (collision.CompareTag("NewPowerUp"))
        {
            GameManager.Instance.LoseLifePowerUp();
            Destroy(collision.gameObject); // Lo destruimos
        } 
        else if (collision.CompareTag("powerUpPlataforma"))
        {
            bound = 0.5f;
            AumentarPlataforma();
            Destroy(collision.gameObject); // Lo destruimos
            bound = 2.5f;
        }
    }

    private void AumentarPlataforma()
    {        
        if(sizeX <= (sizeX * 2))
        {
            Player player = GameObject.FindFirstObjectByType<Player>();
            player.transform.localScale = new Vector3(sizeX * 2, sizeY, sizeZ);
        }        
    }

    

    /* private void Contador()
     {
         if (segundos > 0)
         {
             segundos -= Time.deltaTime;
             int segundosRestantes = Mathf.RoundToInt(segundos); // Redondea los segundos restantes
         }
         segundos = 10;
     }*/
}
