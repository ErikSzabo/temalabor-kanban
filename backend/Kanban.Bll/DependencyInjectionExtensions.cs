using Kanban.Data;
using Kanban.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kanban.Bll
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddKanbanBll(this IServiceCollection services, string connectionString) =>
            services.AddKanbanContext(connectionString)
                        .AddCardRepository()
                        .AddColumnRepository()
                        .AddColumnService()
                        .AddCardService();

        public static IServiceCollection AddColumnService(this IServiceCollection services) =>
            services.AddScoped<IColumnService, ColumnService>();
        
        public static IServiceCollection AddCardService(this IServiceCollection services) =>
            services.AddScoped<ICardService, CardService>();

        public static IServiceCollection AddCardRepository(this IServiceCollection services) =>
            services.AddScoped<ICardRepository, CardRepository>();

        public static IServiceCollection AddColumnRepository(this IServiceCollection services) =>
            services.AddScoped<IColumnRespository, ColumnRepository>();

        public static IServiceCollection AddKanbanContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<KanbanContext>(options => options.UseSqlServer(connectionString));

    }
}
