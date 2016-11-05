using UnityEngine;
using System.Collections;

public class scr_MovingBloodTrail : MonoBehaviour
{
    public float copyTimer = 0.0f;                  // Copy of the timer for deug purposes
    public float timer = 0.0f;                      
    public float startBloodTimer = 0.1f;
    public float offsetBloodChange = -0.6f;
    public bool isMovingBlood = false;
    public bool stopTimer = true;
    public bool reset = false;
    public GameObject bloodTrailTile;
    AudioSource hauntedWalking;
    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        hauntedWalking = gameObject.GetComponent<AudioSource>();
        timer = 0.0f;
        stopTimer = true;
        copyTimer = startBloodTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 7.0f)
        {
            if (!isMovingBlood)
            {
                if (bloodTrailTile.GetComponent<scr_TileV2>().occupiedBy != null)
                {
                    if (bloodTrailTile.GetComponent<scr_TileV2>().occupiedBy.name == "Player")
                    {
                        stopTimer = false;
                        isMovingBlood = true;
                    }
                } 
            }

            if (!stopTimer)
            {
                timer += Time.deltaTime;

                if (timer >= startBloodTimer)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = true;
                    hauntedWalking.Stop();
                    hauntedWalking.Play();
                    stopTimer = true;
                }
            }
        }

        if (reset)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            startBloodTimer = copyTimer;
            isMovingBlood = false;
            timer = 0.0f;
            stopTimer = false;
            reset = false;
        }
    }
}
