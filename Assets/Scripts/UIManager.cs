using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button StartServerButton;

    [SerializeField]
    private Button StartHostButton;

    [SerializeField]
    private Button StartClientButton;

    [SerializeField]
    private Text playersInGameText;

    [SerializeField]
    private Button CheckButton;

    private void Awake()
    {
        Cursor.visible = true;
    }

    public void Update()
    {
        playersInGameText.text = $"Players in game: {PlayersManager.Instance.PlayersInGame}";
    }

    public void Start()
    {
        StartHostButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Host arrancado");
            } else
            {
                Debug.Log("Horror: Host no pudo arrancar");
            }
        });
        StartServerButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartServer())
            {
                Debug.Log("Server arrancado");
            }
            else
            {
                Debug.Log("Horror: Server no pudo arrancar");
            }
        });
        StartClientButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient())
            {
                Debug.Log("Client arrancado");
            }
            else
            {
                Debug.Log("Horror: Client no pudo arrancar");
            }
        });

        CheckButton.onClick.AddListener(() =>
        {
            Debug.Log(FindObjectOfType<PlayersManager>());
            
        });

    }
}
