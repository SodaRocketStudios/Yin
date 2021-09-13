using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    [SerializeField]
    private bool infiniteHorizontal = true;

    [SerializeField]
    private bool infiniteVertical = false;

    private Transform cameraTransform;
    private Vector2 textureUnitSize;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSize.x = texture.width / sprite.pixelsPerUnit;
        textureUnitSize.y = texture.height / sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(infiniteHorizontal){
            if(Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSize.x)
            {
                float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSize.x;
                transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y, transform.position.z);
            }
        }
        if(infiniteVertical)
        {
            if(Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSize.y)
            {
                float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSize.y;
                transform.position = new Vector3(cameraTransform.position.x, transform.position.y + offsetPositionY, transform.position.z);
            }
        }
    }
}
