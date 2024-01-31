using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;
public class PreviousColorSelct : MonoBehaviour
{
    public ColorEvent OnColorSelect;
    public ColorPicker colorPicker;
    public Material prevColorMaterial;
    public Image previousColor;
    RectTransform rectPrev;
    Texture2D colorTexture;
    // Start is called before the first frame update
    // void Start()
    // {
    //     rectPrev = GetComponent<RectTransform>();
    //     colorTexture = GetComponent<Image>().mainTexture as Texture2D;
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(RectTransformUtility.RectangleContainsScreenPoint(rectPrev, Input.mousePosition))
    //     {
    //         Vector2 delta;
    //         RectTransformUtility.ScreenPointToLocalPointInRectangle(rectPrev, Input.mousePosition, null, out delta);


    //         float width = rectPrev.rect.width;
    //         float height = rectPrev.rect.height;
    //         delta += new Vector2(width * .5f, height * .5f);
    //         float x = Mathf.Clamp(delta.x / width, 0f, 1f);
    //         float y = Mathf.Clamp(delta.y / height, 0f, 1f);
    //         int texX = Mathf.RoundToInt(x * colorTexture.width);
    //         int texY = Mathf.RoundToInt(y * colorTexture.height);

    //         Color color = colorTexture.GetPixel(texX, texY);

    //         OnColorPreview?.Invoke(color);
    //         if(Input.GetMouseButtonDown(0)){
    //             colorPicker.newColor = colorPicker.prevColor;
    //             previousColor.color = prevColor;
    //             OnColorSelect?.Invoke(color);
    //         }
    //     }

    // }
}
