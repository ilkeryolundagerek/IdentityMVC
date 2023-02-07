using IdentityMVC.Toolbox.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

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



    [HtmlTargetElement("ol")]
    public class CustomOL : TagHelper
    {
        public IEnumerable<string> CustomItems { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            foreach (var item in CustomItems)
            {
                output.Content.AppendHtml($"<li>{item.ToUrl()}</li>");
            }
        }
    }
}
