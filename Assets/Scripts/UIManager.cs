using TMPro;
using Unity.Netcode;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public TMP_Text playersOnline;
    [SerializeField] public TMP_Text debugFeedback;

    private void Update()
    {
        playersOnline.text = $"Jogadores online: {PlayersManager.Instance.players_online}";
    }

    public void StartHost()
    {
        if (NetworkManager.Singleton.StartHost())
        {
            debugFeedback.text = "Hospedeiro iniciado.";
        }
        else
        {
            debugFeedback.text = "Erro ao iniciar Hospedeiro.";
        }
    }
    
    public void StartClient()
    {
        if (NetworkManager.Singleton.StartClient())
        {
            debugFeedback.text = "Cliente iniciado.";
        }
        else
        {
            debugFeedback.text = "Erro ao iniciar cliente.";
        }
    }
    
    public void StartServer()
    {
        if (NetworkManager.Singleton.StartServer())
        {
            debugFeedback.text = "Servidor iniciado.";
        }
        else
        {
            debugFeedback.text = "Erro ao iniciar servidor.";
        }
    }
}
