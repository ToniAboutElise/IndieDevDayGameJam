using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public AudioSource audioSource;
    public SpriteRenderer spriteRenderer;

    public Sprite[] colorSprite;
    public Sprite[] headSprite;
    public Sprite[] eyesSprite;
    public Sprite[] beakSprite;

    public SpriteRenderer colorImage;
    public SpriteRenderer headImage;
    public SpriteRenderer eyesImage;
    public SpriteRenderer beakImage;

    public BoxCollider boxCollider;

    public Color color;
    public Head head;
    public Eyes eyes;

    public enum Color
    {
        Yellow,
        Red,
        Blue,
        Green,
        White,
        Grey
    }

    public enum Head
    {
        None,
        ArmyHelmet,
        BaseballCap
    }

    public enum Eyes
    {
        None,
        GlassesWhite,
        GlassesBlue,
        GlassesOrange,
        GlassesPink,
        SunGlassesWhite,
        SunGlassesBlue,
        SunGlassesOrange,
        SunGlassesPink
    }

    public enum Beak
    {
        Yellow,
        Red,
        Blue,
        Green,
        White,
        Grey
    }

    [ExecuteInEditMode]
    protected void SetChickenVisualValues()
    {

    }

    public void ChickenHit()
    {
        boxCollider.enabled = false;
        audioSource.Play();
        spriteRenderer.enabled = false;
    }

}
