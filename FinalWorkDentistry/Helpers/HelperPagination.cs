using FinalWorkDentistry.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace FinalWorkDentistry.Helpers
{
    public static class HelperPagination
    {
        public static HtmlString PaginationBootstrapServ(
            this IHtmlHelper helper,
            ServicePageModel modelService)
        {
            var tagUl = new TagBuilder("ul");
            tagUl.AddCssClass("pagination");
            tagUl.AddCssClass("ml-auto");

            string partRouteService = modelService.CategoryName == null
                ? "Services"
                : modelService.CategoryName;

            for (int i = 0; i < modelService.PagesQuantity; i++)
            {
                var tagLi = new TagBuilder("li");
                tagLi.AddCssClass("page-item");
                tagLi.AddCssClass("mx-1"); // Добавляем отступы между элементами

                var tagA = new TagBuilder("a");
                tagA.AddCssClass("page-link");

                if (modelService.ActivePage == i + 1)
                {
                    // Стили для активной страницы
                    tagA.MergeAttribute("style", "background-color: white; color: #3f555a; border-color: #536D86;");
                }
                else
                {
                    // Стили для неактивных страниц
                    tagA.MergeAttribute("style", "background-color: #536D86; color: white; border-color: #536D86;");
                }

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

        public static HtmlString Pagination(
            this IHtmlHelper helper,
            ServicePageModel model)
        {
            var tagDiv = new TagBuilder("div");
            tagDiv.AddCssClass("pagination-custom");
            tagDiv.AddCssClass("ml-auto");

            for (int i = 0; i < model.PagesQuantity; i++)
            {
                var tagA = new TagBuilder("a");
                tagA.AddCssClass("page-link");
                tagA.AddCssClass("mx-1"); // Добавляем отступы между элементами
                

                if (model.ActivePage == i + 1)
                {
                    // Стили для активной страницы
                    tagA.MergeAttribute("style", "background-color: white; color: #3f555a; border-color: #536D86; padding: 0.5em 1em; text-decoration: none;");
                }
                else
                {
                    // Стили для неактивных страниц
                    tagA.MergeAttribute("style", "background-color: #536D86; color: white; border-color: #536D86; padding: 0.5em 1em; text-decoration: none;");
                }

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
