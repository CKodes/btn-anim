# Modifications

Progress list:

| Status | Modification |
| ------ | ------ |
| ✓ | Rounded Buttons |
| ✓ | Event-based script|
|  WIP | Loading spinner |


Refer to [GitHub Repo] for commit details

## Added Backend.cs

Created the following delegates so that events from `EventManager.cs` can be called

```js
    // Hover on
    public delegate void OnEnterHover();
    public static event OnEnterHover onEntHover;
    
    // Hover off
    public delegate void OnExitHover();
    public static event OnExitHover onExtHover;
    
    // Click Down
    public delegate void OnClickDown();
    public static event OnClickDown onPress;

    //Click Release
    public delegate void OnClickRelease();
    public static event OnClickRelease onRelease;
```

## Refactored EventManager.cs

Refactored animations into events, for eg `EntHoverAnim` method for `onEntHover`:

```js
    public void OnPointerEnter(PointerEventData eventData)
    {
        Backend.onEntHover += EntHoverAnim;
        Backend.onExtHover -= ExtHoverAnim;
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
```

## Added New Btn Sprite

Added a new UI sprite `Rounded40px` with higher border-radius to make button look more rounded on the corners.

[//]: # 

   [GitHub Repo]: <https://github.com/CKodes/btn-anim>


