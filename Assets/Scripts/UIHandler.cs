using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class UIHandler : MonoBehaviour
{
    public static UIHandler instance {get; private set;}
    private VisualElement m_Healthbar;

    public float displayTime = 4.0f;
    private VisualElement m_NPCDialogue;
    private float m_TimerDisplay;


    private void Awake() {

    instance = this;

    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();

        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar"); //q=Query
        SetHealthValue(1.0f);
        //healthBar.style.width = Length.Percent(CurrentHealth * 100.0f); //style sadrzi sve vizualne atribute koje mogu mjenjati, length je struktura koja sadrzi razlicite jedinice za duzinu, postotak u ovom slucaju
        m_NPCDialogue = uiDocument.rootVisualElement.Q<VisualElement>("Background");
        m_NPCDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;


    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(percentage * 100);
    }

    private void Update() {

        if (m_TimerDisplay > 0) {

            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay <= 0) {

                m_NPCDialogue.style.display = DisplayStyle.None;
            }

       }
    }

    public void DisplayDialogue() {

        m_NPCDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;   

    }


    
 }
