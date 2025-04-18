// MIT License
//
// Copyright (c) 2023-Present - Violet Hansen - (aka HotCakeX on GitHub) - Email Address: spynetgirl@outlook.com
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// See here for more information: https://github.com/HotCakeX/Harden-Windows-Security/blob/main/LICENSE
//

using System.IO;
using AppControlManager.Others;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;

namespace AppControlManager.Pages;

/// <summary>
/// Handles the selection and confirmation of a custom MSIX file path for updates. Updates UI elements based on user
/// interactions.
/// </summary>
internal sealed partial class UpdatePageCustomMSIXPath : Page
{

	private AppSettings.Main AppSettings { get; } = App.AppHost.Services.GetRequiredService<AppSettings.Main>();

	/// <summary>
	/// Initializes the UpdatePageCustomMSIXPath component and sets the initial state of the confirm toggle switch.
	/// Navigation cache mode is set to required.
	/// </summary>
	internal UpdatePageCustomMSIXPath()
	{
		this.InitializeComponent();

		// Set the initial state of things
		SetConfirmToggleSwitchState();

		this.NavigationCacheMode = NavigationCacheMode.Required;
	}


	/// <summary>
	/// Opens a file picker to select an MSIX file path. Updates the path on the main page and enables confirmation
	/// settings if valid.
	/// </summary>
	private void BrowseForCustomMSIXPathButton_Click()
	{
		// Offer file picker to select MSIX file path
		string? MSIXPath = FileDialogHelper.ShowFilePickerDialog("MSIX/MSIXBundle files|*.msixbundle;*.msix");

		// If user has selected a path and the file name is valid
		if (!string.IsNullOrWhiteSpace(MSIXPath))
		{
			// Update the path variable on the main update page
			UpdatePage.Instance.customMSIXBundlePath = MSIXPath;

			// Enable the confirmation settings card
			ConfirmUseOfCustomMSIXPathSettingsCard.IsEnabled = true;
		}
		else
		{
			ConfirmUseOfCustomMSIXPathSettingsCard.IsEnabled = false;
			ConfirmUseOfCustomMSIXPath.IsOn = false;

			// Revert the update button's text back to the default value
			GlobalVars.updateButtonTextOnTheUpdatePage = "Check for update";
		}
	}


	private void ConfirmUseOfCustomMSIXPath_Click()
	{
		ConfirmUseOfCustomMSIXPath.IsOn = !ConfirmUseOfCustomMSIXPath.IsOn;

		UpdatePage.Instance.useCustomMSIXBundlePath = ConfirmUseOfCustomMSIXPath.IsOn;

		SetConfirmToggleSwitchState();
	}


	/// <summary>
	/// Set the initial state of the toggle switch based on whether a custom MSIX file path is selected or not
	/// </summary>
	private void SetConfirmToggleSwitchState()
	{

		if (!string.IsNullOrEmpty(UpdatePage.Instance.customMSIXBundlePath))
		{
			ConfirmUseOfCustomMSIXPathSettingsCard.IsEnabled = true;

			if (ConfirmUseOfCustomMSIXPath.IsOn)
			{
				// Update the Update button's text content to reflect the selected MSIX file name
				GlobalVars.updateButtonTextOnTheUpdatePage = $"Install {Path.GetFileName(UpdatePage.Instance.customMSIXBundlePath)}";
			}
			else
			{
				// Revert the update button's text back to the default value
				GlobalVars.updateButtonTextOnTheUpdatePage = "Check for update";
			}
		}
		else
		{
			ConfirmUseOfCustomMSIXPathSettingsCard.IsEnabled = false;
			ConfirmUseOfCustomMSIXPath.IsOn = false;

			// Revert the update button's text back to the default value
			GlobalVars.updateButtonTextOnTheUpdatePage = "Check for update";
		}
	}
}
