﻿using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockObject : MonoBehaviour
{
    public BlockType type;

    public Material blueMaterial;
    public Material redMaterial;
    public Material yellowMaterial;
    public Material greenMaterial;

    public bool isSelected = false;

    public bool isDeleting = false;

    private new Renderer renderer;

    private Queue<Vector3> moveQueue = new Queue<Vector3>();

    public void MoveTo(Vector3 target, int frame)
    {
        var current = gameObject.transform.position;

        if (frame == 0)
        {
            gameObject.transform.position = target;
            return;
        }

        for (var i = 0; i < frame; ++i)
        {
            moveQueue.Enqueue((target - current) / frame);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case BlockType.NONE:
                renderer.material.color = new Color(0, 0, 0, 0.0f);
                break;
            case BlockType.RED:
                renderer.material = this.redMaterial;
                break;
            case BlockType.BLUE:
                renderer.material = this.blueMaterial;
                break;
            case BlockType.GREEN:
                renderer.material = this.greenMaterial;
                break;
            case BlockType.YELLOW:
                renderer.material = this.yellowMaterial;
                break;
        }

        if (moveQueue.Count > 0)
        {
            var delta = moveQueue.Dequeue();
            gameObject.transform.Translate(delta);
        }

        if (type != BlockType.NONE)
        {
            if (isSelected)
            {
                renderer.material.color = new Color(1.0f, 0.5f, 0.5f, 1);
            }
            else
            {
                renderer.material.color = new Color(1, 1, 1, 1);
            }
        }

        if (isDeleting)
        {
            renderer.material.color = new Color(0, 0, 0.5f, 1);
        }
    }
}
