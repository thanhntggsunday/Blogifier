using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogifier.Core.Common;
using Blogifier.Core.Modules.Pms.Interfaces;
using Blogifier.Core.Modules.Pms.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class PmsDefaultController : Controller
    {

        private ICartProvider _cartProvider;
        private IOrderProvider _orderProvider;
        private IProvider<ProductCategoryDto> _productCategoryProvider;
        private IProvider<ProductDto> _productProvider;
        private IProvider<TagDto> _tagProvider;

        public IActionResult Index(ICartProvider cartProvider, IOrderProvider orderProvider,
            IProvider<ProductCategoryDto> productCategoryProvider, IProvider<ProductDto> productProvider,
            IProvider<TagDto> tagProvider)
        {
            _productCategoryProvider = productCategoryProvider;
            _productProvider = productProvider;
            _cartProvider = cartProvider;
            _orderProvider = orderProvider;
            _tagProvider = tagProvider;

            return View($"~/{ApplicationSettings.BlogThemesFolder}/OneFour/Default.cshtml");
        }
    }
}