using System;
using System.Maui.CustomAttributes;
using System.Maui.Internals;

#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
using System.Maui.Core.UITests;
#endif

namespace System.Maui.Controls.Issues
{
#if UITEST
	[Category(UITestCategories.Navigation)]
	[NUnit.Framework.Category(Core.UITests.UITestCategories.UwpIgnore)]
#endif
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 1355, "Setting Main Page in quick succession causes crash on Android",
		PlatformAffected.Android)]
	public class Issue1355 : TestContentPage
	{
		int _runCount = 0;
		int _maxRunCount = 2;
		const string Success = "Success";

		protected override void Init()
		{
			Appearing += OnAppearing;
		}

		private void OnAppearing(object o, EventArgs eventArgs)
		{
			Application.Current.MainPage = CreatePage();
		}

		ContentPage CreatePage()
		{
			var page = new ContentPage
			{
				Content = new Label { Text = Success },
				Title = $"CreatePage Iteration: {_runCount}"
			};

			page.Appearing += (sender, args) =>
			{
				_runCount += 1;
				if (_runCount <= _maxRunCount)
				{
					Application.Current.MainPage = new NavigationPage(CreatePage());
				}
			};

			return page;
		}

#if UITEST
		[Test]
		public void SwitchMainPageOnAppearing()
		{
			// Without the fix, this would crash. If we're here at all, the test passed.
			RunningApp.WaitForElement(Success);
		}
#endif
	}
}
