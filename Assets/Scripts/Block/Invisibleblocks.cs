using UnityEngine;

public class Invisibleblocks : Block
{

    private bool _invisible;
    protected override void Start()
    {
        base.Start();

        SetAlpha(0f);
    }

    protected override void SetSprite()
    {  
        SetAlpha(1f);
        if(_invisible)
            base.SetSprite();
        _invisible = true;


    }

    private void SetAlpha(float alpha)
    {
        Color spriteColor = _spriteRenderer.color;
        spriteColor.a = alpha;
        _spriteRenderer.color = spriteColor;
    }
}