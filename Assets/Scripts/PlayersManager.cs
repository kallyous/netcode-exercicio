using Unity.Netcode;
using UnityEngine;

public class PlayersManager : NetworkBehaviour
{
    // Singleton, configuração.
    public static PlayersManager Instance { get; private set; }
    
    // Variável de rede, contador de jogadores online.
    public NetworkVariable<int> net_players_online = new NetworkVariable<int>(0);
    
    // Propriedade, read-only getter para o contador de jogadores online.
    public int players_online => net_players_online.Value;
    
    // Singleton, inicialização.
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Configuração, adicionamos algumas funções na lista de callbacks de conexão.
    private void Start()
    {
        // Adiciona à lista de callbacks de conexão de clientes uma função anônima pra incrementar contador.
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            // Quem rastreia jogadores conectados é o servidor.
            if (IsServer)
            {
                net_players_online.Value++;
                Debug.Log($"Jogador {id} conectou.");
            }
        };
        
        // Adiciona à lista de callbacks de conexão de clientes uma função anônima pra decremetnar contador.
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            // Quem rastreia jogadores conectados é o servidor.
            if (IsServer)
            {
                net_players_online.Value--;
                Debug.Log($"Jogador {id} desconectou.");
            }
        };
    }
}
