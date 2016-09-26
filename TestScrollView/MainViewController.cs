using System;
using UIKit;
using CoreGraphics;

namespace LayoutSample
{
	public class MainViewController : UIViewController
	{
		private MainView mainView;

		public override void LoadView()
		{
			mainView = new MainView();
			this.View = mainView;
		}

		public override void ViewDidLoad()
		{
			//If you don't know what is this code meaning, try to comment it, then you will get what it is.
			this.AutomaticallyAdjustsScrollViewInsets = false;

			UIBarButtonItem left0 = new UIBarButtonItem("-Frame", UIBarButtonItemStyle.Done, delegate
			{
				mainView.DecreaseScrollViewFrame();
			});

			UIBarButtonItem left1 = new UIBarButtonItem("-CS", UIBarButtonItemStyle.Done, delegate
			{
				mainView.DecreaseScrollViewContentSize();
			});

			UIBarButtonItem right0 = new UIBarButtonItem("+Frame", UIBarButtonItemStyle.Done, delegate
			{
				mainView.IncreaseScrollViewFrame();
			});

			UIBarButtonItem right1 = new UIBarButtonItem("+CS", UIBarButtonItemStyle.Done, delegate
			{
				mainView.IncreaseScrollViewContentSize();
			});

			this.NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { left0,left1 }, true);

			this.NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { right0, right1 }, true);

		}
	}

	class MainView : UIView
	{
		private UIScrollView scrollView;
		private UIView containerView;
		private UIView topView;
		private UIView midView;
		private UIView bottomView;

		public MainView()
		{
			scrollView = new UIScrollView();
			scrollView.BackgroundColor = UIColor.Red;
			nfloat navigationBarHeight = 64;
			scrollView.Frame = new CGRect(0, navigationBarHeight, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 100 - navigationBarHeight);
			scrollView.ContentSize = scrollView.Frame.Size;
			this.AddSubview(scrollView);

			containerView = new UIView();
			containerView.BackgroundColor = UIColor.Green;
			scrollView.AddSubview(containerView);

			topView = new UIView();
			topView.BackgroundColor = UIColor.Blue;
			containerView.AddSubview(topView);

			midView = new UIView();
			midView.BackgroundColor = UIColor.Purple;
			containerView.AddSubview(midView);

			bottomView = new UIView();
			bottomView.BackgroundColor = UIColor.Yellow;
			containerView.AddSubview(bottomView);
		}

		//It will also be invoked when your view's Frame is changed, and of cause you can invoke it whenever you want
		public override void LayoutSubviews()
		{
			Console.WriteLine("LayoutSubviews");

			nfloat padding = 10;

			CGRect containerFrame = new CGRect(new CGPoint(0,0),scrollView.ContentSize);
			containerFrame.X += padding;
			containerFrame.Y += padding;
			containerFrame.Width -= 2 * padding;
			containerFrame.Height -= 2 * padding;
			containerView.Frame = containerFrame;

			nfloat bottomHeight = 100;
			nfloat topHeight = 0.3f * containerView.Frame.Height;
			nfloat midHeight = containerView.Frame.Height - topHeight - bottomHeight - 4 * padding;

			nfloat containerSubviewWidth = containerView.Frame.Width - 2 * padding;
			topView.Frame = new CGRect(padding, padding, containerSubviewWidth, topHeight);
			midView.Frame = new CGRect(padding, padding + topView.Frame.Y + topView.Frame.Height, containerSubviewWidth, midHeight);
			bottomView.Frame = new CGRect(padding, padding + midView.Frame.Y + midView.Frame.Height, containerSubviewWidth, bottomHeight);
		}

		public void IncreaseScrollViewContentSize()
		{
			CGSize tmpSize = scrollView.ContentSize;
			tmpSize.Height += 100;
			scrollView.ContentSize = tmpSize;
			LayoutSubviews();
		}

		public void DecreaseScrollViewContentSize()
		{
			CGSize tmpSize = scrollView.ContentSize;
			tmpSize.Height -= 100;
			scrollView.ContentSize = tmpSize;
			LayoutSubviews();
		}

		public void IncreaseScrollViewFrame()
		{
			CGRect tmpRect = scrollView.Frame;
			tmpRect.Height += 10;
			scrollView.ContentSize = new CGSize(tmpRect.Width, tmpRect.Height);
			scrollView.Frame = tmpRect;
		}

		public void DecreaseScrollViewFrame()
		{
			CGRect tmpRect = scrollView.Frame;
			tmpRect.Height -= 10;
			scrollView.ContentSize = new CGSize(tmpRect.Width, tmpRect.Height);
			scrollView.Frame = tmpRect;
		}
	}
}
