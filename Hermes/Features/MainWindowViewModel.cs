﻿using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hermes.Common.Messages;
using Hermes.Models;
using SukiUI;
using System.Collections.Generic;
using System.Linq;

// Alias para la clase Settings
using SettingsClass = Hermes.Models.Settings;

namespace Hermes.Features
{
    public partial class MainWindowViewModel : ViewModelBase, IRecipient<NavigateMessage>
    {
        public IAvaloniaReadOnlyList<PageBase> Pages { get; }
        [ObservableProperty] private ThemeVariant? _baseTheme;
        [ObservableProperty] private bool _titleBarVisible;
        [ObservableProperty] private PageBase? _activePage;

        private readonly SukiTheme _theme;

        
        public MainWindowViewModel(IEnumerable<PageBase> pages, SettingsClass settings)
        {
            this.Pages = new AvaloniaList<PageBase>(pages.OrderBy(x => x.Index).ThenBy(x => x.DisplayName));
            this._theme = SukiTheme.GetInstance();
            this._theme.ChangeBaseTheme(ThemeVariant.Light);
            this.UpdateBaseTheme();
            if (settings.AutostartUutProcessor)
            {
                Messenger.Send(new StartUutProcessorMessage());
            }
        }
        

        [RelayCommand]
        private void ToggleBaseTheme()
        {
            this._theme.SwitchBaseTheme();
            this.UpdateBaseTheme();
        }

        private void UpdateBaseTheme()
        {
            this.BaseTheme = _theme.ActiveBaseTheme == ThemeVariant.Dark ? ThemeVariant.Light : ThemeVariant.Dark;
        }

        [RelayCommand]
        private void ToggleTitleBar()
        {
            TitleBarVisible = !TitleBarVisible;
            Messenger.Send(new ShowToastMessage(
                $"Title Bar {(TitleBarVisible ? "Visible" : "Hidden")}",
                $"Window title bar has been {(TitleBarVisible ? "shown" : "hidden")}."
            ));
        }

        [RelayCommand]
        private void Exit(Window window)
        {
            Messenger.Send(new ExitMessage());
        }

        [RelayCommand]
        private void ShowSettings(Window window)
        {
            Messenger.Send(new ShowSettingsMessage());
        }

        [RelayCommand]
        private void ShowSnackbar()
        {
            Messenger.Send(new ShowToastMessage("Hello!", "This is a snackbar!"));
        }

        public void Receive(NavigateMessage message)
        {
            var pageType = message.Value.GetType();
            var page = Pages.FirstOrDefault(x => x.GetType() == pageType);
            if (page is null || this.ActivePage?.GetType() == pageType) return;
            this.ActivePage = page;
        }
    }
}