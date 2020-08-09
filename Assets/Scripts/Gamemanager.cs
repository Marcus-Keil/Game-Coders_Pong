using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] GameObject Ball;
    Rigidbody BallRB;
    [SerializeField] float ballStartSpeed = 4.0f;
    [SerializeField] GameObject Paddle_1;
    [SerializeField] TMP_Text ScoreBoard_1;
    int Score_1 = 0;
    [SerializeField] GameObject Paddle_2;
    [SerializeField] TMP_Text ScoreBoard_2;
    int Score_2 = 0;
    [SerializeField] TMP_Text CountDown;
    [SerializeField] int Counter = 3;

    [SerializeField] float boundary = 7.5f;

    [SerializeField] AudioSource MainMusic;
    [SerializeField] float maxSpeed = 10.0f;
    [SerializeField] float basePitch = 0.75f;
    [SerializeField] float maxPitch = 1.25f;

    [SerializeField] Canvas MenuCanvas;
    [SerializeField] Button StartButton;
    [SerializeField] Button ContinueButton;

    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        MainMusic.Pause();
        Ball = GameObject.Find("Ball");
        BallRB = Ball.GetComponent<Rigidbody>();
        startPosition = Ball.gameObject.transform.position;
        ScoreBoard_1.text = Score_1.ToString();
        ScoreBoard_2.text = Score_2.ToString();
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Ball.gameObject.transform.position.x) >= boundary)
        {
            if (Mathf.Sign(Ball.gameObject.transform.position.x) >= 0)
            {
                Score_1 += 1;
                ScoreBoard_1.text = Score_1.ToString();
            }
            else
            {
                Score_2 += 1;
                ScoreBoard_2.text = Score_2.ToString();
            } 
            InitializeBall();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0.0f;
            MainMusic.Pause();
            MenuCanvas.gameObject.SetActive(true);
        }
        Debug.Log(((Mathf.Abs(BallRB.velocity.x) - ballStartSpeed) / (2 * (maxSpeed - ballStartSpeed))));
        if (ballStartSpeed <= Mathf.Abs(BallRB.velocity.x) && Mathf.Abs(BallRB.velocity.x) <= maxSpeed)
            MainMusic.pitch = basePitch + ((Mathf.Abs(BallRB.velocity.x) - ballStartSpeed) / (2 * (maxSpeed - ballStartSpeed)));
        else if (ballStartSpeed > Mathf.Abs(BallRB.velocity.x))
            MainMusic.pitch = basePitch;
        else
            MainMusic.pitch = maxPitch;
    }

    IEnumerator Countdown(int CountDownLength)
    {
        while (CountDownLength > 0)
        {
            CountDown.text = CountDownLength.ToString();
            yield return new WaitForSeconds(1.0f);
            CountDownLength -= 1;
        }
        int SX = Random.Range(0, 2) == 0 ? -1 : 1;
        float SZ = Random.Range(-1.0f, 1.0f);
        BallRB.AddForce(ballStartSpeed * SX, 0, ballStartSpeed * SZ, ForceMode.Impulse);
        BallRB.AddTorque(0, 0, 0, ForceMode.Impulse);
        CountDown.gameObject.SetActive(false);
        if( !MainMusic.isPlaying)
            MainMusic.Play();
    }

    private void InitializeBall()
    {
        Ball.gameObject.transform.position = startPosition;
        BallRB.velocity = new Vector3(0, 0, 0);
        BallRB.angularVelocity = new Vector3(0, 0, 0);
        if (!CountDown.gameObject.activeInHierarchy)
            CountDown.gameObject.SetActive(true);
        StartCoroutine(Countdown(Counter));
    }

    public void ButtonContinue()
    {
        MenuCanvas.gameObject.SetActive(false);
        MainMusic.Play();
        Time.timeScale = 1.0f;
    }

    public void ButtonStart()
    {
        if (StartButton.GetComponentInChildren<Text>().text == "Restart")
        {
            Score_1 = 0;
            ScoreBoard_1.text = Score_1.ToString();
            Score_2 = 0;
            ScoreBoard_2.text = Score_2.ToString();
            MenuCanvas.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            InitializeBall();
        }
        else
        {
            MenuCanvas.gameObject.SetActive(false);
            StartButton.GetComponentInChildren<Text>().text = "Restart";
            ContinueButton.interactable = true;
            Time.timeScale = 1.0f;
            InitializeBall();
        }
        
    }
}
