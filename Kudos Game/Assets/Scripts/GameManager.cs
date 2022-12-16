using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameCondition;
    [SerializeField] GameObject personStayPoint;
    Animator currentPersonAnimator;
    private Action deneme;
    
    [Header("Properties Card")]
    public PropertiesCard propertiesCard;

    [Header("Person List")]
    public List<Interview> personList;
    Interview currentPersonInterview;
    
    [Header("Decision Text")]
    [SerializeField] GameObject decisionTextPrefab;
    [SerializeField] Transform decisionTextPos;

    [Header("Final Scene")]
    [SerializeField] GameObject[] finalScreenEffects;
    // [SerializeField] GameObject[] moneyVFXs;
    // [SerializeField] GameObject finalTMPro;
    
    void Start()
    {
        currentPersonInterview = personList.First();
        
        personStayPoint = Instantiate(currentPersonInterview.GetPersonPrefab(), personStayPoint.transform.position, Quaternion.Euler(new Vector3(0,90,0)));
        currentPersonAnimator = personStayPoint.GetComponent<Animator>();
        
        propertiesCard.ChangePropertiesCardImage(currentPersonInterview.GetImage());
        propertiesCard.ChangeButtonText(currentPersonInterview.GetGreenButtonText(),currentPersonInterview.GetRedButtonText());

        InterviewStartProcess();
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PersonChange();
        }
       
    }
    
    public void PersonChange()
    {
        
        int index = personList.IndexOf(currentPersonInterview);
        
        if (index + 1 < personList.Count())
        {
            Destroy(personStayPoint);
            index++;
            currentPersonInterview = personList[index];
            personStayPoint = Instantiate(currentPersonInterview.GetPersonPrefab(), personStayPoint.transform.position, Quaternion.Euler(new Vector3(0,90,0)));
            currentPersonAnimator = personStayPoint.GetComponent<Animator>();
            
            //Down animation of properties card and change information of properties card.
            StartCoroutine(PropertiesUpCardAnimationStarter(currentPersonAnimator.GetCurrentAnimatorStateInfo(0).length));
            propertiesCard.ChangePropertiesCardImage(currentPersonInterview.GetImage());
            
            return;
        }
        
        //Final Screen
        foreach (var effects in finalScreenEffects)
        {
            effects.SetActive(true);
        }
        
    }
    private void PlayParticleEffects(bool condition)
    {
        var partialEffects = currentPersonInterview.GetPartialEffects(condition);
        foreach (var partialEffect in partialEffects)
        {
            var temp = Instantiate(partialEffect);
            Destroy(temp, temp.GetComponent<ParticleSystem>().duration + 1);
        }
    }
    
    public void PressGreenButton()
    {
        String pressedButton = "pressedGreenButton";
        gameCondition = true;
        DecisionTextSettings(gameCondition);
        InterviewEndProcess(gameCondition, pressedButton);
    }
    
    public void PressRedButton()
    {
        String pressedButton = "pressedRedButton";
        gameCondition = false;
        DecisionTextSettings(gameCondition);
        InterviewEndProcess(gameCondition, pressedButton);
    }
    private void DecisionTextSettings(bool condition)
    {
        var decisionText = Instantiate(decisionTextPrefab, decisionTextPos);
        TextMeshProUGUI decisionTextTMPro = decisionText.GetComponent<TextMeshProUGUI>();

        if (condition)
        {
            decisionTextTMPro.color = currentPersonInterview.GetGoodDecisionColor();
            decisionTextTMPro.text = currentPersonInterview.GetGoodDecisionText();
        }

        else
        {
            decisionTextTMPro.color = currentPersonInterview.GetBadDecisionColor();
            decisionTextTMPro.text = currentPersonInterview.GetBadDecisionText();
        }
        
        Destroy(decisionText, decisionText.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
    private void InterviewStartProcess()
    {
        StartCoroutine(PropertiesUpCardAnimationStarter(currentPersonAnimator.GetCurrentAnimatorStateInfo(0).length));
    }
    private void InterviewEndProcess(bool condition, String pressedButton)
    {
        propertiesCard.DownCard();
        StartCoroutine(PlayParticleEffectsStarter(propertiesCard.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length, condition,pressedButton));
    }
    private IEnumerator PropertiesUpCardAnimationStarter(float delay)
    {
        yield return new WaitForSeconds(delay);
        propertiesCard.UpCard();
    }
    private IEnumerator PlayParticleEffectsStarter(float delay, bool condition, String pressedButton)
    {
        yield return new WaitForSeconds(delay);
        PlayParticleEffects(condition);
        currentPersonAnimator.SetBool(pressedButton,true);
        yield return new WaitForSeconds(currentPersonAnimator.GetCurrentAnimatorStateInfo(0).length / 2);
        PersonChange();
    }

}
