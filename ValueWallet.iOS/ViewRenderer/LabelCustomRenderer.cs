using System;
using System.ComponentModel;
using Foundation;
using UIKit;
using ValueWallet.iOS.ViewRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SizeF = CoreGraphics.CGSize;
using RectangleF = CoreGraphics.CGRect;

[assembly: ExportRenderer(typeof(Label), typeof(LabelCustomRenderer))]
namespace ValueWallet.iOS.ViewRenderer
{
    public class LabelCustomRenderer : LabelRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            try
            {
                // Make sure control is not null
                if (Element == null || Control == null) return;

                SetFontByNotUseTextSpan(Element);

                if (Element.FormattedText != null)
                {
                    UpdateFormattedText();
                }

                UpdateAlignment();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("iOS Label OnElementChanged Error ========> " + ex.Message);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Label.HorizontalTextAlignmentProperty.PropertyName)
                UpdateAlignment();
            else if (e.PropertyName == Label.VerticalTextAlignmentProperty.PropertyName)
                LayoutSubviews();
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            if (Control == null) return;

            SizeF fitSize;
            nfloat labelHeight;
            switch (Element.VerticalTextAlignment)
            {
                case TextAlignment.Start:
                    fitSize = Control.SizeThatFits(Element.Bounds.Size.ToSizeF());
                    labelHeight = (nfloat)Math.Min(Bounds.Height, fitSize.Height);
                    Control.Frame = new RectangleF(0, 0, (nfloat)Element.Width, labelHeight);
                    break;
                case TextAlignment.Center:
                    Control.Frame = new RectangleF(0, 0, (nfloat)Element.Width, (nfloat)Element.Height);
                    break;
                case TextAlignment.End:
                    fitSize = Control.SizeThatFits(Element.Bounds.Size.ToSizeF());
                    labelHeight = (nfloat)Math.Min(Bounds.Height, fitSize.Height);
                    Control.Frame = new RectangleF(0, 0, (nfloat)Element.Width, labelHeight);
                    break;
            }
        }

        private void UpdateAlignment()
        {
            Control.TextAlignment = Element.HorizontalTextAlignment switch
            {
                TextAlignment.Center => UITextAlignment.Center,
                TextAlignment.End => UITextAlignment.Right,
                _ => UITextAlignment.Left,
            };
            LayoutSubviews();
        }

        private void UpdateFormattedText()
        {
            try
            {
                if (Control?.AttributedText is NSMutableAttributedString text)
                {
                    string fontFamily = Element.FontFamily;
                    text.BeginEditing();
                    FixBackground(text);
                    if (Element.FormattedText == null)
                    {
                        return;
                    }
                    else
                    {
                        int location = 0;
                        foreach (Span span in Element.FormattedText.Spans)
                        {
                            string spanFamily = span.FontFamily ?? fontFamily;
                            FixFontAtLocation(location, text, spanFamily, span.FontAttributes);
                            location += span.Text.Length;
                        }
                    }
                    text.EndEditing();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("iOS CustomLabelRenderer : Error UpdateFormattedText() =======> " + ex.Message);
            }
        }

        private void SetFontByNotUseTextSpan(Label label)
        {
            if (!string.IsNullOrEmpty(label.Text))
            {
                NSMutableAttributedString labelString = new NSMutableAttributedString(label.Text);
                Control.AttributedText = labelString;
            }
        }

        private static void FixBackground(NSMutableAttributedString text)
        {
            try
            {
                string str = text.Value;

                if (string.IsNullOrEmpty(str))
                    return;

                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '\n')
                    {
                        text.RemoveAttribute(UIStringAttributeKey.BackgroundColor, new NSRange(i, 1));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("iOS LabelRenderer : Error FixBackground() " + ex.Message);
            }

        }

        private void FixFontAtLocation(int location, NSMutableAttributedString text, string fontFamily, FontAttributes? fontAttributes)
        {
            if (!string.IsNullOrEmpty(fontFamily))
            {
                NSObject objectNS = text.GetAttribute(UIStringAttributeKey.Font, location, out NSRange range);
                UIFont font = (UIFont)objectNS;
                string newName = GetFontName(fontFamily, fontAttributes);
                font = UIFont.FromName(newName, font.PointSize);
                text.RemoveAttribute(UIStringAttributeKey.Font, range);
                text.AddAttribute(UIStringAttributeKey.Font, font, range);
            }
        }

        private static string GetFontName(string fontFamily, FontAttributes? fontAttributes)
        {
            string pathFont;
            if (!string.IsNullOrEmpty(fontFamily))
                pathFont = $"Fonts/{fontFamily}.ttf";
            else if (fontAttributes == FontAttributes.Bold)
                pathFont = $"Fonts/EkkamaiNewBold.ttf";
            else
                pathFont = $"Fonts/EkkamaiNew.ttf";

            return pathFont;
        }
    }
}
