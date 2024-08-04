﻿using CommunityToolkit.Mvvm.Messaging.Messages;
using SukiUI.Enums;

namespace Hermes.Common.Messages;

public class ShowToastMessage(string title, string message)
    : ValueChangedMessage<string>(message)
{
    public double Duration { get; set; } = 8;
    public string Title => title;
};