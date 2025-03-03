using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameSettings : INotifyBindablePropertyChanged
{
    public static GameSettings Instance { get; private set; } = null!;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void RuntimeInitializeOnLoad() => Instance = new GameSettings();


    public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;
}
