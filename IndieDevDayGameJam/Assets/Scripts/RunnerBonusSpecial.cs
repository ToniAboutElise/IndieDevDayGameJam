using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerBonusSpecial : RunnerBonus
{
    public SpecialBonusType specialBonusType;
    public SpriteRenderer bonusSpecialImage;
    public Sprite superBonusSprite;
    public Sprite puzzlePieceSprite;
    public Sprite keySprite;

    public enum SpecialBonusType
    {
        SuperBonus,
        PuzzlePiece,
        Key,
    }

    private void Start()
    {
        SetBonusType();
    }

    public void SetBonusType()
    {
        int rand = Random.Range(0, 100);

        if (rand >= 0 && rand < 85)
        {
            specialBonusType = SpecialBonusType.SuperBonus;
            bonusSpecialImage.sprite = superBonusSprite;
        }
        else if (rand >= 85 && rand < 94)
        {
            specialBonusType = SpecialBonusType.Key;
            bonusSpecialImage.sprite = keySprite;
        }
        else
        {
            specialBonusType = SpecialBonusType.PuzzlePiece;
            bonusSpecialImage.sprite = puzzlePieceSprite;
        }

        Debug.Log(specialBonusType);
    }

    public override void BonusGrabbed()
    {
        switch (specialBonusType)
        {
            case SpecialBonusType.SuperBonus:

                break;
            case SpecialBonusType.PuzzlePiece:

                break;
            case SpecialBonusType.Key:

                break;
        }
    }
}
