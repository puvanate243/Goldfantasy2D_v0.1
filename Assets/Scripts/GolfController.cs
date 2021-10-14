using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GolfController : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;
    private bool Grounded = false;
    private bool Moving = true;
    private bool StopMove = false;
    private bool SwingHit = false;
    private bool CanSwing = false;
    private bool Spining = false;
    private bool Spined = false;
    private bool Win = false;
    private float power = 0;
    private Rigidbody2D rb;
    private Quaternion OriginRotation;
    private float StartPosition;
    private GameObject arrow;
    private float RaycastLimit = 0.01f;
    private int SpinCnt = 0;

    public bool WantSpin = false;
    public bool PowerUp = false;
    public float Final_Power=0;
    public float Degree;
    public Transform arrow_point;
    public GameObject PowerBar;
    private Image PowerBarImage;
    private Color PowerBarColor;
    public LayerMask RayLayer;
    private SceneController SceneController;
    public GameObject EffectExplosion;

    void Start()
    {
        Setup();
    }

    void Update()
    {
        Swing();
        CanSwingController();
        StopMoving();
        GroundCheck();
        SpinController();
        
    }

    void FixedUpdate()
    {
        SwingController();
    }

    private void Setup()
    {
        rb = GetComponent<Rigidbody2D>();
        arrow = transform.GetChild(0).gameObject;
        OriginRotation = transform.rotation;
        circleCollider2D = GetComponent<CircleCollider2D>();
        SceneController = GetComponent<SceneController>();
        PowerBarImage = PowerBar.GetComponent<Image>();
        PowerBarColor = PowerBarImage.color;
       
    }
    private void CanSwingController()
    {
        float CurrentSpeed = GetComponent<Rigidbody2D>().velocity.magnitude;
        float time = 0;
        if (CurrentSpeed < 0.5 && Grounded)
        {
            time = Time.time;
        }

        if(time+2f > Time.time && CurrentSpeed < 0.5 && Grounded)
        {
            StopMove = true;
        }

        if (CurrentSpeed <= 0 && Moving && Grounded && !Spining)
        {
            //Reset
            StopMove = true;
            CanSwing = true;
            Moving = false;
            transform.rotation = OriginRotation;
            power = 0;
            SpinCnt = 0;
            Spined = false;
        }
    }
    private void SwingController()
    {
        if (CanSwing)
        {
            PowerBarImage.fillAmount = power / 10;
            arrow.SetActive(true);
            if (Input.GetKey(KeyCode.W))
            {
                transform.Rotate(0, 0, 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Rotate(0, 0, -1);
            }
            Degree = transform.rotation.eulerAngles.z;
        }
        else
        {
            arrow.SetActive(false);
        }
    }
    private void Swing()
    {
        if (SwingHit)
        {
           
            StopMove = false;
            Moving = true;
            CanSwing = false;
            Vector2 direction = arrow_point.position - transform.position;
            
            if (power >= 8)
            {
                Final_Power = power + 1;
            }
            else if(power < 8 && power > 5)
            {
                Final_Power = power + 2;
            }
            else
            {
                Final_Power = power + 3f;
            }

            if (PowerUp)
            {
                Final_Power += Final_Power * 10/100;
            }
                    
            Final_Power = Final_Power + (Final_Power * 2f / 10);
            rb.velocity = direction * Final_Power;

            StartPosition = transform.position.x;
            SwingHit = false;

        }
    }
    private void StopMoving()
    {
        if (StopMove && Grounded)
        {
            rb.velocity = Vector2.zero;
        }
        else if(StopMove && !Grounded)
        {
            StopMove = false;
        }
    }
    private void GroundCheck()
    {
        RaycastHit2D BoxCenter = Physics2D.BoxCast(circleCollider2D.bounds.center, circleCollider2D.bounds.size, 0f, Vector2.down, RaycastLimit, RayLayer);
        if (BoxCenter.collider != null)
        {
            Grounded = true;
        }
        else if (PowerUp)
        {
            Grounded = false;
            SpinCnt = 1;
        }
        else
        {
            Grounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Win")
        {
            CanSwing = false;
            Moving = false;
            Win = true;
            Invoke("ResetScene", 2);
        }
           
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" && Moving)
        {
            SpinCnt += 1;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && Moving && PowerUp && !Spined)
        {
            SpinCnt = 1;
        }
    }

    public void ResetScene()
    {

        SceneController.LoadScene(GameManager.SceneIndex);

    }
    private void SpinController()
    {
        if (PowerUp && Moving && Grounded && SpinCnt > 0)
        {
            Explosion();
           
            StopMove = true;
            Spining = true;
            Invoke("Spin", 0.5f);
            SpinCnt = 0;
        }
        else if (Moving && SpinCnt > 2 && Grounded && power > 5 && WantSpin)
        {
            StopMove = true;
            Spining = true;
            Invoke("Spin",0.5f);
            SpinCnt = 0;
        }
    }
    private void Spin()
    {
        if (Spining && !Spined && power > 2 && !Win)
        {
            StopMove = false;
            
            if (transform.position.x - StartPosition > 0 && WantSpin)
            {
                rb.velocity = new Vector2(-2, 0);
            }
            else if(WantSpin)
            {
                rb.velocity = new Vector2(2, 0);
            }
            Spining = false;
            Spined = true;
        }
        else
        {
            Spined = true;
            Spining = false;
        }
    }

    public void Button_Up()
    {
        if (CanSwing)
        {
            arrow.SetActive(true);
            transform.Rotate(0, 0, 1);
        }
        else
        {
            arrow.SetActive(false);
        }
    }
    public void Button_Down()
    {
        if (CanSwing)
        {
            arrow.SetActive(true);
            transform.Rotate(0, 0, -1);
        }
        else
        {
            arrow.SetActive(false);
        }

    }
    public void Button_Hit_KeyDown()
    {
        if (CanSwing)
        {
            power += 0.07f;
        }
    }
    public void Button_Hit_KeyUp()
    {
        if (CanSwing)
        {
            if (power > 10)
                power = 10;
            SwingHit = true;
        }
    }

    public void Button_PowerUp()
    {
        if (CanSwing)
        {
            PowerUp = !PowerUp;
            if (!PowerUp)
            {
                PowerBarImage.color = PowerBarColor;
            }
            else
            {
                PowerBarImage.color = Color.red;
            }
        }
    }

    public void Button_Spin()
    {
        if (CanSwing)
        {
            WantSpin = !WantSpin;
        }
    }

    private void Explosion()
    {
        EffectExplosion.transform.position = transform.position + new Vector3(0,0.7f,0);
        EffectExplosion.SetActive(true);
    }
    
}
