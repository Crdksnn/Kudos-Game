using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Interview", menuName = "New Interview")]
public class Interview : ScriptableObject
{
    
    
    [SerializeField] GameObject personPrefab;
    [SerializeField] Sprite propertiesCardImage;

    [Header("Particle Effects")]
    [SerializeField] GameObject[] winParticleEffects;
    [SerializeField] GameObject[] loseParticleEffects;

    [Header("Button Texts")] 
    [SerializeField] String greenButtonText;
    [SerializeField] String redButtonText;

    [Header("Decision Text Configures")] 
    [SerializeField] String goodDecisitonText;
    [SerializeField] String badDecisionText;
    [SerializeField] Color goodDecisitonColor;
    [SerializeField] Color badDecisitonColor;
    
    public GameObject GetPersonPrefab(){ return personPrefab; }
    public Sprite GetImage() { return propertiesCardImage; }
    public String GetGreenButtonText(){ return greenButtonText; }
    public String GetRedButtonText(){ return redButtonText; }
    public String GetGoodDecisionText() { return goodDecisitonText; }
    public String GetBadDecisionText() { return badDecisionText; }
    public Color GetGoodDecisionColor() { return goodDecisitonColor; }
    public Color GetBadDecisionColor() { return badDecisitonColor; }
    public GameObject[] GetPartialEffects(bool condition)
    {

        if (condition)
        {
            return winParticleEffects;
        }

        return loseParticleEffects;

    }
    
}
