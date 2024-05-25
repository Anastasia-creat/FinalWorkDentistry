using FinalWorkDentistry.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Text.Encodings.Web;

namespace FinalWorkDentistry.Helpers
{
    public static class HelperPagination
    {
        public static HtmlString PaginationBootstrapServ(
            this IHtmlHelper helper,
            ServicePageModel modelService )
        {
            var tagUl = new TagBuilder("ul");
            tagUl.AddCssClass("pagination");

            string partRouteService = modelService.CategoryName == null
                ? "Services"
                : modelService.CategoryName;

           

            for (int i = 0; i < modelService.PagesQuantity; i++)
            {
                var tagLi = new TagBuilder("li"); // <li></li>
                tagLi.AddCssClass("page-item");

                if (modelService.ActivePage == i + 1)
                    tagLi.AddCssClass("active");

                var tagA = new TagBuilder("a"); // <a> </a>
                tagA.AddCssClass("page-link");

                tagA.MergeAttribute("href",
                    $"/Service/ListView/" +
                    $"?categoryName={modelService.CategoryName}" +
                    $"&page={i + 1}");

                tagA.InnerHtml.SetContent((i + 1).ToString());

                tagLi.InnerHtml.AppendHtml(tagA);

                tagUl.InnerHtml.AppendHtml(tagLi);
            }
            var writer = new System.IO.StringWriter();
            tagUl.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());

        }

        public static HtmlString PaginationBootstrapDoc(
           this IHtmlHelper helper,
            DoctorPageModel modelDoctor)
        {
            var tagUl = new TagBuilder("ul");
            tagUl.AddCssClass("pagination");


            string partRouteDoctor = modelDoctor.CategoryName == null
               ? "Doctors"
               : modelDoctor.CategoryName;
   

            for (int i = 0; i < modelDoctor.PagesQuantity; i++)
            {
                var tagLi = new TagBuilder("li"); // <li></li>
                tagLi.AddCssClass("page-item");

                if (modelDoctor.ActivePage == i + 1)
                    tagLi.AddCssClass("active");

                var tagA = new TagBuilder("a"); // <a> </a>
                tagA.AddCssClass("page-link");

                tagA.MergeAttribute("href",
                    $"/Doctor/ListView/" +
                    $"?categoryName={modelDoctor.CategoryName}" +
                    $"&page={i + 1}");

                tagA.InnerHtml.SetContent((i + 1).ToString());

                tagLi.InnerHtml.AppendHtml(tagA);

                tagUl.InnerHtml.AppendHtml(tagLi);
            }
            var writer = new System.IO.StringWriter();
            tagUl.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }




        public static HtmlString Pagination(
            this IHtmlHelper helper,
            ServicePageModel model)
        {
            var tagDiv = new TagBuilder("div");
            for (int i = 0; i < model.PagesQuantity; i++)
            {
                var tagA = new TagBuilder("a"); // <a> </a>
                tagA.MergeAttribute("href", $"/Services/Page{i + 1}");
                tagA.InnerHtml.SetContent((i + 1).ToString());
                tagDiv.InnerHtml.AppendHtml(tagA);
            }
            var writer = new System.IO.StringWriter();
            tagDiv.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
