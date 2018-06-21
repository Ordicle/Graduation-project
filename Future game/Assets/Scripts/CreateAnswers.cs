using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAnswers : MonoBehaviour {

    [SerializeField]
    private Text AnswerText;
    private Text[] AnsArray;
    DialogueSystem ds;
    [Range(0,60)]
    [SerializeField]
    private float GabBetweenPrefabs;
    private RectTransform RectScrollView;


    void Awake () {

        AnswerText = AnswerText.GetComponent<Text>();
        ds = GameObject.Find("Interface").GetComponent<DialogueSystem>();
    }

    public void ShowAnswer()
    {

        if (AnsArray != null)
        {

            for (int n = 0; n < AnsArray.Length; n++)
            {
                Destroy(AnsArray[n].gameObject);
            }

            AnsArray = null;
        }

        AnsArray = new Text[ds.dialogueSetting.node[ds.i].answers.Length];

        for (int i = 0; i < ds.dialogueSetting.node[ds.i].answers.Length; i++)
        {
            AnsArray[i] = Instantiate(AnswerText, transform, false) as Text;

            AnsArray[i].GetComponent<ButtonManager>().end = "";
            AnsArray[i].text = ds.dialogueSetting.node[ds.i].answers[i].anstext;
            AnsArray[i].GetComponent<ButtonManager>().curI = ds.dialogueSetting.node[ds.i].answers[i].NValue;
            AnsArray[i].GetComponent<ButtonManager>().NumButton = i;

          

            if (i == 0)
            {
                AnsArray[i].GetComponent<RectTransform>().position = new Vector2(gameObject.GetComponent<RectTransform>().position.x,
                    gameObject.GetComponent<RectTransform>().position.y + 75);
            }
            else
            {
                AnsArray[i].GetComponent<RectTransform>().position = new Vector2(AnsArray[0].GetComponent<RectTransform>().position.x,
                    AnsArray[i - 1].GetComponent<RectTransform>().position.y - (AnsArray[i - 1].GetComponent<RectTransform>().sizeDelta.y) /*GabBetweenPrefabs*/);
            }
 

          AnsArray[i].alignment = TextAnchor.UpperLeft;

        }
    }
}