using Microsoft.AspNet.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Yotodo.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
         private const string EmailDomain = "contoso.com";

        // Can be passed via <email mail-to="..." />. 
        // Pascal case gets translated into lower-kebab-case.
        // public string MailTo { get; set; }

        // public override void Process(TagHelperContext context, TagHelperOutput output)
        // {
        //     output.TagName = "a";    // Replaces <email> with <a> tag

        //     var address = MailTo + "@" + EmailDomain;
        //     output.Attributes["href"] = "mailto:" + address;
        //     output.Content.SetContent(address);
        // }
        
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";    // Replaces <email> with <a> tag
            var content = await output.GetChildContentAsync();
            var target = content.GetContent() + "@" + EmailDomain;
            output.Attributes["href"] = "mailto:" + target;
            output.Content.SetContent(target);
        }
    }
}
