using MetricService.BLL.Dto;
using MetricService.BLL.Exceptions;
using MetricService.BLL.Interfaces;
using MetricService.DAL.Interfaces;
using MetricService.Domain.Models;
using System.Security.Claims;
using UserService.Domain.Models;

namespace MetricService.BLL.Services
{
    public class HealthMetricsBaseService(IHealthMetricsBaseRepository healthMetricsBaseRepository, IValidator<HealthMetricsBase> validator,  ClaimsPrincipal authorizationService) : IHealthMetricsBaseService
    {
        private readonly IHealthMetricsBaseRepository _repository = healthMetricsBaseRepository;
        private readonly IValidator<HealthMetricsBase> _validator = validator;
        private readonly ClaimsPrincipal _authorizationService = authorizationService;


        /// <summary>
        /// Удалить запись о базовых медицинских показателей
        /// </summary>
        /// <param name="healthMetricsBaseId">идентификатор записи</param>
        /// <returns>true -  в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public async Task<bool> DeleteRecordOfHealthMetricsBaseAsync(int healthMetricsBaseId)
        {
            var healthMetricsBaseFind = await _repository.GetByIdAsync(healthMetricsBaseId);
            if (healthMetricsBaseFind == null) return false;

            if (!_authorizationService.IsInRole("Admin") && healthMetricsBaseFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено удалить только свою запись о базовых показателях здоровья", Common.Common.GetAuthorId(_authorizationService), healthMetricsBaseFind.UserId, _repository.Name);
            }

            return await _repository.DeleteAsync(healthMetricsBaseId);
        }



        /// <summary>
        /// Получить все записи для пользователя
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="begDate">начало периода для выборки</param>
        /// <param name="endDate">конец периода для выборки</param>
        /// <param name="pageNum">номер страницы для пагинации</param>
        /// <param name="pageSize">кол-во строк на странице для пагинации</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public async Task<IEnumerable<HealthMetricsBaseDTO>> GetAllRecordsOfHealthMetricsBaseByUserIdAsync(int userId, DateTime begDate, DateTime endDate, int pageNum, int pageSize)
        {
            if (!_authorizationService.IsInRole("Admin") && userId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о базовых показателях здоровья", Common.Common.GetAuthorId(_authorizationService), userId, _repository.Name);
            }

            var recordsOfHealthMetricsBase = (await _repository.GetAllAsync()).Where(h => h.UserId == userId && h.MetricDate >= begDate && h.MetricDate <= endDate)
                .Skip((pageNum - 1) * pageSize).Take(pageSize);

            var recordsOfHealthMetricsBaseDTO = new List<HealthMetricsBaseDTO>();
            if (recordsOfHealthMetricsBase.Any())
            {
                foreach (var healthMetricsBase in recordsOfHealthMetricsBase)
                {
                    recordsOfHealthMetricsBaseDTO.Add(CreateHealthMetricsBaseDTO(healthMetricsBase)!);
                }
            }
            return recordsOfHealthMetricsBaseDTO;
        }



        /// <summary>
        /// получить запись о базовых показателях пользователя
        /// </summary>
        /// <param name="healthMetricsBaseId">ИД записи</param>
        /// <returns>модель DTO</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        public async Task<HealthMetricsBaseDTO?> GetRecordOfHealthMetricsBaseByIdAsync(int healthMetricsBaseId)
        {
            var healthMetricsBaseFind = await _repository.GetByIdAsync(healthMetricsBaseId);
            if (healthMetricsBaseFind == null) return null;

            if (!_authorizationService.IsInRole("Admin") && healthMetricsBaseFind.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вам разрешено просматривать только свои записи о базовых показателях здоровья", Common.Common.GetAuthorId(_authorizationService), healthMetricsBaseFind.UserId, _repository.Name);
            }

            return CreateHealthMetricsBaseDTO(healthMetricsBaseFind);
        }



