using System;
using ValueWallet.Models;
using Xamarin.Forms;

namespace ValueWallet.Services.CoreService
{
    public class ControlStyleService
    {
        public static void RegisterStyles()
        {
			Color lightGray = (Color)Application.Current.Resources["LightGray"];
			Color gray = (Color)Application.Current.Resources["Gray"];
			Color navy = (Color)Application.Current.Resources["Navy"];
			Color yellowMs = (Color)Application.Current.Resources["YellowMs"];
			//#FFFFFF


			string fontNormal = "kkamaiNew";
			string fontBold = "EkkamaiNewBold";

			double screenWidth = LocalDeviceInfo.CurrentDevice.ScreenWidth;
			double multiplier;

			if (screenWidth >= 415)
			{
				// large screen
				multiplier = 1.05;
			}
			else if (screenWidth >= 375)
			{
				// normal screen
				multiplier = 1;
			}
			else
			{
				// small screen
				multiplier = 0.9;
			}

			//font size
			double headSize = 36d * multiplier;
			double subHeadSize = 24d * multiplier;
			double bodySize = 18d * multiplier;
			double subBodySize = 16d * multiplier;
			double smallSize = 14d * multiplier;

			//Scope
			{
				//header
				Style style = new(typeof(Label));
				style.Setters.Add(Label.FontFamilyProperty,fontBold);
				style.Setters.Add(Label.FontSizeProperty, headSize);
				style.Setters.Add(Label.TextColorProperty, navy);
				Application.Current.Resources["Style-Label-Header"] = style;
			}
			{
				//sub header
				Style style = new(typeof(Label));
				style.Setters.Add(Label.FontFamilyProperty, fontNormal);
				style.Setters.Add(Label.FontSizeProperty, subHeadSize);
				style.Setters.Add(Label.TextColorProperty, gray);
				Application.Current.Resources["Style-Label-SubHeader"] = style;
			}
            {
				//light gray sub header
				Style style = new(typeof(Label));
				style.Setters.Add(Label.FontFamilyProperty, fontNormal);
				style.Setters.Add(Label.FontSizeProperty, subHeadSize);
				style.Setters.Add(Label.TextColorProperty, lightGray);
				Application.Current.Resources["Style-Label-Detail"] = style;
			}
			{
				//light gray sub header
				Style style = new(typeof(Label));
				style.Setters.Add(Label.FontFamilyProperty, fontNormal);
				style.Setters.Add(Label.FontSizeProperty, bodySize);
				style.Setters.Add(Label.TextColorProperty, navy);
				Application.Current.Resources["Style-Label-Detail-nv"] = style;
			}

			//Entry
			{
				Style style = new Style(typeof(Entry));
				style.Setters.Add(Entry.FontFamilyProperty, fontNormal);
				style.Setters.Add(Entry.FontSizeProperty, bodySize);
				style.Setters.Add(Entry.TextColorProperty,navy);
				style.Setters.Add(Entry.PlaceholderColorProperty, lightGray);
				Application.Current.Resources["Style-Entry-Main"] = style;
			}

			//Button
			{
				Style style = new(typeof(Button));
				style.Setters.Add(Button.FontFamilyProperty,fontNormal);
				style.Setters.Add(Button.FontSizeProperty, 16d * multiplier);
				style.Setters.Add(Button.TextColorProperty, Color.White);
				style.Setters.Add(VisualElement.BackgroundColorProperty, yellowMs);
				style.Setters.Add(Button.CornerRadiusProperty, 45);
				style.Setters.Add(VisualElement.HeightRequestProperty, 54d);

				Trigger disabledTrigger = new(typeof(Button))
				{
					Property = VisualElement.IsEnabledProperty,
					Value = false
				};

				disabledTrigger.Setters.Add(Button.TextColorProperty, Color.FromHex("#A8A8A8"));
				disabledTrigger.Setters.Add(VisualElement.BackgroundColorProperty, Color.FromHex("#FFF3D5"));

				style.Triggers.Add(disabledTrigger);
				Application.Current.Resources["Style-Button-Primary"] = style;
			}
			{
				Style style = new Style(typeof(Button));
				style.Setters.Add(Button.FontFamilyProperty,fontNormal);
				style.Setters.Add(Button.FontSizeProperty, 17d * multiplier);
				style.Setters.Add(Button.TextColorProperty, Color.White);
				style.Setters.Add(VisualElement.BackgroundColorProperty, Color.FromHex("#3E3E3E"));
				style.Setters.Add(Button.CornerRadiusProperty, 0);
				style.Setters.Add(VisualElement.HeightRequestProperty, 54d);

				Trigger disabledTrigger = new Trigger(typeof(Button))
				{
					Property = VisualElement.IsEnabledProperty,
					Value = false
				};
				disabledTrigger.Setters.Add(Button.TextColorProperty, Color.FromHex("#CCCCCC"));
				disabledTrigger.Setters.Add(VisualElement.BackgroundColorProperty, Color.FromHex("#3E3E3E"));

				style.Triggers.Add(disabledTrigger);
				Application.Current.Resources["Style-Button-Secondary"] = style;
			}


		}
    }
}
