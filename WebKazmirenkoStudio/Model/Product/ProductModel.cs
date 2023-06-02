using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebKazmirenkoStudio.Model.Base;
using WebKazmirenkoStudio.Model.Product;

namespace WebKazmirenkoStudio.Model
{
    /// <summary>
    /// Продукт
    /// </summary>
    public class ProductModel :BaseLookupEntity
    {
        /// <summary>
        /// Цена товара
        /// </summary>
        [Display(Name = "Price", ResourceType = typeof(ProductResource))]
        [Column(TypeName = "decimal(10, 2)")]
        public float Price { get; set; }
    }
}
