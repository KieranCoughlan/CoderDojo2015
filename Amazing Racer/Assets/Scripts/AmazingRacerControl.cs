using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class AmazingRacerControl : MonoBehaviour
{
	public GameObject player;
	public GameObject spawn;

  public AudioClip wooshSound;
  public AudioClip fanfareSound;

  public FirstPersonController fpsController;

  private Rigidbody playerRB;
  private AudioSource playerAudioSource;

  private bool gameStarted;
  private bool gameRunning;

  private float gameTime;
  private float bestTime;
  private bool bestTimeSet;

	// Use this for initialization
	void Start ()
	{
    // Disable the first player controller
    // and set the flags to indicate that the
    // game's never been started and that it's not
    // currenty running
    EnableFPSController (false);
    gameStarted = false;
    gameRunning = false;

    // Set the gametime to zero
    gameTime = 0.0f;

    // never yet set a best time
    bestTimeSet = false;

    // Grab the players rigidbody and audio source 
    playerRB = player.GetComponent<Rigidbody>();
    playerAudioSource = player.GetComponent<AudioSource> ();

    // Position the player
    ReturnPlayerToSpawn ();
	}
	
	// Update is called once per frame
	void Update ()
	{
    // If the game is running, update the game counter with
    // the time since the last frame
    if (gameRunning)
      gameTime += Time.deltaTime;
	}

	public void ReturnPlayerToSpawn ()
	{
		// Teleport the player back to the spawn
    // and play a whooshing noise
    playerRB.transform.position = spawn.transform.position;

    playerAudioSource.clip = wooshSound;
    playerAudioSource.Play ();
	}

  public void StartGame()
  {
    // Put the player at the starting position,
    // zero our timer, set the game running and enable
    // the FPS controller

    ReturnPlayerToSpawn ();

    gameTime = 0.0f;
    gameRunning = true;

    EnableFPSController (true);
  }

  public void EndGame()
  {
    // End the game. Play a fanfare. Stop it running, disable the FPS
    // contoller and set the best time

    playerAudioSource.clip = fanfareSound;
    playerAudioSource.Play ();

    gameRunning = false;
    EnableFPSController (false);

    // If we never set a best time before, any time
    // is our best time...
    if (bestTimeSet == false || gameTime < bestTime)
    {
      bestTime = gameTime;
      bestTimeSet = true;
    }
  }

  private void EnableFPSController(bool enable)
  {
    // Enable/Disable the FPS constroller to start/stop 
    // it moving and _also_ make it grab/release the mouse
    // (depending on the value of enable)

    fpsController.enabled = enable;
    fpsController.LockMouse(enable);
  }

	public void OnGUI ()
	{
    // We have three distinct GUI elements. We've put them
    // their own individual methods for clarity

    ShowGameTime ();

    ShowBestTime ();

    ShowStartRestartButtons ();
	}

  private void ShowGameTime()
  {
    // Display time
    string gameTimeLabel;
    Rect gameTimeRect = new Rect (10, 10, 100, 100);

    // If the game's currently running show the time, otherwise
    // put a dash
    if (gameRunning)
      gameTimeLabel = "Time: " + FormatTime(gameTime) + "s";
    else
      gameTimeLabel = "Time: -";

    // On the screen
    GUI.Label (gameTimeRect, gameTimeLabel);
  }

  private void ShowBestTime()
  {
    // Display best time
    string bestTimeLabel;
    Rect bestTimeRect = new Rect (Screen.width - 210, 10, 200, 100);

    // If we have a best time, show it, otherwise just put a dash
    if (bestTimeSet)
      bestTimeLabel = "Best Time: " + FormatTime(bestTime) + "s";
    else
      bestTimeLabel = "Best Time: -";

    // On the screen
    GUI.Label (bestTimeRect, bestTimeLabel);
  }

  private void ShowStartRestartButtons()
  {
    // Buttons to start or restart the game
    Rect buttonRect = new Rect (Screen.width / 2 - 70, 
                                Screen.height / 2, 
                                140, 80);

    // If the game's never been started, show the "Click to play"
    // button. If it's finished, show "Click to restart". If we're 
    // in the middle of a game, it won't show anything
    if (gameStarted == false)
    {
      if (GUI.Button (buttonRect, "Click to play"))
      {
        // Mark the game as started and get going!
        gameStarted = true;
        StartGame ();
      }
    }
    else if (gameRunning == false)
    {
      if (GUI.Button (buttonRect, "Click to restart"))
      {
        // Start the game!
        StartGame ();
      }
    } 
  }

  private string FormatTime(float value)
  {
    // Format a float into a string with two numbers
    // after the decimal point. Google .NET string formatting
    // codes to see the huge range of possibities when it comes
    // to formatting numbers
    return string.Format("{0:F2}", value);
  }
}