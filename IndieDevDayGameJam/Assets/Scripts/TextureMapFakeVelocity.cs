using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureMapFakeVelocity : MonoBehaviour
{
    public float scrollSpeed = 0.5F;
    public float scrollSpeedHorizontal = 0.0f;
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        float offsetHorizontal = Time.time * scrollSpeedHorizontal;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offsetHorizontal, offset));
    }
}
