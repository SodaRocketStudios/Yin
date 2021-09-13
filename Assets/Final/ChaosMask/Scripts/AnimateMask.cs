using UnityEngine;

public class AnimateMask : MonoBehaviour
{
    private SpriteMask mask;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        mask = GetComponent<SpriteMask>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mask.sprite = spriteRenderer.sprite;
    }
}
