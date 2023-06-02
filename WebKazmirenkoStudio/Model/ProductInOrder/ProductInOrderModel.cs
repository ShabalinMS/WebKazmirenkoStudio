using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;
using WebKazmirenkoStudio.Model.Base;
using WebKazmirenkoStudio.Model.Product;

namespace WebKazmirenkoStudio.Model.ProductInOrder
{
    /// <summary>
    /// Продукты в заказе
    /// </summary>
    public class ProductInOrderModel :BaseEntity
    {

        /// <summary>
        /// Продукт
        /// </summary>
        [Display(Name = "Product", ResourceType = typeof(ProductInOrderResource))]
        public ProductModel Product { get; set; }

        /// <summary>
        /// Заказ
        /// </summary>
        [Display(Name = "Order", ResourceType = typeof(ProductInOrderResource))]
        public OrderModel Order { get; set; }
    }
}
