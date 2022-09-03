using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;
    private float jump = 10f;
    private Rigidbody2D rb;
    bool startPlaying = false;
    private bool isPlaying = false;
    private Transform trans;
    public SpriteRenderer spriteRender;
    public ParticleSystem deathAnim;

    GameRules gameRules;
    StarShard starShardScript;

    bool isGoingRight = true;

    public int currentScore = 0;
    int lastScore;
    int highScore;
    public int stars;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI starText;
    public TextMeshProUGUI shopStarText;

    public GameObject gameplayCanvas;
    public GameObject menuCanvas;
    public GameObject shopCanvas;
    public GameObject tutorialCanvas;

    public AudioSource jumpAudio;
    public AudioSource buttonAudio;
    public AudioSource deathAudio;

    public Sprite audioOn;
    public Sprite audioOff;
    public Image audioImage;

    public Camera mainCamera;

    void Awake()
    {
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
        starShardScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<StarShard>();
        gameRules = GameObject.FindGameObjectWithTag("Player").GetComponent<GameRules>();
        trans = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        highScore = PlayerPrefs.GetInt("highscore");
        stars = PlayerPrefs.GetInt("stars");
    }
    void Start(){
        starText.text = stars.ToString();
        shopStarText.text = stars.ToString();
        highScoreText.text = highScore.ToString();
        gameplayCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        shopCanvas.SetActive(false);
        rb.simulated = false;
        AudioCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if(startPlaying){
            if(Input.GetMouseButtonDown(0)){
                jumpAudio.Play();
                if(isPlaying == false){
                    isPlaying = true;
                    rb.simulated = true;
                    GoRight();
                }
                else{
                    if(isGoingRight == true){
                        rb.velocity = new Vector2(speed,jump);
                    }else{
                        rb.velocity = new Vector2(-speed,jump);
                    }
                    
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
           OnDeath();
        }

        if(collision.gameObject.tag == "RightSwitch"){
            GoLeft();
            gameRules.DestroyRightSwitches();
            gameRules.RandomRight();
        }
         if(collision.gameObject.tag == "LeftSwitch"){
            GoRight();
            gameRules.DestroyLeftSwitches();
            gameRules.RandomLeft();
        }
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag == "Diamond"){
            starShardScript.DestroyShard();
            starShardScript.CreateShard();
            currentScore++;
            currentScoreText.text = currentScore.ToString();
        }
    }

    void OnDeath(){
        StartCoroutine(PlayDeathAnim());
        
        lastScore = currentScore;
        lastScoreText.text = lastScore.ToString();
        if(currentScore > highScore){
            highScore = currentScore;
            highScoreText.text = highScore.ToString();
            PlayerPrefs.SetInt("highscore", highScore);
            }
        stars = stars + (currentScore/5);
        starText.text = stars.ToString();
        shopStarText.text = stars.ToString();
        PlayerPrefs.SetInt("stars",stars);
        currentScoreText.text = currentScore.ToString();                
    }
    void GoRight(){
        isGoingRight = true;
        rb.velocity = new Vector2(0f,0f);
        rb.AddForce(transform.right * speed,ForceMode2D.Impulse); 
    }
    void GoLeft(){
        isGoingRight = false;
        rb.velocity = new Vector2(0f,0f);
        rb.AddForce(-transform.right * speed,ForceMode2D.Impulse); 
    }
    public void StartPlaying(){
        buttonAudio.Play();
        gameplayCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        startPlaying = true;
        gameRules.RandomLeft();
        gameRules.RandomRight();
        starShardScript.CreateShard();
    }

    public void OpenShop(){
        buttonAudio.Play();
        menuCanvas.SetActive(false);
        shopCanvas.SetActive(true);
    }

    public void CloseShop(){
        buttonAudio.Play();
        shopCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }
    public void OpenTutorial(){
        buttonAudio.Play();
        tutorialCanvas.SetActive(true);
        this.gameObject.SetActive(false);
        menuCanvas.SetActive(false);
    }
    public void CloseTutorial(){
        buttonAudio.Play();
        tutorialCanvas.SetActive(false);
        this.gameObject.SetActive(true);
        menuCanvas.SetActive(true);
    }
    public void AudioAdjust(){
        if(PlayerPrefs.GetInt("isMute") == 1){
            PlayerPrefs.SetInt("isMute",0);
            Debug.Log(PlayerPrefs.GetInt("isMute"));
        }else if(PlayerPrefs.GetInt("isMute") == 0){
            PlayerPrefs.SetInt("isMute",1);
            Debug.Log(PlayerPrefs.GetInt("isMute"));
        }
        AudioCheck();
    }
    void AudioCheck(){
        if(PlayerPrefs.GetInt("isMute") == 1){
            mainCamera.GetComponent<AudioSource>().volume = 0;
            jumpAudio.volume = 0;
            buttonAudio.volume = 0;
            deathAudio.volume = 0;
            audioImage.sprite = audioOff;
        }else if(PlayerPrefs.GetInt("isMute") == 0){
            mainCamera.GetComponent<AudioSource>().volume = 1;
            jumpAudio.volume = 1;
            buttonAudio.volume = 1;
            deathAudio.volume = 1;
            audioImage.sprite = audioOn;
        }
    }
    IEnumerator PlayDeathAnim(){
        Debug.Log("Death anim start");
        deathAnim.Play();
        spriteRender.enabled = false;
        rb.simulated = false;
        isGoingRight = true;
        isPlaying = false;
        startPlaying = false;
        rb.simulated = false;
        deathAudio.Play();

        yield return new WaitForSeconds(2);

        deathAnim.Stop();
        Debug.Log("Death anim stop");
        currentScore = 0;
        spriteRender.enabled = true;
        trans.position = new Vector2(0,0);
        starShardScript.DestroyShard();
        gameplayCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        gameRules.DestroyLeftSwitches();
        gameRules.DestroyRightSwitches();
        
    }

}
