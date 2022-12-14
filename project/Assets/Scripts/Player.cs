using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public TMP_InputField input;
    public TMP_Text correct;
    public TMP_Text incorrect;
    public TMP_Text health;
    public TMP_Text state_text;
    public float jumpBoost;
    public float vol;
    private int correctCount = 0;
    private int incorrectCount = 0;
    private int healthCount = 100;
    private Hashtable capitals = new Hashtable();

    private float defaultJumpHeight;
    private float boostedJumpHeight;
    
    // Sound Effects
    public AudioClip fallingToDeath; // Mario "Waaaaahhhhh"
    public AudioClip takeDamage; // Mario ooooff or damage sound from mario
    public AudioClip guessedCorrectly; // Mario red coin sound
    public AudioClip guessedIncorrectly; // Mario oof sound
    public AudioClip alienShot;

    public AudioSource source;

    private bool fallingPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        capitals.Add("Alabama", "Montgomery");
        capitals.Add("Alaska", "Juneau");
        capitals.Add("Arizona", "Phoenix");
        capitals.Add("Arkansas", "Little Rock");
        capitals.Add("California", "Sacramento");
        capitals.Add("Colorado", "Denver");
        capitals.Add("Connecticut", "Hartford");
        capitals.Add("Delaware", "Dover");
        capitals.Add("Florida", "Tallahassee");
        capitals.Add("Georgia", "Atlanta");
        capitals.Add("Hawaii", "Honolulu");
        capitals.Add("Idaho", "Boise");
        capitals.Add("Illinois", "Springfield");
        capitals.Add("Indiana", "Indianapolis");
        capitals.Add("Iowa", "Des Moines");
        capitals.Add("Kansas", "Topeka");
        capitals.Add("Kentucky", "Frankfort");
        capitals.Add("Louisiana", "Baton Rouge");
        capitals.Add("Maine", "Augusta");
        capitals.Add("Maryland", "Annapolis");
        capitals.Add("Massachusetts", "Boston");
        capitals.Add("Michigan", "Lansing");
        capitals.Add("Minnesota", "Saint Paul");
        capitals.Add("Mississippi", "Jackson");
        capitals.Add("Missouri", "Jefferson City");
        capitals.Add("Montana", "Helena");
        capitals.Add("Nebraska", "Lincoln");
        capitals.Add("Nevada", "Carson City");
        capitals.Add("New Hampshire", "Concord");
        capitals.Add("New Jersey", "Trenton");
        capitals.Add("New Mexico", "Santa Fe");
        capitals.Add("New York", "Albany");
        capitals.Add("North Carolina", "Raleigh");
        capitals.Add("North Dakota", "Bismarck");
        capitals.Add("Ohio", "Columbus");
        capitals.Add("Oklahoma", "Oklahoma City");
        capitals.Add("Oregon", "Salem");
        capitals.Add("Pennsylvania", "Harrisburg");
        capitals.Add("Rhode Island", "Providence");
        capitals.Add("South Carolina", "Columbia");
        capitals.Add("South Dakota", "Pierre");
        capitals.Add("Tennessee", "Nashville");
        capitals.Add("Texas", "Austin");
        capitals.Add("Utah", "Salt Lake City");
        capitals.Add("Vermont", "Montpelier");
        capitals.Add("Virginia", "Richmond");
        capitals.Add("Washington", "Olympia");
        capitals.Add("West Virginia", "Charleston");
        capitals.Add("Wisconsin", "Madison");
        capitals.Add("Wyoming", "Cheyenne");

        defaultJumpHeight = gameObject.GetComponent<StarterAssets.ThirdPersonController>().JumpHeight;
        boostedJumpHeight = defaultJumpHeight + jumpBoost;

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health.text.Substring(8) != healthCount.ToString())
        {
            healthCount = int.Parse(health.text.Substring(8));
        }
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Vector3.down, out hit, 10f))
        {
            state_text.text = hit.collider.name == "PlayerArmature" ? "" : hit.collider.name;
            GameObject floor = hit.collider.gameObject;

            if (floor != null && hit.collider.name != "PlayerArmature") 
            {
                if (floor.GetComponent<Renderer>().material.color == Color.green)
                {
                    if(floor.tag == "boost") 
                    {
                        gameObject.GetComponent<StarterAssets.ThirdPersonController>().JumpHeight = boostedJumpHeight;
                    } 
                }
                else
                {
                    gameObject.GetComponent<StarterAssets.ThirdPersonController>().JumpHeight = defaultJumpHeight;
                }
            }
        }
        else
        {
            state_text.text = "";
        }

        if(this.gameObject.transform.position.y < 0) 
        {
            if(!fallingPlaying) 
            {
                source.PlayOneShot(fallingToDeath, vol);
                fallingPlaying = true;
            }
        }

        if (healthCount <= 0 || this.gameObject.transform.position.y < -20)
        {
            SceneManager.LoadScene("GameOver");
        }else if(correctCount == 50)
        {
            SceneManager.LoadScene("Winner");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            input.ActivateInputField();
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            input.DeactivateInputField();
            Time.timeScale = 1;

            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Vector3.down, out hit, 10f))
            {
                string state = hit.collider.gameObject.name;
                string capital = input.text.ToLower();
                if (capitals[state].ToString().ToLower() == capital)
                {
                    if (hit.collider.gameObject.GetComponent<Renderer>().material.color == Color.green)
                    {
                        return;
                    }
                    correctCount++;
                    correct.text = "States Correct: " + correctCount;
                    healthCount = Mathf.Min(healthCount + 10, 100);
                    health.text = "Health: " + healthCount;
                    hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;

                    source.PlayOneShot(guessedCorrectly, vol);
                }
                else
                {
                    incorrectCount++;
                    incorrect.text = "States Incorrect: " + incorrectCount;
                    healthCount = Mathf.Max(healthCount - 10, 0);
                    health.text = "Health: " + healthCount;

                    source.PlayOneShot(guessedIncorrectly, vol);
                }
            }
        }
    }
}
