using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageController : MonoBehaviour
{
    public Sprite Image1;
    public Sprite Image2;
    public Button btn_spin;
    public Button btn_powerup;
    public GolfController GolfController;

    // Update is called once per frame
    void Update()
    {
        if (!GolfController.WantSpin)
        {
            btn_spin.image.sprite = Image1;
        }
        else
        {
            btn_spin.image.sprite = Image2;
        }

        if (!GolfController.PowerUp)
        {
            btn_powerup.image.sprite = Image1;
        }
        else
        {
            btn_powerup.image.sprite = Image2;
        }
    }
}
