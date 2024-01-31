using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

[Serializable]
public class ColorEvent : UnityEvent<Color>{ }
public class ColorPicker : MonoBehaviour
{
    public TextMeshProUGUI DebugText;
    public ColorEvent OnColorPreview;
    public ColorEvent OnColorSelect;
    public Material newColorMaterial;
    public Material prevColorMaterial;
    public Image previousColor;
    public Image newColor;
    RectTransform rect;
    Texture2D colorTexture;
    public bool debugOn = false;
    // Start is called before the first frame update
    void Start()
    {
        newColor.color = newColorMaterial.color;
        previousColor.color = prevColorMaterial.color;
        rect = GetComponent<RectTransform>();
        colorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition))
        {
            Vector2 delta;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out delta);

            string debug = "mousePosition" + Input.mousePosition;
            debug +="<br>delta=" + delta;

            float width = rect.rect.width;
            float height = rect.rect.height;
            delta += new Vector2(width * .5f, height * .5f);
            debug += "<br>offset delta" + delta;
            float x = Mathf.Clamp(delta.x / width, 0f, 1f);
            float y = Mathf.Clamp(delta.y / height, 0f, 1f);
            debug += "<br>x=" + x + "y=" + y;
            int texX = Mathf.RoundToInt(x * colorTexture.width);
            int texY = Mathf.RoundToInt(y * colorTexture.height);
            debug += "<br>texX=" + texX + "texY=" + texY;

            Color color = colorTexture.GetPixel(texX, texY);
            
            if (debugOn == true){
                DebugText.color = color;
                DebugText.text = debug;
            }

            OnColorPreview?.Invoke(color);
            if(Input.GetMouseButtonDown(0)){
                prevColorMaterial.color = newColorMaterial.color;
                newColorMaterial.color = color;
                previousColor.color = prevColorMaterial.color;
                OnColorSelect?.Invoke(color);
            }
        }

    }

    public void ReturnColor(){
        newColorMaterial.color = prevColorMaterial.color;
        newColor.color = previousColor.color;
    }
}
