using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSequence : MonoBehaviour
{
    public TextReveal textReveal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (textReveal.lineNumber == 2 && Camera.main.GetComponent<ClickEvent>().enabled == false) // Enable clicking for player
            Camera.main.GetComponent<ClickEvent>().enabled = true;
        else if (textReveal.lineNumber == 5 && Camera.main.GetComponent<AreaMovement>().enabled == false)
            Camera.main.GetComponent<AreaMovement>().enabled = true;
    }
}
