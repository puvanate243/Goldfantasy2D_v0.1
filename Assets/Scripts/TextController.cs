using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public GolfController golf;
    public Text txt_power;
    public GameObject txt_spin;
    public GameObject txt_powerup;
    // Update is called once per frame
    void Update()
    {
        Text_Power();
        Text_Spin();
        Text_PowerUp();
        
    }

    private void Text_Power()
    {
        if (golf.Final_Power <= 0)
        {
            txt_power.gameObject.SetActive(false);
        }
        else
        {
            txt_power.gameObject.SetActive(true);
            txt_power.text = (golf.Final_Power * 10).ToString("N0") + "y";
        }
    }
    private void Text_Spin()
    {
        if (golf.WantSpin)
        {
            txt_spin.SetActive(true);
        }
        else
        {
            txt_spin.SetActive(false);
        }
    }
    private void Text_PowerUp()
    {
        if (golf.PowerUp)
        {
            txt_powerup.SetActive(true);
        }
        else
        {
            txt_powerup.SetActive(false);
        }
    }



}
