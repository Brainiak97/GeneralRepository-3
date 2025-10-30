using MetricService.Api.Contracts.Services;

namespace MetricService.Api.Contracts
{
    public interface IMetricServiceClient : 
        IAccessToMetricsServiceClient, 
        IAnalysisCategoryServiceClient, 
        IAnalysisResultServiceClient,
        IAnalysisTypeServiceClient, 
        IDosageFormServiceClient, 
        IHealthConditionServiceClient, 
        IHealthMetricServiceClient,
        IHealthMetricValueServiceClient, 
        IIntakeServiceClient, 
        IMedicationServiceClient, 
        IPhysicalActivityServiceClient,
        IRegimenServiceClient, 
        IReminderServiceClient, 
        ISleepServiceClient, 
        IUserServiceClient, 
        IWorkoutServiceClient
    { }
}
