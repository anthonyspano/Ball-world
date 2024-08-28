using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupGem : MonoBehaviour
{
    public Text counterUI;
    private int CoinCount = 0;
    
    public AudioClip pickupSound;

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Gem"))
        {
            // play sound
            GetComponent<AudioSource>().PlayOneShot(pickupSound, 0.7f);

            // spin ui gem
            StartCoroutine(SpinGemIcon());

            // add to ui counter
            CoinCount += 1;
            counterUI.text = CoinCount.ToString();

            // add to player gui
            Destroy(col.gameObject);
        }
    }

    public Transform GemIcon; 
    private float spinTimer;
    public float maxSpinTime;
    public float rotateSpeed;

    private IEnumerator SpinGemIcon()
    {
        spinTimer = 0;
        
        while(!DoneSpinning())
        {
            GemIcon.localEulerAngles+=new Vector3(0, rotateSpeed * Time.deltaTime, 0);
            yield return null;
        }

        // reset gem to normal position
        GemIcon.localEulerAngles = Vector3.zero;

    }

    private bool DoneSpinning()
    {
        if(spinTimer < maxSpinTime)
        {
            spinTimer += Time.deltaTime;
            return false;
        }

        return true;
    }
}