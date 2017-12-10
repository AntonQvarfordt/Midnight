using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CursorEffects : MonoBehaviour {

    public Image InnerSprite;
    public Image OuterSprite;

    Color InnerSpriteStartColor;
    Color OuterSpriteStartColor;

    private void Awake()
    {
        InnerSpriteStartColor = InnerSprite.color;
        OuterSpriteStartColor = OuterSprite.color;
    }

    public void Bop (Transform controller)
    {
        controller.localScale = Vector3.one * 2;
        controller.DOScale(1, 0.5f).SetEase(Ease.InBack);

        OuterSprite.color = Color.red;
        OuterSprite.DOColor(OuterSpriteStartColor, 1f);

        InnerSprite.color = Color.red;
        InnerSprite.DOColor(InnerSpriteStartColor, 1f);
    }
}
