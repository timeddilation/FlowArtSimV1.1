using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControls : MonoBehaviour
{
    [Header("Menu Open and Close")]
    public float menuCollapseSpeed = 5f;
    public float collapsedMenuBufferSize = 150;

    [Header("Static Unity Objects")]
    public ScrollRect scrollbarRect;
    public Scrollbar scrollbar;
    public Sprite popMenuCaretUp;
    public Sprite popMenuCaretDown;
    public Button popMenuButton;
    private float hieghtBuffer;
    private Vector2 collapseDestination;
    private Vector2 expandDestination;

    private RectTransform myRectTransform;
    private bool resizingWindow = false;
    private bool isCollapsed = false;
    private bool scrollMenuHidden = false;

    private void Start()
    {
        myRectTransform = gameObject.GetComponent<RectTransform>();
        hieghtBuffer = myRectTransform.rect.height - collapsedMenuBufferSize;
        collapseDestination = new Vector2(myRectTransform.offsetMin.x, hieghtBuffer);
        expandDestination = myRectTransform.offsetMin;
    }

    private void Update()
    {
        if (resizingWindow) { PopMenu(); }
    }

    public void PopMenu()
    {
        resizingWindow = true;

        if (isCollapsed)
        {
            popMenuButton.image.sprite = popMenuCaretUp;
            scrollMenuHidden = false;
            ToggleScrollMenu();

            Vector2 smoothTransition = Vector2.Lerp(myRectTransform.offsetMin, expandDestination, Time.deltaTime * menuCollapseSpeed);
            myRectTransform.offsetMin = smoothTransition;

            if (myRectTransform.offsetMin.y < 0.15f)
            {
                isCollapsed = false;
                resizingWindow = false;
            }
        }
        else
        {
            popMenuButton.image.sprite = popMenuCaretDown;

            Vector2 smoothTransition = Vector2.Lerp(myRectTransform.offsetMin, collapseDestination, Time.deltaTime * menuCollapseSpeed);
            myRectTransform.offsetMin = smoothTransition;

            if ((hieghtBuffer - myRectTransform.offsetMin.y) < 0.15f)
            {
                isCollapsed = true;
                resizingWindow = false;
            }
            //hide menu and scroll bar slightly before menu has finished collapsing
            else if ((hieghtBuffer - myRectTransform.offsetMin.y) < 5f)
            {
                scrollMenuHidden = true;
                ToggleScrollMenu();
            }
        }
    }

    public void ToggleScrollMenu()
    {
        if (!scrollMenuHidden)
        {
            scrollbarRect.enabled = true;
            scrollbar.gameObject.SetActive(true);
        }
        else
        {
            scrollbarRect.enabled = false;
            scrollbar.gameObject.SetActive(false);
        }
    }
}
