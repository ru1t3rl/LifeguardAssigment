using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextReveal : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    int totalVisibleChars, counter, visibleCount;
    public int lineNumber = 0;
    private bool endOfLine = false, typing = false;
    public string[] lines = new string[10];

    private void Update()
    {
        if (Input.GetKeyDown("space") && !typing)
        {
            endOfLine = false;
            textMesh.text = lines[lineNumber];
            textMesh.ForceMeshUpdate(true);
            if (lineNumber < 9)
                lineNumber++;
            else
                lineNumber = 0;

            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        totalVisibleChars = textMesh.textInfo.characterCount;
        counter = 0;
        typing = true;

        while (!endOfLine)
        {
            visibleCount = counter % (totalVisibleChars + 1);
            textMesh.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleChars)
            {
                endOfLine = true;
                typing = false;
            }

            counter += 1;

            yield return new WaitForSeconds(0.05f);
        }
    }
}