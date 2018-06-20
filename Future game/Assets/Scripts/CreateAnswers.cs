using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAnswers : MonoBehaviour {

    [SerializeField]
    private GameObject ScrollView;
    private ScrollRect scrollRect;
    [SerializeField]
    private Text AnswerText;
    private Text[] AnsArray;
    DialogueSystem ds;
    private RectTransform RectScrollView;
    private bool IsScrolling;
    private Vector3 vectorsmoothpanel;
    [SerializeField]
    [Range(0, 10)]
    private float smoothSpeedScroll;


    void Awake () {

        RectScrollView = GetComponent<RectTransform>();
        scrollRect = ScrollView.GetComponent<ScrollRect>();
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

            AnsArray[i].gameObject.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            AnsArray[i].alignment = TextAnchor.UpperLeft;

        }
    }

    public void Scroll(bool scroll)
    {
        IsScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }

    void FixedUpdate()
    {
        if (RectScrollView.anchoredPosition.y <= AnsArray[0].rectTransform.rect.y || 
            RectScrollView.anchoredPosition.y >= AnsArray[AnsArray.Length - 1].rectTransform.rect.y)
        {
            IsScrolling = false;
            scrollRect.inertia = false;
        }

        float scrollvelocity = Mathf.Abs(scrollRect.velocity.y);
        if (scrollvelocity < 400f && !IsScrolling) scrollRect.inertia = false;

        if (IsScrolling || scrollvelocity > 400f) return;
        vectorsmoothpanel.y = Mathf.SmoothStep(RectScrollView.anchoredPosition.y, AnsArray[2].rectTransform.rect.y, smoothSpeedScroll * Time.fixedDeltaTime);
        RectScrollView.anchoredPosition = vectorsmoothpanel;

        Debug.Log(AnsArray[0].gameObject.transform.position.y);
        Debug.Log(AnsArray[AnsArray.Length - 1].transform.position.y);
    }

    
}  

    

