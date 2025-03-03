using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{   
    public float CurrentHealth = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument UIDocument = GetComponent<UIDocument>();

        VisualElement healthBar = UIDocument.rootVisualElement.Q<VisualElement>("HealthBar"); //q=Query

        healthBar.style.width = Length.Percent(CurrentHealth * 100.0f); //style sadrzi sve vizualne atribute koje mogu mjenjati, length je struktura koja sadrzi razlicite jedinice za duzinu, postotak u ovom slucaju


    }

    
}
