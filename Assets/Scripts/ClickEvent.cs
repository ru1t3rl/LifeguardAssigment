using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ClickEvent : MonoBehaviour
{
    public float raycastDistance = 200f;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.transform.tag == "Swimmer" && hit.transform.GetComponent<Swimmer>().state == SwimmerState.Drowning)
                {
                    hit.transform.DOMoveY(4f, 1f).OnComplete(() => Destroy(hit.transform.gameObject));
                    score += 10;
                    scoreText.text = "Score: " + score.ToString();
                }
                else if (hit.transform.tag == "Swimmer" && hit.transform.GetComponent<Swimmer>().state == SwimmerState.Swimming)
                {
                    Debug.Log(hit.transform.GetComponent<Swimmer>().state);
                    hit.transform.DOMoveY(4f, 1f);
                    score -= 5;
                    scoreText.text = "Score: " + score.ToString();
                }
            }
        }
    }
}