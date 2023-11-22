using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;

namespace ECommerce.WebUI.TagHelpers
{
    [HtmlTargetElement("product-list-pager")]
    public class PagingTagHelper : TagHelper
    {
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }

        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }

        [HtmlAttributeName("current-category")]
        public int CurrentCategory { get; set; }

        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        [HtmlAttributeName("filter-az")]
        public string? Filter_Az { get; set; }
        [HtmlAttributeName("filter-hl")]

        public string? Filter_Hl { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            var sb = new StringBuilder();
            if (PageCount > 1)
            {
                sb.Append("<ul class='pagination'>");
                if (CurrentPage > 1)
                {
                    sb.Append($"<li class=\"page-item\"><a class=\"page-link\" href=/product/index?page={CurrentPage - 1}&category={CurrentCategory}&filteraz={Filter_Az}&filterhl={Filter_Hl}>Prev</a></li>");
                }
                for (int i = 1; i <= PageCount; i++)
                {
                    sb.AppendFormat("<li class='{0}'>", i == CurrentPage ? "page-item active" : "page-item");
                    sb.AppendFormat("<a class='page-link' href='/product/index?page={0}&category={1}&filteraz={3}&filterhl={4}'>{2}</a>",
                            i, CurrentCategory, i,Filter_Az,Filter_Hl);
                    sb.Append("</li>");
                }
                if (CurrentPage != PageCount) {
                    sb.Append($"<li class=\"page-item\"><a class=\"page-link\" href=/product/index?page={CurrentPage + 1}&category={CurrentCategory}&filteraz={Filter_Az}&filterhl={Filter_Hl}>Next</a></li>");
                }
                sb.Append("</ul>");
            }
            output.Content.SetHtmlContent(sb.ToString());
        }

    }
}
