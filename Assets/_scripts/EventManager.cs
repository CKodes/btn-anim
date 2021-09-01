using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class EventManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        currentColor = normalColor;
        controller.GetComponent<Image>().color = currentColor;
    }

    // HoverEnter Anim
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentColor == normalColor)
        {
            arrowTrsfm.DOMoveY(moveUp, imgAnimTime);
            checkTrsfm.DOMoveY(537, imgAnimTime).SetEase(Ease.InBounce);
        }
        else
        {
            currentColor = currentColor;
        }
    }

    // HoverExit Anim
    public void OnPointerExit(PointerEventData eventData)
    {
        if(currentColor == normalColor)
        {
            arrowTrsfm.DOMoveY(moveCenter, imgAnimTime);
            checkTrsfm.DOMoveY(moveDown, imgAnimTime);
        }
        else
        {
            currentColor = currentColor;
        }
    }

    // Clicked Anim
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            checkImg.DOFade(0, 0.1f).SetEase(Ease.Flash).SetAutoKill(true);
            arrowImg.DOFade(0, 0.1f).SetEase(Ease.Flash).SetAutoKill(true);
            circleMaskImg.DOFade(0, imgAnimTime).SetEase(Ease.Flash);
            btnTrsfm.DOScale(new Vector3(0.99f, 0.99f, 0.99f), 0.1f).SetLoops(2, LoopType.Yoyo);

            //Text Anim
            activateTrsfm.DOMoveY(moveUp, textAnimTime).SetAutoKill(true);
            waitingTrsfm.DOMoveY(moveCenter, textAnimTime);

            //Loading Circle Anim
            loadingCircleImg.DOFade(1, 0.3f).SetEase(Ease.Flash);
            loadingCircleImg.DOFillAmount(0, 1.5f).SetLoops(loops:3).OnComplete(() =>
            {
                // Post-Load Completion Anim
                currentColor = activatedColor;
                controller.GetComponent<Image>().color = currentColor;
                waitingTrsfm.DOMoveY(moveUp, textAnimTime).SetAutoKill(true);
                activatedTrsfm.DOMoveY(moveCenter, textAnimTime);
                circleMaskImg.DOFade(1, imgAnimTime).SetEase(Ease.Flash);
                greenCheckImg.DOFade(1, imgAnimTime).SetEase(Ease.Flash);
            });
        }
    }
}