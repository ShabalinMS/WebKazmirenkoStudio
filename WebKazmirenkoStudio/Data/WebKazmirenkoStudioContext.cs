using Microsoft.EntityFrameworkCore;
using WebKazmirenkoStudio.Model;
using WebKazmirenkoStudio.Model.Contact;
using WebKazmirenkoStudio.Model.Lookup.StatusOrder;
using WebKazmirenkoStudio.Model.MaterialConsumptionFact;
using WebKazmirenkoStudio.Model.ProductInOrder;

namespace WebKazmirenkoStudio.Data
{
    /// <summary>
    /// Подключения к сущностям
    /// </summary>
    public class WebKazmirenkoStudioContext : DbContext
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public WebKazmirenkoStudioContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        /// <summary>
        /// Источник данных название сырья
        /// </summary>
        public DbSet<WebKazmirenkoStudio.Model.RawMaterialCaption> RawMaterialCaption { get; set; } = default!;

        /// <summary>
        /// Источник данных мера измерения
        /// </summary>
        public DbSet<WebKazmirenkoStudio.Model.MeasureOfMeasurement> MeasureOfMeasurement { get; set; } = default!;

        /// <summary>
        /// Источник данных справочники
        /// </summary>
        public DbSet<LookupModel> Lookup { get; set; } = default!;

        /// <summary>
        /// Источник данных закупка
        /// </summary>
        public DbSet<WebKazmirenkoStudio.Model.Purchase> Purchase { get; set; } = default!;

        /// <summary>
        /// Источник данных сырье
        /// </summary>
        public DbSet<WebKazmirenkoStudio.Model.RawMaterial> RawMaterial { get; set; } = default!;

        /// <summary>
        /// Источник данных магазин
        /// </summary>
        public DbSet<ShopModel> Shop { get; set; }

        /// <summary>
        /// Источник данных склад
        /// </summary>
        public DbSet<WarehouseModel> Warehouse { get; set; }

        /// <summary>
        /// Источник данных склад
        /// </summary>
        public DbSet<ProductModel> Product { get; set; }

        /// <summary>
        /// Плановый расход 
        /// </summary>
        public DbSet<MaterialConsumptionPlanModel> MaterialConsumptionPlan { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public DbSet<StatusOrderModel> StatusOrder { get; set; }

        /// <summary>
        /// Контакт
        /// </summary>
        public DbSet<ContactModel> Contact { get; set; }

        /// <summary>
        /// Заказ
        /// </summary>
        public DbSet<OrderModel> Order { get; set; }

        /// <summary>
        /// Продукты в заказе
        /// </summary>
        public DbSet<ProductInOrderModel> ProductInOrder{ get; set; }

        /// <summary>
        /// Фактический расход
        /// </summary>
        public DbSet<MaterialConsumptionFactModel> MaterialConsumptionFact { get; set; }

        /// <summary>
        /// Конфиг подключения
        /// </summary>
        /// <param name="optionsBuilder">Опции для сборки</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5433;Database=SoapMaking;Username=postgres;Password=postgres"
            );
    }
}
