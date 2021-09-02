using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    [SerializeField]
    private Transform tempParent;

    private Mouse mouse;

    private Transform[] children;

    private void Start()
    {
        mouse = Mouse.current;
        children = new Transform[transform.childCount];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = mouse.position.ReadValue();
        DisconnectChildren();
        transform.position = newPosition;
        ConnectChildren();
    }

    private void DisconnectChildren()
    {
        for(int i = 0; i < children.Length; i++)
        {
            Transform child = transform.GetChild(i);

            children[i] = child;
            child.parent = tempParent;
        }
    }

    private void ConnectChildren()
    {
        for(int i = 0; i < children.Length; i++)
        {
            children[i].parent = transform;
        }
    }
}
