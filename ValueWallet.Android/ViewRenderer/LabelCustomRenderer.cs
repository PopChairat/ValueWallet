using System;
using Android.Content;
using ValueWallet.Droid.ViewRenderer;
using Xamarin.Forms;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;
using System.Reflection;
using Android.Text;

[assembly: ExportRenderer(typeof(Label), typeof(LabelCustomRenderer))]
namespace ValueWallet.Droid.ViewRenderer
{
    internal class LabelCustomRenderer : Xamarin.Forms.Platform.Android.FastRenderers.LabelRenderer
    {
        private readonly Context context;
        public LabelCustomRenderer(Context context) : base(context)
        {
            this.context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);


            if (Control == null) return;

            string pathFont;
            if (!string.IsNullOrEmpty(e.NewElement?.FontFamily))
                pathFont = $"Fonts/{e.NewElement.FontFamily}.ttf";
            else if (e.NewElement?.FontAttributes == FontAttributes.Bold)
                pathFont = $"Fonts/EkkamaiNewBold.ttf";
            else
                pathFont = $"Fonts/EkkamaiNew.ttf";
            // force no intrinsic padding
            Control.SetPadding(0, 0, 0, 0);
            Control.Typeface = Typeface.CreateFromAsset(Context.ApplicationContext.Assets, pathFont);
            Control.SetLineSpacing(0, 1.1f);

            if (Element?.FormattedText == null) return;

            Type extensionType = typeof(FormattedStringExtensions);
            Type type = extensionType.GetNestedType("FontSpan", BindingFlags.NonPublic);
            SpannableString ss = new SpannableString(Control.TextFormatted);
            Java.Lang.Object[] spans = ss.GetSpans(0, ss.ToString().Length, Java.Lang.Class.FromType(type));
            
            foreach (var span in spans)
            {
                int start = ss.GetSpanStart(span);
                int end = ss.GetSpanEnd(span);
                SpanTypes flags = ss.GetSpanFlags(span);
                Font font = (Font)type.GetProperty("Font").GetValue(span, null);
                ss.RemoveSpan(span);
                CustomTypefaceSpan newSpan = new CustomTypefaceSpan(context, Control, font, Element);
                ss.SetSpan(newSpan, start, end, flags);
            }

            Control.TextFormatted = ss;
        }
    }
}