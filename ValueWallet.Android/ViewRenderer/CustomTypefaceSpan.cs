using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ValueWallet.Droid.ViewRenderer
{
    public class CustomTypefaceSpan : MetricAffectingSpan
    {
        private readonly Typeface _typeFace;
        private readonly TextView _textView;
        private Font _font;
        public CustomTypefaceSpan(Context context, TextView textView, Font font, Label label)
        {
            _textView = textView;
            _font = font;

            string result = GetFontPath(_font.FontFamily ?? label.FontFamily, _font.FontAttributes);
            _typeFace = Typeface.CreateFromAsset(context.ApplicationContext.Assets, result);
        }

        private string GetFontPath(string fontFamily, FontAttributes? fontAttributes)
        {
            string fontPath;
            if (!string.IsNullOrEmpty(fontFamily))
                fontPath = $"Fonts/{fontFamily}.ttf";
            else if (fontAttributes == FontAttributes.Bold)
                fontPath = $"Fonts/EkkamaiNewBold.ttf";
            else
                fontPath = $"Fonts/EkkamaiNew.ttf";
            return fontPath;
        }
        public override void UpdateDrawState(TextPaint paint)
        {
            ApplyCustomTypeFace(paint);
        }

        public override void UpdateMeasureState(TextPaint paint)
        {
            ApplyCustomTypeFace(paint);
        }

        private void ApplyCustomTypeFace(Paint paint)
        {
            paint.SetTypeface(_typeFace);
            paint.TextSize = TypedValue.ApplyDimension(ComplexUnitType.Sp, _font.ToScaledPixel(), _textView.Resources.DisplayMetrics);
        }
    }
}