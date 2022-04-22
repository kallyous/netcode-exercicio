using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerControlV2 : NetworkBehaviour
{
    public float move_speed = 0.1f;
    public Vector3 m_move_vector;

    private float prev_mov_x = 0;
    private float prev_mov_y = 0;

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            //UpdateServer();
        }

        if (IsClient)
        {
            UpdateClient();
        }
    }
    
    void UpdateClient()
    {
        var curr_x = Input.GetAxis("Horizontal");
        var curr_y = Input.GetAxis("Vertical");

        if (prev_mov_x != curr_x || prev_mov_y != curr_y)
        {
            SetMovementServerRpc(curr_x, curr_y);
            prev_mov_x = curr_x;
            prev_mov_y = curr_y;
        }
    }

    private void FixedUpdate()
    {
        if (IsServer)
        {
            FixedUpdateServer();
        }
    }

    void FixedUpdateServer()
    {
        transform.position += m_move_vector;
    }

    [ServerRpc]
    public void SetMovementServerRpc(float x, float y)
    {
        // Multiplicação só pode ser feita durante a criação de vetor novo, com "new Vector3() * x".
        var move = new Vector3(x, 0f, y).normalized * move_speed;
        m_move_vector = move;
    }
}
