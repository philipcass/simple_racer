using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MenuPage : BPage {
    
    List<FButton> menuElements = new List<FButton>();
    
    override public void Start() {
        
        FSprite leftSide = new FSprite(Futile.whiteElement);
        leftSide.color = Color.black;
        leftSide.height = Futile.screen.height;
        leftSide.width = Futile.screen.width * 0.66f;
        
        FSprite rightSide = new FSprite(Futile.whiteElement);
        rightSide.height = Futile.screen.height;
        rightSide.width = Futile.screen.width * 0.33f;
        rightSide.scaleX *= 2;
        rightSide.scaleY *= 2;
        
        
        rightSide.x += Futile.screen.halfWidth;
        rightSide.rotation += 22.5f;

        this.AddChild(leftSide);
        this.AddChild(rightSide);
        setupMenu();
    }
 
    FSprite selector = new FSprite(Futile.whiteElement.name);
    public void setupMenu() {
        FContainer menuSelector = new FContainer();
        menuSelector.x = Futile.screen.width * 0.3f;
        menuSelector.y = -Futile.screen.height * 0.2f;
        
        selector.color = Color.black;
        selector.height = 48;
        selector.width = Futile.screen.width*2;
        menuSelector.AddChild(selector);
        
        FButton toAdd;
        
        toAdd = addButton("Play");
        menuSelector.AddChild(toAdd);
        toAdd = addButton("Options");
        toAdd.y -= 48;
        menuSelector.AddChild(toAdd);
        toAdd = addButton("Exit");
        toAdd.y -= 96;
        menuSelector.AddChild(toAdd);

        this.AddChild(menuSelector);
        menuElements[0].label.color = Color.white;
    }
    
    FButton addButton(string name) {
        FButton button = new FButton(Futile.whiteElement.name);
        button.AddLabel("Abstract", name, Color.black);
        button.sprite.alpha = 0;
        button.scale *= 2.5f;
        menuElements.Add(button);
        return button;
    }
    
    override public void HandleAddedToStage() {
        Futile.instance.SignalUpdate += HandleUpdate;
        base.HandleAddedToStage();  
    }

    override public void HandleRemovedFromStage() {
        Futile.instance.SignalUpdate -= HandleUpdate;
        base.HandleRemovedFromStage();  
    }

    int menuIndex = 0;
    bool inputEnabled = true;

    void HandleUpdate() {
        if(true){
            if(Input.GetKeyUp(KeyCode.S) && menuIndex < menuElements.Count -1) {
                menuElements[menuIndex].label.color = Color.black;
                menuIndex++;
                inputEnabled = false;
                Go.killAllTweensWithTarget(selector);
                Go.to(selector, 0.33f, new TweenConfig().setEaseType(EaseType.QuadOut).floatProp("y", menuElements[menuIndex].y).onComplete(hurr));
            } else if(Input.GetKeyUp(KeyCode.W) && menuIndex > 0) {
                menuElements[menuIndex].label.color = Color.black;
                menuIndex--;
                inputEnabled = false;
                Go.killAllTweensWithTarget(selector);
                Go.to(selector, 0.33f, new TweenConfig().setEaseType(EaseType.QuadOut).floatProp("y", menuElements[menuIndex].y).onComplete(hurr));
            }
            
            if(Input.GetKeyUp(KeyCode.Space)) {
                inputEnabled = false;
                Go.to(selector, 1, new TweenConfig().setEaseType(EaseType.QuadOut).colorProp("color", Color.white).floatProp("height", Futile.screen.height*2).onComplete(hurr));
                menuElements.ForEach(delegate(FButton b)
                {
                    b.label.color = Color.white;
                });
            }
        }
    }

    void hurr (AbstractTween hurr)
    {
        menuElements[menuIndex].label.color = Color.white;
        inputEnabled = true;
    }
}