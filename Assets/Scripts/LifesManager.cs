using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using UnityEngine.UI;

public class LifesManager : NetworkBehaviour
{
    [SerializeField]
    private int lifes;

    [SerializeField]
    private static Text lifesText;

    [SerializeField]
    GameObject playerLifesText;

    private void Awake()
    {                                                                           
        playerLifesText = GameObject.FindGameObjectWithTag("PlayerLifes");      
        lifesText = playerLifesText.GetComponent<Text>();                       // Obtengo el componente de texto de las vidas mediante un tag para modificarlo luego.
    }
    
    [ClientRpc]
    void UpdateHealthClientRpc(int lifes)                                       // Notifica al cliente su vida actual.
    {
        this.lifes = lifes;
    }

    void OnCollisionEnter(Collision collision)                                  // Las colisiones se calculan en el servidor.
    {                                                                           // Cuando un jugador colisiona con otra cosa que no sea el suelo, se le resta una vida
                                                                                // y el servidor avisa al cliente de que su vida ha cambiado.
        if (collision.gameObject.tag != "Floor" && IsHost)                      
        {
            lifes--;         
            UpdateHealthClientRpc(lifes);
        }
    }

    void Update()
    {                                                                            
        if (IsLocalPlayer)                                                      // Actualizo el texto de vidas del jugador local.
        {                                                                       
            lifesText.text = "Vidas: " + lifes;                                 
        }
                                                                                
        if (lifes <= 0)                                                         // Cuando un jugador se queda sin vidas, es eliminado.
        {
            this.gameObject.SetActive(false);                                   // Desactivo al jugador.
        }
    }
}