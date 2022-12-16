using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesCard : MonoBehaviour
{
    [SerializeField] RawImage propertiesCardImage;
    public Animator propertiesAnimator;
    
    string isUp = "isUp";
    string isDown = "isDown";
    
    [SerializeField] TextMeshProUGUI greenButtonText;
    [SerializeField] TextMeshProUGUI redButtonText;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpCard();
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DownCard();
        }
    }
    
    public void ChangePropertiesCardImage(Sprite sprite)
    {
        propertiesCardImage.texture = sprite.texture;
    }
    public void UpCard()
    {
        propertiesAnimator.SetBool(isUp,true);
        propertiesAnimator.SetBool(isDown,false);
    }
    public void DownCard()
    {
        propertiesAnimator.SetBool(isUp,false);
        propertiesAnimator.SetBool(isDown,true);
    }
    public void ChangeButtonText(String greenButtonText, String redButtonText)
    {
        this.greenButtonText.text = greenButtonText;
        this.redButtonText.text = redButtonText;
    }
    
}
