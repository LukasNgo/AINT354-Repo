using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerInputEcholocation : MonoBehaviour {

    // echolocation toggle
    private bool echolocation = false;

    // monster controller script
    private MonsterController m_monsterController_script;

    // UI objects
    // Timer
    private int cooldownTimerforUI;
    private Text timeText;
    private Image coolDownTimerRend;
    // Slider
    private GameObject sliderObject;

    // IEnumerator Helpers
    private bool isRunningCooldown = false;
    private bool isRunningSlider = false;

    private bool isCooldownNeeded = false;

    // 69A1FF 176EFF
    // Use this for initialization
    void Start () {
        // Get Monster Controller script
        m_monsterController_script = GameObject.FindGameObjectWithTag("Enemy").GetComponent<MonsterController>();

        // Get Objects for Cooldown Timer
        timeText = GameObject.FindGameObjectWithTag("coolDownTimerText").GetComponent<Text>();
        coolDownTimerRend = GameObject.FindGameObjectWithTag("coolDownTimer").GetComponent<Image>();

        // Get Slider
        sliderObject = GameObject.FindGameObjectWithTag("slider");

        // Slider off
        sliderObject.SetActive(false);

        // Cooldown timer off
        coolDownTimerRend.enabled = false;
        timeText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        // mousebutton(1) hold
        echolocation = Input.GetMouseButton(1);

        // if mousebutton(1) hold
        if(echolocation)
        {
            if (!isRunningSlider && !isCooldownNeeded && !isRunningCooldown)
            {
                StartCoroutine("updateSlider");
            }
        }

        // if no cooldown
        if (!isRunningCooldown)
        {
            //Debug.Log("Can now use 'focus'");
            // fade in shaders
            m_monsterController_script.setTransparencyBool(echolocation);
        }

        if (Input.GetMouseButtonUp(1) || isCooldownNeeded)
        {
            if (!isRunningCooldown)
            {
                //Debug.Log("'focus' on cooldown for 10 seconds");
                StartCoroutine("cooldownTimer");
            }
        }
    }

    private IEnumerator updateSlider()
    {
        FindObjectOfType<AudioManager>().Play("HoldBreath");

        isRunningSlider = true;

        // turn on slider
        sliderObject.SetActive(true);
        // set slider initial value
        sliderObject.GetComponent<Slider>().value = 1.0f;

        for (float time = 10; time > 0; time -= Time.deltaTime)
        {
            // update timer
            sliderObject.GetComponent<Slider>().value = time / 10.0f;
            
            yield return null;
        }

        isRunningSlider = false;
        isCooldownNeeded = true;
    }

    private IEnumerator cooldownTimer()
    {
        FindObjectOfType<AudioManager>().Play("CatchBreath");

        isRunningCooldown = true;

        m_monsterController_script.setTransparencyBool(false);

        // initial start and enable timer
        cooldownTimerforUI = 5;
        coolDownTimerRend.enabled = true;
        timeText.enabled = true;

        // disable slider
        sliderObject.SetActive(false);

        for (float time = 5; time > 0; time -= Time.deltaTime)
        {
            cooldownTimerforUI = Mathf.RoundToInt(time);
            // update timer
            timeText.text = cooldownTimerforUI.ToString();
            yield return null;
        }

        // disable timer
        coolDownTimerRend.enabled = false;
        timeText.enabled = false;
        isRunningCooldown = false;
        isCooldownNeeded = false;
    }
}
