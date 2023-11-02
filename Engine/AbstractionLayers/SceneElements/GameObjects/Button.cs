using Engine.SceneElements.Components;
using System;
using System.Collections.Generic;

namespace Engine.SceneElements.GameObjects;

public class Button : ComponentHandler
{
    public Button()
        : this(Array.Empty<ComponentHandler>()) { }

    public Button(IEnumerable<ComponentHandler> children)
        : base(new Component[] { new TransformComponent() }, children) { }

    public void EnteredIvoke() => OnEntered?.Invoke();

    public void ExitedIvoke() => OnExited?.Invoke();

    public void PressedIvoke() => OnPressed?.Invoke();

    public void ReleasedIvoke() => OnReleased?.Invoke();

    public void ClickedIvoke() => OnClicked?.Invoke();

    public event Action OnEntered;
    public event Action OnExited;
    public event Action OnPressed;
    public event Action OnReleased;
    public event Action OnClicked;
}
