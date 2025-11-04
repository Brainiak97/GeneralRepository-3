using AutoMapper.Internal;
using ReportService.Api.Contracts.Data.Interfaces;
using ReportService.BLL.Data;
using Shared.Common.Extensions;

namespace ReportService.BLL.Common.DataSources.Containers;

/// <inheritdoc />
internal class DataSourceInstancesContainer : IDataSourceInstancesContainer
{
    private readonly IReadOnlyDictionary<string, List<TemplateField>> _reportTemplateFields;

    public DataSourceInstancesContainer()
    {
        _reportTemplateFields = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => !t.IsAbstract && typeof(IReportData).IsAssignableFrom(t))
            .Select<Type, KeyValuePair<string, List<TemplateField>>>(type =>
            {
                var templateFields = GetTypeTemplateFields(type);
                return new KeyValuePair<string, List<TemplateField>>(type.Name, templateFields);
            })
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    /// <inheritdoc />
    public List<TemplateField> GetDataSourceTemplateFieldsByName(string sourceTypeName)
    {
        var instance = _reportTemplateFields.TryGetValue(sourceTypeName, out var dataSourceType)
            ? dataSourceType
            : throw new InvalidOperationException($"Не найден тип источника данных - {sourceTypeName}");

        return instance;
    }

    private static List<TemplateField> GetTypeTemplateFields(Type type)
    {
        var typeProperties = type.GetProperties().Where(p => p.IsPublic());
        var templateFields = new List<TemplateField>();
        foreach (var property in typeProperties)
        {
            var mayBeNull = Nullable.GetUnderlyingType(property.PropertyType) is not null || !property.PropertyType.IsValueType;
            var templateField = new TemplateField(
                property.Name,
                property.PropertyType.ToString(),
                property.GetPropertyDisplayName(),
                mayBeNull);
            
            templateFields.Add(templateField);
        }  

        return templateFields;
    }
}