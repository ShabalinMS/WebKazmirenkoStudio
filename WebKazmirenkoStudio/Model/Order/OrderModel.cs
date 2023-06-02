using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;
using WebKazmirenkoStudio.Model.Base;
using WebKazmirenkoStudio.Model.Contact;
using WebKazmirenkoStudio.Model.Lookup.StatusOrder;
using WebKazmirenkoStudio.Model.Order;

namespace WebKazmirenkoStudio.Model
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class OrderModel :BaseEntity
    {
        /// <summary>
        /// Состояние заказа
        /// </summary>
        [Display(Name = "StatusOrder", ResourceType = typeof(OrderResource))]
        public StatusOrderModel StatusOrder { get; set; }

        /// <summary>
        /// Контакт
        /// </summary>
        [Display(Name = "Contact", ResourceType = typeof(OrderResource))]
        public ContactModel? Contact { get; set; }

        /// <summary>
        /// Дата окончания работ
        /// </summary>
        [Display(Name = "MakeDate", ResourceType = typeof(OrderResource))]
        public DateTime MakeDate { get; set; }

        /// <summary>
        /// Дата доставки
        /// </summary>
        [Display(Name = "DeliveryDate", ResourceType = typeof(OrderResource))]
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// Цена доставки
        /// </summary>
        [Display(Name = "ShippingСost", ResourceType = typeof(OrderResource))]
        public float? ShippingСost { get; set; }

        /// <summary>
        /// Дата регистрации заказа
        /// </summary>
        [Display(Name = "RegistrationDate", ResourceType = typeof(OrderResource))]
        public DateTime RegistrationDate { get; set; }
    }
}
