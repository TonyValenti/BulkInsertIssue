using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace ConsoleApp20 {
    public static class ModelBuilderExtensions {

        public static ModelBuilder ForEachProperty<T>(this ModelBuilder modelBuilder, Action<PropertyBuilder<T>> Action, Func<PropertyInfo, bool>? PropertyFilter = default, bool? StrictMatch = default) {
            var type = typeof(T);

            Func<PropertyInfo, bool> Filter1 = StrictMatch ?? true
                ? x => x.PropertyType == type
                : x => x.PropertyType.IsAssignableTo(type)
                ;

            var Filter2 = PropertyFilter ?? (x => true);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {

                {

                    var properties = entityType.ClrType.GetProperties()
                        .Where(Filter1)
                        .Where(Filter2)
                        .ToList()
                        ;

                    if (properties.Any()) {

                        //var Builder = modelBuilder.Entity(entityType.Name);
                        var Builder = new EntityTypeBuilder(entityType);

                        foreach (var property in properties) {
                            var Prop = Builder
                                .Property<T>(property.Name)
                                ;

                            Action(Prop);
                        }
                    }
                }
            }

            return modelBuilder;
        }

        public static ModelBuilder ForEachType<T>(this ModelBuilder modelBuilder, Action<EntityTypeBuilder<T>> Action, bool? StrictMatch = default)
            where T : class {

            var type = typeof(T);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
                if (type.IsAssignableFrom(entityType.ClrType)) {
                    var BuilderType = typeof(EntityTypeBuilder<>);

                    var SpecificType = BuilderType.MakeGenericType(type);

                    var Instance = Activator.CreateInstance(SpecificType, entityType) as EntityTypeBuilder<T>;

                    if (Instance is { }) {
                        Action(Instance);
                    }

                }
            }


            return modelBuilder;
        }
    }
}