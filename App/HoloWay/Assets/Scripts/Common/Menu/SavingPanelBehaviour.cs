using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SavingPanelBehaviour : MonoBehaviour
{
    public float RotationSpeedMultiplier = 360.0f;
    public Transform SavingIcon;
    public TMP_Text SavingIconText;
    public float currentStartTime;
    public float totalTimeToHide;
    public bool shouldHidePanelAfterXSeconds = false;
    private bool IsShowingPanel = false;
    // Start is called before the first frame update
    void Start()
    {
        this.HidePanel();
        this.SavingIconText.text = "Saving";
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldHidePanelAfterXSeconds)
        {
            if(Time.time - currentStartTime > totalTimeToHide)
            {
                this.HidePanel();
                shouldHidePanelAfterXSeconds = false;
            }
        }
        IsShowingPanel = this.gameObject.activeSelf;
        if (IsShowingPanel)
        {
            SavingIcon.Rotate(new Vector3(0.0f, 0.0f, -RotationSpeedMultiplier * Time.deltaTime));
        }
    }
    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }
    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }
    public void ShowPanel(float totalTimeToHide)
    {
        this.currentStartTime = Time.time;
        this.shouldHidePanelAfterXSeconds = true;
        this.totalTimeToHide = totalTimeToHide;
        this.gameObject.SetActive(true);

    }
    public bool IsShown()
    {
        return this.gameObject.activeSelf;
    }
}
