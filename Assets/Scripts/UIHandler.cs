using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class UIHandler : MonoBehaviour
{
    public static UIHandler instance {get; private set;}
    private VisualElement m_Healthbar;

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
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(percentage * 100);
    }
    
 }
