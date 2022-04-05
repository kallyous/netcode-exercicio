using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] public float move_speed;
    
    public NetworkVariable<Vector3> net_position = new NetworkVariable<Vector3>();
    public NetworkVariable<Quaternion> net_rotation = new NetworkVariable<Quaternion>();

    private void FixedUpdate()
    {
        // O dono calcula seu movimento e então atualiza sua posição na variavel de rede.
        if (IsOwner)
        {
            // Multiplicação só pode ser feita durante a criação de vetor novo, com "new Vector3() * x".
            var move =
                new Vector3(
                    Input.GetAxis("Horizontal"),
                    0f,
                    Input.GetAxis("Vertical")
                ).normalized * move_speed;

            transform.position += move;
            net_position.Value = transform.position;
            net_rotation.Value = transform.rotation;
        }
        // As instâncias dos jogadores replicados sincronizam com a variável de rede.
        else
        {
            transform.position = net_position.Value;
            transform.rotation = net_rotation.Value;
        }
    }
}
