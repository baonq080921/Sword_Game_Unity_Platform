using UnityEngine;

public class Player_Mana : MonoBehaviour
{
    public float currentMana;
    public float maxMana = 100f;
    public float delayTimer;
    public float delayTime = 0f;
    public float manaFraction ;
    public  float manaRegen = 0.1f
    public float chipSpeed = 2f;
    public Image fontManaBar;
    public Image backManaBar;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        currentMana = maxMana;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentMana = Mathf.Clamp(currentMana , 0 , maxMana);
        ManaUsing(Random.range(5f,10f));
        
    }

    public void UpdateManaUI(){

        manaFraction = currentMana / maxMana;
        float fillManaF = fontManaBar.fillAmount;
        float fillManaB = backManaBar.fillAmount;
        if(fillManaB > manaFraction){
            delayTimer += Time.deltaTime;
            if(delayTimer >= delayTime){
                fillManaB = Mathf.Lerp(fillManaB,manaFraction,Time.deltaTime *chipspeed);
            }
        }else{
            delayTimer = 0;
            fillManaB = manaFraction;
        }


    }

    public void ManaUsing(int manaUse){
        UpdateManaUI();
        if(Input.GetKeyDown(KeyCode.H)){
            currentMana -= manaUse;
        }else{
            currentMana += manaRegen;
        }

    }
}
