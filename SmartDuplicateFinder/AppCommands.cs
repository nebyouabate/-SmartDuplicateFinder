﻿using System;
using System.Windows.Input;

namespace SmartDuplicateFinder;

public static class AppCommands
{
    private static readonly Type OwnerType = typeof(AppCommands);
    // menu item command
    //
    public static readonly RoutedUICommand Exit = new("_Exit", "Exit", OwnerType, new InputGestureCollection(new[] { new KeyGesture(Key.F4, ModifierKeys.Alt) }));
    public static readonly RoutedUICommand AboutHelp = new("About...", "AboutHelp", OwnerType);

    // Standard dialog box command
    //
    public static readonly RoutedUICommand Okay = new("Ok", "Ok", OwnerType);
    public static readonly RoutedUICommand Cancel = new("Cancel", "Cancel", OwnerType);
    public static readonly RoutedUICommand Close = new("Close", "Close", OwnerType);

    // Other types of buttons
    //
    public static readonly RoutedUICommand Refresh = new("_Refresh", "Refresh", OwnerType);
    public static readonly RoutedUICommand ClearAll = new("Clear _All", "Clear", OwnerType);
}
