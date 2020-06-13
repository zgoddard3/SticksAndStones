using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTransition : MonoBehaviour
{
	public Vector2 teleportPoint;
	public float fadeFactor = 0.2f;
	public CanvasGroup canvasGroup;

	private GameObject playerController;
	private bool inBounds = false;
	private AudioSource openDoor;
    
	void Awake()
    {
		playerController = GameObject.Find("MovePoint");
		canvasGroup = GameObject.Find("BlackoutImage").GetComponent<CanvasGroup>();
		openDoor = GetComponent<AudioSource>();
	}
		
    public IEnumerator Blackout()
    {
        yield return StartCoroutine("DoFade");
        yield return StartCoroutine("MovePlayer");
        yield return new WaitForSeconds(0.5f);
        Reload();
    }

    void Reload()
    {
        StartCoroutine(EndFade());
    }

    IEnumerator DoFade()
    {
		openDoor.Play();
		while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return new WaitForSeconds(fadeFactor);//pauses to run coroutine again next Frame
        }
    }
	IEnumerator MovePlayer()
    {
		playerController.transform.position = teleportPoint;
			//Vector2.MoveTowards(playerController.transform.position, teleportPoint, 50);
		PlayerMovement.teleporting = true;
        yield return null;
    }
    IEnumerator EndFade()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return new WaitForSeconds(fadeFactor);//pauses to run coroutine again next Frame
        }
		PlayerMovement.teleporting = false;
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		inBounds = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		inBounds = false;
	}
   
	private void Update()
    {
		if (inBounds && Input.GetKeyDown(KeyCode.Z)) {
			Debug.Log("switching locations");
			StartCoroutine(Blackout());
		}

		/*if (Input.GetKeyDown(KeyCode.T))//testing for blackout
        {
            StartCoroutine(Blackout());
        }
        else if (Input.GetKeyDown(KeyCode.Y))//testing for night effect on stats
        {
            TimeProgression.Instance.TransitionToNight();
        }*/
    }
}
