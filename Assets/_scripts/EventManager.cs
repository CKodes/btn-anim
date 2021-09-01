using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class EventManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // Button GameObject
    [SerializeField] private Button controller;

    // Images - For Fade & Fill Anims
    [SerializeField] private Image circleMaskImg;
    [SerializeField] private Image checkImg;
    [SerializeField] private Image greenCheckImg;
    [SerializeField] private Image arrowImg;
    [SerializeField] private Image loadingCircleImg;

    // Transforms - For Anims Along Y-axis
    [SerializeField] private Transform btnTrsfm;
    [SerializeField] private Transform checkTrsfm;
    [SerializeField] private Transform arrowTrsfm;
    [SerializeField] private Transform waitingTrsfm;
    [SerializeField] private Transform activateTrsfm;
    [SerializeField] private Transform activatedTrsfm;

    // Anim Y-Axis Endpoints
    [SerializeField] private int moveUp; // y:600
    [SerializeField] private int moveDown; //y:475
    [SerializeField] private int moveCenter; //y:540

    // Anim Duration
    [SerializeField] private float textAnimTime; //0.7f
    [SerializeField] private float imgAnimTime; //0.3f

    // Color States
    [SerializeField] private Color activatedColor; // 36D66B
    [SerializeField] private Color normalColor; // 4B21EB
    [SerializeField] private Color currentColor;

    void Start()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        currentColor = normalColor;
        controller.GetComponent<Image>().color = currentColor;
    }

    // Hover-on, call EntHoverAnim method
    public void OnPointerEnter(PointerEventData eventData)
    {
        Backend.onEntHover += EntHoverAnim;
        Backend.onExtHover -= ExtHoverAnim;
    }

    // Hover-off, call ExtHoverAnim method
    public void OnPointerExit(PointerEventData eventData)
    {
        Backend.onEntHover -= EntHoverAnim;
        Backend.onExtHover += ExtHoverAnim;
    }

    // Click down, call ClickDownAnim
    public void OnPointerDown(PointerEventData eventData)
    {
        Backend.onPress += ClickDownAnim;
    }

    // Click release, call ClickReleaseAnim
    public void OnPointerUp(PointerEventData eventData)
    {
        Backend.onRelease += ClickReleaseAnim;
    }

    // Hover-On Anim
    void EntHoverAnim()
    {
        if (currentColor == normalColor)
        {
            arrowTrsfm.DOMoveY(moveUp, imgAnimTime);
            checkTrsfm.DOMoveY(537, imgAnimTime);
        }
    }

    // Hover-Off Anim
    void ExtHoverAnim ()
    {
        if (currentColor == normalColor)
        {
            arrowTrsfm.DOMoveY(moveCenter, imgAnimTime);
            checkTrsfm.DOMoveY(moveDown, imgAnimTime);
        }
    }

    // ClickDownAnim
    void ClickDownAnim ()
    {
        if (currentColor == normalColor)
        {
            //Btn scales down
            btnTrsfm.DOScale(new Vector3(0.97f, 0.97f, 0.97f), 0.1f);
        }
    }

    // ClickReleaseAnim
    void ClickReleaseAnim()
    {
        if (currentColor == normalColor)
        {   
            //Btn scales to original
            btnTrsfm.DOScale(new Vector3(1f, 1f, 1f), 0.1f);

            checkTrsfm.DOMoveY(moveDown, imgAnimTime);
            checkImg.DOFade(0, 0.1f).SetEase(Ease.Flash);
            arrowImg.DOFade(0, 0.1f).SetEase(Ease.Flash);
            circleMaskImg.DOFade(0, imgAnimTime).SetEase(Ease.Flash);

            checkTrsfm.DOMoveY(moveDown, imgAnimTime);
            checkImg.DOFade(0, 0.1f).SetEase(Ease.Flash);
            arrowImg.DOFade(0, 0.1f).SetEase(Ease.Flash);
            circleMaskImg.DOFade(0, imgAnimTime).SetEase(Ease.Flash);

            //Text Anim
            activateTrsfm.DOMoveY(moveUp, textAnimTime);
            waitingTrsfm.DOMoveY(moveCenter, textAnimTime);

            loadingCircleImg.DOFade(1, 0.3f).SetEase(Ease.Flash);
            loadingCircleImg.DOFillAmount(0, 3f).OnComplete(() =>
            {
                // Post-Load Completion Anim
                currentColor = activatedColor;
                controller.GetComponent<Image>().color = currentColor;
                waitingTrsfm.DOMoveY(moveUp, textAnimTime);
                activatedTrsfm.DOMoveY(moveCenter, textAnimTime);
                circleMaskImg.DOFade(1, imgAnimTime).SetEase(Ease.Flash);
                greenCheckImg.DOFade(1, imgAnimTime).SetEase(Ease.Flash);
            });
        }
    }
};