        /// <summary>
        /// обновить запись
        /// </summary>
        /// <param name="healthMetricsBaseDTO">информвция о записи</param>
        /// <returns>true - в случае успеха</returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public async Task<bool> UpdateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseDTO healthMetricsBaseDTO)
        {
            var findHealthMetricsBase = await _repository.GetByIdAsync(healthMetricsBaseDTO.Id);
            if (findHealthMetricsBase == null) return false;

            if (!_authorizationService.IsInRole("Admin") && findHealthMetricsBase.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new ViolationAccessException("Вы не можете изменять данные о базовых показателях здоровья для других пользователей", Common.Common.GetAuthorId(_authorizationService), findHealthMetricsBase.UserId, _repository.Name);
            }

            findHealthMetricsBase.BloodPressureDia = healthMetricsBaseDTO.BloodPressureDia;
            findHealthMetricsBase.BloodPressureSys = healthMetricsBaseDTO.BloodPressureSys;
            findHealthMetricsBase.WaterIntake = healthMetricsBaseDTO.WaterIntake;
            findHealthMetricsBase.BodyFatPercentage = healthMetricsBaseDTO.BodyFatPercentage;
            findHealthMetricsBase.HeartRate = healthMetricsBaseDTO.HeartRate;
            findHealthMetricsBase.MetricDate = healthMetricsBaseDTO.MetricDate;           

            if (!_validator.Validate(findHealthMetricsBase, out IDictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о базовых показателях здоровья пользователя", errorList);
            }            

            return await _repository.UpdateAsync(findHealthMetricsBase);
        }



        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="healthMetricsBaseDTO">Информация о записи</param>
        /// <returns></returns>
        /// <exception cref="ViolationAccessException">Возникает при нарушении уровня доступа к чужим данным</exception>
        /// <exception cref="ValidateModelException">Возникает когда данные содержат не корректные данные</exception>
        public async Task<bool> CreateRecordOfHealthMetricsBaseAsync(HealthMetricsBaseDTO healthMetricsBaseDTO)
        {
            if (!_authorizationService.IsInRole("Admin") && healthMetricsBaseDTO.UserId != Common.Common.GetAuthorId(_authorizationService))
            {
                throw new Exception("Вы не можете создавать данные о базовых показателях здоровья для других пользователей");
            }

            HealthMetricsBase healthMetricsBase = CreateModelFromDTO(healthMetricsBaseDTO);

            if (!_validator.Validate(healthMetricsBase, out IDictionary<string, string> errorList))
            {
                throw new ValidateModelException("Некорректные данные о базовых показателях здоровья пользователя", errorList);
            }

            return await _repository.CreateAsync(healthMetricsBase);

        }

        /// <summary>
        /// Создание HealthMetricsBaseDTO по модели "Базовые медицинские показатели"
        /// </summary>
        /// <param name="healthMetricsBase"></param>
        /// <returns></returns>
        private static HealthMetricsBaseDTO? CreateHealthMetricsBaseDTO(HealthMetricsBase? healthMetricsBase)
        {
            if (healthMetricsBase == null) return null;
            return new HealthMetricsBaseDTO
            {
                Id = healthMetricsBase.Id,
                BloodPressureDia = healthMetricsBase.BloodPressureDia,
                BloodPressureSys = healthMetricsBase.BloodPressureSys,
                BodyFatPercentage = healthMetricsBase.BodyFatPercentage,
                HeartRate = healthMetricsBase.HeartRate,
                MetricDate = healthMetricsBase.MetricDate,
                UserId = healthMetricsBase.UserId,
                WaterIntake = healthMetricsBase.WaterIntake
            };
        }



        /// <summary>
        /// Создание модели из DTO
        /// </summary>
        /// <param name="healthMetricsBaseDTO">модель DTO</param>
        /// <returns>модель</returns>
        private static HealthMetricsBase CreateModelFromDTO(HealthMetricsBaseDTO healthMetricsBaseDTO)
        {
            var healthMetricsBase = new HealthMetricsBase
            {
                Id = healthMetricsBaseDTO.Id,
                BloodPressureDia = healthMetricsBaseDTO.BloodPressureDia,
                BloodPressureSys = healthMetricsBaseDTO.BloodPressureSys,
                BodyFatPercentage = healthMetricsBaseDTO.BodyFatPercentage,
                HeartRate = healthMetricsBaseDTO.HeartRate,
                MetricDate = healthMetricsBaseDTO.MetricDate,
                UserId = healthMetricsBaseDTO.UserId,
                WaterIntake = healthMetricsBaseDTO.WaterIntake

            };
            return healthMetricsBase;
        }

    }
}
