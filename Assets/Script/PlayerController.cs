using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public AudioSource backSound;
    bool isJump = true;
    bool isDead = false;
    int idMove = 0;
    Animator anim;
    int score = 0;
    public TextMeshProUGUI txScore;
    public string NameScene = "";
    public GameObject panelWin;
    public GameObject panelGameOver;
    public int winScore;

    // Use this for initialization 
    private void Start()
    {
        anim = GetComponent<Animator>();
        panelWin.SetActive(false);
        panelGameOver.SetActive(false);
    }

    // Update is called once per frame 
    void Update()
    {
        txScore.text = score.ToString() + " / " + winScore;

        //Debug.Log("Jump "+isJump); 
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Idle();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Idle();
        }
        Move();

    }

        private void OnCollisionStay2D(Collision2D collision)
        {
            // Kondisi ketika menyentuh tanah 
            if (isJump)
            {
                anim.ResetTrigger("jump");
                if (idMove == 0) anim.SetTrigger("idle");
                isJump = false;
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            // Kondisi ketika menyentuh tanah 
            anim.SetTrigger("jump");
            anim.ResetTrigger("run");
            anim.ResetTrigger("idle");
            isJump = true;
        }
        public void MoveRight()
        {
            idMove = 1;
        }
        public void MoveLeft()
        {
            idMove = 2;
        }
        private void Move()
        {
            if (idMove == 1 && !isDead)
            {
                // Kondisi ketika bergerak ke kekanan 
                if (!isJump) anim.SetTrigger("run");
                transform.Translate(1 * Time.deltaTime * 5f, 0,0);
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            if (idMove == 2 && !isDead)
            {
                // Kondisi ketika bergerak ke kiri
                if (!isJump) anim.SetTrigger("run");
                transform.Translate(-1 * Time.deltaTime * 5f, 0, 0);
                transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            }
        }
        public void Jump()
        {
            if (!isJump)
            {
                // Kondisi ketika Loncat   
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300f);
                AudioManager.singleton.PlaySound(0);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag.Equals("Item"))
            {
                AudioManager.singleton.PlaySound(2);
                score++;
                Destroy(collision.gameObject);
            }

            if (collision.tag.Equals("EndPoint") && score >= winScore)
            {
                backSound.Stop();
                panelWin.SetActive(true);
            }

            if (collision.transform.tag.Equals("Water"))
            {
                Dead();
            }
        }
        public void Idle()
        {
            // kondisi ketika idle/diam 
            if (!isJump)
            {
                anim.ResetTrigger("jump");
                anim.ResetTrigger("run");
                anim.SetTrigger("idle");
            }
            idMove = 0;

        }
        public void Dead()
        {
            backSound.Stop();
            AudioManager.singleton.PlaySound(1);
            panelGameOver.SetActive(true);
            Time.timeScale = 0;
        }

    public void Next()
    {
        SceneManager.LoadScene(NameScene);
    }

}
