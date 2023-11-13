using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public int choosenSkinId;
    public GameObject player;
    public Vector3 pointSpawn = new Vector3(0, 0, 0);

    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        player = GameObject.FindGameObjectWithTag("Player");
        base.Awake();
        if (pointSpawn == null)
            pointSpawn = player.transform.position;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if(player == null)
        {
            Instantiate(player, pointSpawn, Quaternion.identity);
        }

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
