using IdentityMVC.Toolbox.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Security.Policy;
using System.Text.Encodings.Web;

namespace IdentityMVC
{
    [HtmlTargetElement("mail-to")]
    public class MailToTagHelper : TagHelper
    {
        public string Address { get; set; }
        public bool Active { get; set; } = true;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //TagMode.StartTagOnly => <br>, <hr>, <!DOCTYPE html>
            //TagMode.SelfClosing = <img />, <input />, <br />, <hr />
            //TagMode.StartTagAndEndTag = a, select, ul, ol, div, span, p ...
            output.TagName="a";
            output.TagMode=TagMode.StartTagAndEndTag;
            if (Active) output.Attributes.Add("href", $"mailto:{Address.ToLower()}");
            output.Content.SetContent(Address.ToLower().Replace("@", "(at)"));
        }
    }

    [HtmlTargetElement("img-sqr")]
    public class SquareImage : TagHelper
    {
        public string Image { get; set; }
        public string Size { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName ="img";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.Add("src", Image);
            output.Attributes.Add("width", Size);
            output.Attributes.Add("height", Size);
            output.Attributes.Add("alt", "...");
        }
    }


    [HtmlTargetElement("img")]
    public class SetSqrImage : TagHelper
    {
        public int CustomSize { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (CustomSize>0)
            {
                output.Attributes.Add("width", CustomSize);
                output.Attributes.Add("height", CustomSize);
            }
        }
    }

    [HtmlTargetElement("url")]
    public class UrlTagHelper : TagHelper
    {
        public string Text { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName="a";
            output.TagMode=TagMode.StartTagAndEndTag;
            output.Attributes.Add("href", $"{Text.ToUrl()}");
            output.Content.SetContent(Text);
        }
    }

    [HtmlTargetElement("ol")]
    public class CustomOL : TagHelper
    {
        public IEnumerable<string> CustomItems { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            foreach (var item in CustomItems)
            {
                //url etiketini oluşturuyor fakat taghelper onu render etmiyor.
                var url = new TagBuilder("url");
                url.Attributes.Add("text", item);
                var li = new TagBuilder("li");
                li.InnerHtml.SetHtmlContent(url);
                output.Content.AppendHtml(li);
            }
        }
    }

    public static class Tool
    {
        public static string TagToString(IHtmlContent tag)
        {
            using (var w = new StringWriter())
            {
                tag.WriteTo(w, HtmlEncoder.Default);
                return w.ToString();
            }
        }
    }
}
