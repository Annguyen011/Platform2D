using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Player player;
    public Vector3 pointSpawn = new Vector3(0,0,0);

    protected override void Awake()
    {
        base.Awake();
        if (pointSpawn == null)
            pointSpawn = player.transform.position;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
            ResetPlayer();
    }

    public void ResetPlayer()
    {
        player.transform.position = pointSpawn;
    }

    public void ChangePointSpawn(Vector3 pos)
    {
        pointSpawn = pos;
    }
}
