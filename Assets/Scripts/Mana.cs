using UnityEngine.UI;
using UnityEngine;
using System;

public class Mana : MonoBehaviour
{

    private float currentMana;
    private float maxMana = 100f;
    private float manaExpense = 10f;
    private float manaRegen = 5f;
    private float chipSpeed = 2f;
    [SerializeField] private Image _fontManaBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMana = maxMana;
        UpdateManaBar();
    }

    // Update is called once per frame
    void Update()
    {
        checkMana();
        
    }


    void checkMana(){
        currentMana = Mathf.Clamp(currentMana, 0 ,  maxMana);
        bool inputEx = Input.GetKeyDown(KeyCode.F);
        bool inputRe = Input.GetKey(KeyCode.L);
        if(inputEx){
            UseMana(manaExpense);
        }
        if(inputRe){
            RegenMana();
        }
    }

    void UseMana(float amount){
        if(currentMana >= amount){
            currentMana -= amount;
            UpdateManaBar();
        }
    }

    void RegenMana(){
        if(currentMana < maxMana){
            currentMana += manaRegen * Time.deltaTime * chipSpeed;
            currentMana = Mathf.Clamp(currentMana,0 , maxMana);
            UpdateManaBar();
        }
    }

    void  UpdateManaBar(){
        _fontManaBar.fillAmount = currentMana/ maxMana;
    }

}
