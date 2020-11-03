using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;
    public float gravityScale;

    public Text counter;
    public Text highscoreCounter;
    public Text deathMessage;
    public AudioSource eatSound;


    private Vector3 moveDirection;
    private bool isDead = false;

    private static PlayerController _instance;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        int highScore = PlayerPrefs.GetInt("highscore", 0);
        highscoreCounter.text = highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.transform.position.y < 0)
        {
            isDead = true;
            deathMessage.text = "You Died\nPress \"Space\" to try again.";
        }
        if (isDead)
        {
            if(Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }
        
        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
        
        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        
        

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

        

        controller.Move(transform.TransformDirection(moveDirection) * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Collider collider = hit.collider;
        
        if (collider.tag.Equals("Kaas"))
        {
            eatSound.Play();
            
            // Display Current Score
            String text = counter.text;
            int textNum = 0;
            int.TryParse(text, out textNum);
            textNum++;
            counter.text = textNum.ToString();
            moveSpeed += 0.1f;
            Destroy(collider.gameObject);
            StoreHighscore(textNum);
        }

        if (collider.tag.Equals("Die"))
        {
            isDead = true;
            deathMessage.text = "You Died\nPress \"Space\" to try again.";
        }
    }
    
    void StoreHighscore(int newHighscore)
    {
        int oldHighscore = PlayerPrefs.GetInt("highscore", 0);
        if (newHighscore > oldHighscore)
        {
            PlayerPrefs.SetInt("highscore", newHighscore);
            PlayerPrefs.Save();
            highscoreCounter.text = newHighscore.ToString();
        }


    }

    public static PlayerController getInstance() => _instance;
}


