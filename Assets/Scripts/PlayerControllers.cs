using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllers : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playderAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtSplatterParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public Button replayButton;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playderAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && gameOver == false) 
        {
            playerRb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtSplatterParticle.Stop();
            playderAudio.PlayOneShot(jumpSound, 1f);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtSplatterParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("Game Over");
            GameOver();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtSplatterParticle.Stop();
            playderAudio.PlayOneShot(crashSound, 1f);
        }
    }
    private void GameOver()
    {
        gameoverText.gameObject.SetActive(true);
        replayButton.gameObject.SetActive(true);
        gameOver = true;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
