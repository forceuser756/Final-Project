using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject effect;
    public float speed;
    public Text countText;
    public Text winText;
    public Text CollectText;
    public Text endText;

    private Rigidbody2D rb2d;
    private int count;
    private float timer;
    private int wholetime;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        winText.text = "";
        SetCountText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce (movement * speed);
        if (Input.GetKey("escape"))
            Application.Quit();
        timer = timer + Time.deltaTime;
        if (timer >= 10)
        {
            endText.text = "You Lose! :(";
            StartCoroutine(ByeAfterDelay(2));

        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            other.gameObject.SetActive (false);
            count = count + 1;
            //GameLoader.AddScore(1);
            SetCountText();
        }  
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 10)
        {
            winText.text = "You win!";
            StartCoroutine(ByeAfterDelay(2));
        }
    }
    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        
        //GameLoader.gameOn = false;
    }
}
