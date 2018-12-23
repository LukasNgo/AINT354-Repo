using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerInputEcholocation : MonoBehaviour {

    private bool echolocation = false;
    private MonsterController m_monsterController_script;
    private bool cooldown = false;
    private int cooldownTimerforUI;
    private Text timeText;
    private bool timeOut = false;
    private bool timeOut2 = false;
    private Image coolDownTimerRend;
    private GameObject sliderObject;
    
    // 69A1FF 176EFF
    // Use this for initialization
    void Start () {
        // get monster script
        m_monsterController_script = GameObject.FindGameObjectWithTag("Enemy").GetComponent<MonsterController>();
        timeText = GameObject.FindGameObjectWithTag("coolDownTimerText").GetComponent<Text>();
        coolDownTimerRend = GameObject.FindGameObjectWithTag("coolDownTimer").GetComponent<Image>();
        sliderObject = GameObject.FindGameObjectWithTag("slider");
        sliderObject.SetActive(false);
        coolDownTimerRend.enabled = false;
        timeText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        echolocation = Input.GetMouseButton(1);

        if(Input.GetMouseButton(1))
        {
            if (!timeOut2)
            {
                timeOut2 = true;
                StartCoroutine("updateSlider");
            }
        }

        Debug.Log(echolocation);
        // dim lights
        // and pulses
        // change shader transparency
        if (cooldown == false)
        {
            Debug.Log("Can now use 'focus'");

            m_monsterController_script.setTransparencyBool(echolocation);
        }

        
        if (Input.GetMouseButtonUp(1))
        {
            if (!timeOut)
            {
                timeOut = true;
                Debug.Log("'focus' on cooldown for 10 seconds");
                cooldown = true;
                StartCoroutine("cooldownTimer");
            }
        }
	}

    private IEnumerator cooldownTimer()
    {
        // display timer
        cooldownTimerforUI = 10;
        coolDownTimerRend.enabled = true;
        timeText.enabled = true;
        sliderObject.SetActive(false);

        for (float time = 10; time > 0; time -= Time.deltaTime)
        {
            cooldownTimerforUI = Mathf.RoundToInt(time);
            // update timer
            timeText.text = cooldownTimerforUI.ToString();
            yield return null;
        }

        coolDownTimerRend.enabled = false;
        timeText.enabled = false;
        cooldown = false;
        timeOut = false;
    }

    private IEnumerator updateSlider()
    {
        sliderObject.SetActive(true);
        sliderObject.GetComponent<Slider>().value = 1.0f;

        for (float time = 10; time > 0; time -= Time.deltaTime)
        {

            // update timer
            sliderObject.GetComponent<Slider>().value = time / 10.0f;
            yield return null;
        }

        timeOut2 = false;
    }
}
