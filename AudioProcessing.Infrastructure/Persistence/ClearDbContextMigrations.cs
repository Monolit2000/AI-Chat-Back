using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace AudioProcessing.Infrastructure.Persistence
{
    public static class DataCliner
    {
        public static void ClearDbContextMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationDbContext userContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //userContext.Database.EnsureDeleted();

            userContext.Database.ExecuteSqlRaw(@"
            DO $$ 
            BEGIN
                -- Удаление таблицы __EFMigrationsHistory
                EXECUTE 'DROP TABLE IF EXISTS ""public"".""__EFMigrationsHistory""';
        
                -- Удаление всех пользовательских схем
                DECLARE
                    r RECORD;
                BEGIN
                    FOR r IN (SELECT schema_name 
                              FROM information_schema.schemata 
                              WHERE schema_name NOT IN (
                                  'public', 'information_schema', 'pg_catalog', 
                                  'pg_toast', 'pg_temp_1', 'pg_toast_temp_1'
                              )) LOOP
                        EXECUTE 'DROP SCHEMA IF EXISTS ' || quote_ident(r.schema_name) || ' CASCADE';
                    END LOOP;
                END;
            END $$;
            ");

        }
    }
}
