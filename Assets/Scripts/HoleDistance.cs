using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoleDistance : MonoBehaviour
{
    private Text text_hole;
    public GameObject golf;
    public Transform hole;
    private GolfController GolfController;
    private float distance;
    private float height;

    private void Start()
    {
        text_hole = GetComponent<Text>();
        GolfController = golf.GetComponent<GolfController>();
    }

    void Update()
    {
        distance = hole.position.x - golf.transform.position.x;
        height = hole.position.y - golf.transform.position.y;
        text_hole.text = "Hole : " + (distance*10).ToString("N2") + "y / " + height.ToString("N2");
        if(height > 30)
        {
            GolfController.ResetScene();
        }
    }
}
